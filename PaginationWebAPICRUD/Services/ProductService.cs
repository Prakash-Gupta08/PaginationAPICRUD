using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaginationWebAPICRUD.Data;
using PaginationWebAPICRUD.Interfaces;
using PaginationWebAPICRUD.ItemDBContext;
using PaginationWebAPICRUD.Models;
using PaginationWebAPICRUD.PaginationResult;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaginationWebAPICRUD.Services
{
    public class ProductService : IProductService
    {
        private readonly db_context _context;
        public ProductService(db_context sqlDBContext)
        {
            _context = sqlDBContext;
        }

        // Service for the Gadget-item (All)
        public async Task<List<gadet>> GetItem()
        {
            var data = await _context.gadet.ToListAsync();
            return data;
        }

        //Service for the Gadget-item by id
        public async Task<gadet> gadetById(int id)
        {
            var res = await _context.gadet.FindAsync(id);
            return res;
        }

        // Service for the Gadget-item by Pagination
        public async Task<PageResult<gadet>> GetItems(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.gadet.CountAsync();
            var res = await _context.gadet.OrderBy(s => s.item_id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<gadet>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = res
            };


        }

        public async Task<gadet> CreateItems(gadet req)
        {
            await _context.gadet.AddAsync(req);
            await _context.SaveChangesAsync();
            return req;
        }

        public async Task<PageResult<gadet>> SearchItems(RequestModel req)
        {
            var data = _context.gadet.AsQueryable();

            //Filter
            if (!string.IsNullOrEmpty(req.item_name))
            {
                data = data.Where(s => s.item_name.Contains(req.item_name));
            }
            // Total count before pagination
            int totalCount = await data.CountAsync();

            //Sorting
            if (!string.IsNullOrEmpty(req.SortBy))
            {
                if (req.SortBy.ToLower() == "name")
                {
                    data = req.SortOrder == "desc"
                        ? data.OrderByDescending(s => s.item_name)
                        : data.OrderBy(s => s.item_name);
                }
                else if (req.SortBy.ToLower() == "price")
                {
                    data = req.SortOrder == "desc"
                        ? data.OrderByDescending(s => s.item_price)
                        : data.OrderBy(s => s.item_price);
                }
            }

            //Pagination 
            var item = await data.Skip((req.PageNumber - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToListAsync();

            return new PageResult<gadet>
            {
                TotalPages = totalCount,
                Data = item
            };


        }

        public async Task<gadet> UpdateItem(ItemUpdateRequest req)
        {
            var product  = await _context.gadet.FindAsync(req.item_id);
            if (product == null)
            
                return null;

            product.item_name = req.item_name;
            product.item_price = req.item_price;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var product = await _context.gadet.FindAsync(id);
            if(product == null)
            {
                return false; // data not found
            }
            _context.gadet.Remove(product);
            await _context.SaveChangesAsync();
            return true; // delete successfully
        }
    }
}
