using AutoMapper;
using CalorieCalculator.Dtos;
using CalorieCalculator.Helpers;
using CalorieCalculator.Models;
using CalorieCalculator.ViewModels;
using CalorieCalculatorCore.ActionFilters;
using CalorieCalculatorCore.Data;
using CalorieCalculatorCore.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CalorieCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;

        public CalculatorController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _menuRepository = new MenuRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [ImportModelStateAttribute]
        public ActionResult Index()
        {
            var menus = _menuRepository.GetAll();
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
            //TODO remove eager loading of menuItems
            //TODO check if notFound section is necessary
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
            // TODO implement _menuItemRepository
            var menuItem = _context.MenuItems.SingleOrDefault(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            var menuId = menuItem.MenuId;
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();

            return RedirectToAction("Details", "Calculator", new { id = menuId });
        }

        [HttpPost]
        [ExportModelStateAttribute]
        public async Task<ActionResult> Save(MenuItemDto menuItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = menuItem.MenuId });
            }

            var response = await EdamamServiceHelper.GetProduct(menuItem.Name);
            ProductEdamamDto responseFoodDto = JsonConvert.DeserializeObject<ProductEdamamDto>(response);

            var responseFood = responseFoodDto.Parsed.FirstOrDefault();
            if (responseFood == null)
            {
                ModelState.AddModelError("MenuItem.Name", "Invalid product");
                return RedirectToAction("Details", new { id = menuItem.MenuId });
            }

            // TODO implement _productRepository
            string responseFoodId = responseFood.Food.FoodId;
            var productInDb = _context.Products.SingleOrDefault(p => p.FoodId == responseFoodId);

            if (productInDb == null)
            {
                Product product = _mapper.Map<ProductEdamamDto, Product>(responseFoodDto);
                _context.Products.Add(product);
                _context.SaveChanges();
                productInDb = _context.Products.SingleOrDefault(p => p.FoodId == responseFoodId);
            }

            MenuItem newMenuItem = _mapper.Map<MenuItemDto, MenuItem>(menuItem);
            _context.MenuItems.Add(newMenuItem);
            _context.Entry(newMenuItem).Reference(c => c.MeasureType).Load(); // gaunam MenuItem navigation property

            // išsirenkam menuItem likusius duomenis
            var result2 = await EdamamServiceHelper.GetProductNutrition(newMenuItem.Quantity, newMenuItem.MeasureType.Uri, productInDb.FoodId);
            newMenuItem.ProductId = productInDb.Id;
            newMenuItem.Energy = result2.TotalNutrients.Energy.Quantity;
            newMenuItem.Carbs = result2.TotalNutrients.Carbs.Quantity;
            newMenuItem.Protein = result2.TotalNutrients.Protein.Quantity;
            newMenuItem.Fat = result2.TotalNutrients.Fat.Quantity;
            newMenuItem.Fiber = result2.TotalNutrients.Fiber != null ? result2.TotalNutrients.Fiber.Quantity : 0;
            newMenuItem.MenuId = menuItem.MenuId;
            _context.SaveChanges();

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
    }
}