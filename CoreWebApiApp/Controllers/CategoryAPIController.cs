using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiApp.Models;
using CoreWebApiApp.Services;

namespace CoreWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly IService<Category, int> _service;

        public CategoryAPIController(IService<Category, int> service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAsync()
        {
            var cats = await _service.GetAsync();
            return Ok(cats);
        }

        public async Task<IActionResult> GetAsync(int id)
        {
            var cat = await _service.GetAsync(id);
            return Ok(cat);
        }

        public async Task<IActionResult> PostAsync(Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = await _service.CreateAsync(category);
                return Ok(category);
            }

            return BadRequest(ModelState);

        }

        public async Task<IActionResult> PutAsync(int id , Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = await _service.UpdateAsync(id,category);
                return Ok(category);
            }

            return BadRequest(ModelState);

        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _service.DeleteAsync(id);
            if (res)
            {
                return Ok($"Record deleted successfully {res}");
            }

            return NotFound($"Record Not Found");
        }

    }
}
