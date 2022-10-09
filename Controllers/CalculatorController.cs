using AutoMapper;
using CalorieCalculator.Dtos;
using CalorieCalculator.Helpers;
using CalorieCalculator.Models;
using CalorieCalculator.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CalorieCalculatorCore.Data;
using Microsoft.EntityFrameworkCore;
using CalorieCalculator.ActionFilters;
using CalorieCalculatorCore.ActionFilters;

namespace CalorieCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalculatorController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [ImportModelStateAttribute]
        public ActionResult Index()
        {
            var menus = _context.Menus
                                .Include(x => x.MenuItems)
                                .AsEnumerable();

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
            var menu = _context.Menus
                                .Include(m => m.MenuItems)
                                .ThenInclude(m => m.Product)
                                .SingleOrDefault(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            var menuDto = _mapper.Map<Menu, MenuDto>(menu);

            var measureTypes = _context.MeasureTypes
                                        .AsEnumerable();
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
            var menu = _context.Menus.SingleOrDefault(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            _context.Menus.Remove(menu);
            _context.SaveChanges();

            return RedirectToAction("Index", "Calculator");
        }

        //[HttpDelete]
        public ActionResult DeleteMenuItem(int id)
        {
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

            // išsirinkti produkto informaciją
            var response = await EdamamServiceHelper.GetProduct(menuItem.Name);
            ProductEdamamDto responseFoodDto = JsonConvert.DeserializeObject<ProductEdamamDto>(response);

            // ar yra toks produktas duombazėje
            var responseFood = responseFoodDto.Parsed.FirstOrDefault();
            if (responseFood == null)
            {
                ModelState.AddModelError("MenuItem.Name", "Invalid product");
                return RedirectToAction("Details", new { id = menuItem.MenuId });
            }

            string responseFoodId = responseFood.Food.FoodId;
            var productInDb =_context.Products.SingleOrDefault(p => p.FoodId == responseFoodId);

            // jei nėra išsisaugoti produktą
            if (productInDb == null)
            {
                Product product = _mapper.Map<ProductEdamamDto, Product>(responseFoodDto);
                _context.Products.Add(product);
                _context.SaveChanges();
                productInDb = _context.Products.SingleOrDefault(p => p.FoodId == responseFoodId);
            }
            // gaunam MenuItem navigation property
            MenuItem newMenuItem = _mapper.Map<MenuItemDto, MenuItem>(menuItem);
            _context.MenuItems.Add(newMenuItem);
            _context.Entry(newMenuItem).Reference(c => c.MeasureType).Load();

            // išsirenkam menuItem likusius duomenis
            var result2 = await EdamamServiceHelper.GetProductNutrition(newMenuItem.Quantity, newMenuItem.MeasureType.Uri, productInDb.FoodId);
            newMenuItem.ProductId = productInDb.Id;
            newMenuItem.Energy = result2.TotalNutrients.Energy.Quantity;
            newMenuItem.Carbs = result2.TotalNutrients.Carbs.Quantity;
            newMenuItem.Protein = result2.TotalNutrients.Protein.Quantity;
            newMenuItem.Fat = result2.TotalNutrients.Fat.Quantity;
            newMenuItem.Fiber = result2.TotalNutrients.Fiber.Quantity;
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
            _context.Menus.Add(newMenu);
            _context.SaveChanges();

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

            var menuInDb = _context.Menus.Single(m => m.Id == menuDto.Id);
            menuInDb.Name = menuDto.Name;
            _context.SaveChanges();

            return PartialView("MenuNamePartialView", menuDto);
        }
    }
}