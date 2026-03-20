using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaginationWebAPICRUD.Data;
using PaginationWebAPICRUD.Interfaces;
using PaginationWebAPICRUD.Models;

namespace PaginationWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly IProductService _context;
        public ItemAPIController(IProductService context)
        {
            _context = context;
        }

        // Get method for the gadget 
        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetItem()
        {
            var data = await _context.GetItem();
            return Ok(data);
        }


        // Get method for the gadget with Id
        [HttpGet("{id}")]
        public async Task<ActionResult> gadetById(int id)
        {
            var res = await _context.gadetById(id);
            return Ok(res);
        }


        // Get method for the gadget with the pagination
        [HttpGet("paged")]

        public async Task<ActionResult> GetItems(int pageNumber, int pageSize)
        {
            var result = await _context.GetItems(pageNumber, pageSize);
            return Ok(result);
        }

        // Post method for the gadget
        [HttpPost]
        public async Task<ActionResult> CreateItems(gadet req)
        {
            await _context.CreateItems(req);
            return Ok();
        }

        // Post method request for the gadets using search filter
        [HttpPost("Search")]
        public async Task<ActionResult> SearchItems(RequestModel req)
        {
            var res = await _context.SearchItems(req);
            return Ok(res);
        }

        [HttpPut("UpdateIteminpage")]
        public async Task<ActionResult> UpdateItem(ItemUpdateRequest req)
        {
            var newItem = await _context.UpdateItem(req);
            if (newItem == null)
            {
                return NotFound();
            }
            return Ok(newItem);

        }


        [HttpDelete("DeleteItem")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var product= await _context.DeleteItem(id);
            if(!product)
            {
                return NotFound("Itmes not found"); 
            }
            return Ok("Item is deleted");
        }




    }
}
