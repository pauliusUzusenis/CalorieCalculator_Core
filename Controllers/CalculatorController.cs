using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CalorieCalculator.Dtos;
using CalorieCalculator.Helpers;
using CalorieCalculator.Models;
using CalorieCalculator.ViewModels;
using CalorieCalculatorCore.ActionFilters;
using CalorieCalculatorCore.Data;
using CalorieCalculatorCore.Repository;


namespace CalorieCalculator.Controllers
{
    [Authorize]
    public class CalculatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IProductRepository _productRepository;

        public CalculatorController(IMapper mapper, UserManager<IdentityUser> userManager, ApplicationDbContext context, 
            IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _menuRepository = menuRepository;            _menuItemRepository = menuItemRepository;            _productRepository = productRepository;
        }

        [HttpGet]
        [ImportModelStateAttribute]
        public ActionResult Index()
        {
            var menus = _menuRepository.GetAll(_userManager.GetUserId(User));
            var menusDto = _mapper.Map<IEnumerable<Menu>, IEnumerable<MenuDto>>(menus);
            return View(new IndexViewModel
            {
                Menus = menusDto
            });
        }

        [HttpGet]
        [ImportModelStateAttribute]
        public ActionResult Details(int id)
        {
            var menu = _menuRepository.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }
            var menuDto = _mapper.Map<Menu, MenuDto>(menu);

            var measureTypes = _context.MeasureTypes.AsEnumerable();
            var measureTypesDto = _mapper.Map<IEnumerable<MeasureType>, IEnumerable<MeasureTypeDto>>(measureTypes);

            var menuItemDto = new MenuItemDto
            {
                MenuId = id
            };

            var viewModel = new DetailsViewModel
            {
                MeasureTypes = measureTypesDto,
                Menu = menuDto,
                MenuItem = menuItemDto
            };

            return View(viewModel);
        }

        //[HttpDelete]
        public ActionResult DeleteMenu(int id)
        {
            var menu = _menuRepository.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }

            _menuRepository.Delete(id);
            _menuRepository.Save();

            return RedirectToAction("Index", "Calculator");
        }

        //[HttpDelete]
        public ActionResult DeleteMenuItem(int id)
        {
            var menuItem = _menuItemRepository.GetById(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _menuItemRepository.Delete(menuItem);
            _menuItemRepository.Save();

            return RedirectToAction("Details", "Calculator", new { id = menuItem.MenuId });
        }

        [HttpPost]
        [ExportModelStateAttribute]
        public async Task<ActionResult> Save(MenuItemDto menuItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = menuItem.MenuId });
            }

            EdamamProductDto responseProductDto = await EdamamServiceHelper.GetProduct(menuItem.Name);

            var responseParsedDto = responseProductDto.Parsed.FirstOrDefault();
            if (responseParsedDto == null)
            {
                ModelState.AddModelError("MenuItem.Name", "Invalid product");
                return RedirectToAction("Details", new { id = menuItem.MenuId });
            }

            var responseFoodId = responseParsedDto.Food.FoodId;
            var productInDb = _productRepository.GetByFoodId(responseFoodId);

            if (productInDb == null)
            {
                Product product = _mapper.Map<EdamamProductDto, Product>(responseProductDto);
                _productRepository.Insert(product);
                _productRepository.Save();
                productInDb = _productRepository.GetByFoodId(responseFoodId);
            }

            MenuItem newMenuItem = _mapper.Map<MenuItemDto, MenuItem>(menuItem);
            _menuItemRepository.Insert(newMenuItem);
            _context.Entry(newMenuItem).Reference(c => c.MeasureType).Load(); // load MenuItem navigation property

            // get remaining menuItem fields
            var productNutrients = await EdamamServiceHelper.GetProductNutrients(newMenuItem.Quantity, newMenuItem.MeasureType.Uri, productInDb.FoodId);
            newMenuItem.ProductId = productInDb.Id;
            newMenuItem.Energy = productNutrients.TotalNutrients.Energy.Quantity;
            newMenuItem.Carbs = productNutrients.TotalNutrients.Carbs.Quantity;
            newMenuItem.Protein = productNutrients.TotalNutrients.Protein.Quantity;
            newMenuItem.Fat = productNutrients.TotalNutrients.Fat.Quantity;
            newMenuItem.Fiber = productNutrients.TotalNutrients.Fiber != null ? productNutrients.TotalNutrients.Fiber.Quantity : 0;
            newMenuItem.MenuId = menuItem.MenuId;
            _menuItemRepository.Save();

            return RedirectToAction("Details", new { id = menuItem.MenuId });
        }

        [HttpPost]
        [ExportModelStateAttribute]
        public ActionResult SaveMenu(MenuDto menu)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            Menu newMenu = _mapper.Map<MenuDto, Menu>(menu);
            _menuRepository.Insert(newMenu);
            _menuRepository.Save();

            return RedirectToAction("Details", "Calculator", new { id = newMenu.Id });
        }

        [HttpPost]
        public ActionResult RenameMenu(MenuDto menuDto)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("MenuNamePartialView", menuDto);
            }

            var menuInDb = _menuRepository.GetById(menuDto.Id);
            menuInDb.Name = menuDto.Name;
            _menuRepository.Update(menuInDb);
            _menuRepository.Save();

            return PartialView("MenuNamePartialView", menuDto);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}