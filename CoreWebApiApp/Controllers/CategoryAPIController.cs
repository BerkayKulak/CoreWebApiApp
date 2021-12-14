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

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var cats = await _service.GetAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cat = await _service.GetAsync(id);
            return Ok(cat);
        }

        //[HttpPost]
        //[ActionName("PostFromBody")]
        //public async Task<IActionResult> PostAsync([FromBody]Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var cat = await _service.CreateAsync(category);
        //        return Ok(category);
        //    }

        //    return BadRequest(ModelState);

        //}

        //[HttpPost]
        //[ActionName("PostFromQuery")]
        //public async Task<IActionResult> PostQueryAsync(string categoryId,string categoryName,int basePrice)
        //{
        //    var cat = new Category()
        //    {
        //        CategoryId = categoryId,
        //        CategoryName = categoryName,
        //        BasePrice = basePrice
        //    };

        //    if (ModelState.IsValid)
        //    {
        //        cat = await _service.CreateAsync(cat);
        //        return Ok(cat);
        //    }
        //    return BadRequest(modelState: ModelState);
        //}

        //[HttpPost("{categoryId}/{categoryName}/{basePrice}")]
        //[ActionName("PostFromRoute")]
        //public async Task<IActionResult> PostRouteAsync([FromRoute]Category cat)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        cat = await _service.CreateAsync(cat);
        //        return Ok(cat);
        //    }
        //    return BadRequest(modelState: ModelState);
        //}

        [HttpPost]
        [ActionName("PostFromForm")]
        public async Task<IActionResult> PostFormAsync([FromForm] Category cat)
        {


            if (ModelState.IsValid)
            {
                cat = await _service.CreateAsync(cat);
                return Ok(cat);
            }
            return BadRequest(modelState: ModelState);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id , Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = await _service.UpdateAsync(id,category);
                return Ok(category);
            }

            return BadRequest(ModelState);

        }

        [HttpDelete("{id}")]
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
