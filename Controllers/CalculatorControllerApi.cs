using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalorieCalculator.Dtos;
using CalorieCalculatorCore.Data;
using AutoMapper;
using CalorieCalculator.Models;
using CalorieCalculator.ViewModels;

namespace CalorieCalculatorCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorControllerApi : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalculatorControllerApi(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //GET: api/CalculatorControllerApi
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenu()
        //{
        //    var menu;
        //    var menus = await _context.Menus
        //            .Include(x => x.MenuItems)
        //            .AsEnumerable();

        //    var menusDto = _mapper.Map<IEnumerable<Menu>, IEnumerable<MenuDto>>(menus);
        //    return menusDto;

        //}

        //// GET: api/CalculatorControllerApi/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MenuDto>> GetMenuDto(int id)
        //{
        //    var menuDto = await _context.MenuDto.FindAsync(id);

        //    if (menuDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return menuDto;
        //}

        //// PUT: api/CalculatorControllerApi/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMenuDto(int id, MenuDto menuDto)
        //{
        //    if (id != menuDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(menuDto).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MenuDtoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/CalculatorControllerApi
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<MenuDto>> PostMenuDto(MenuDto menuDto)
        //{
        //    _context.MenuDto.Add(menuDto);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMenuDto", new { id = menuDto.Id }, menuDto);
        //}

        //// DELETE: api/CalculatorControllerApi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMenuDto(int id)
        //{
        //    var menuDto = await _context.MenuDto.FindAsync(id);
        //    if (menuDto == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MenuDto.Remove(menuDto);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MenuDtoExists(int id)
        //{
        //    return _context.MenuDto.Any(e => e.Id == id);
        //}
    }
}
