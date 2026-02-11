using Microsoft.AspNetCore.Mvc;
using PaginationWebAPICRUD.Data;
using PaginationWebAPICRUD.Models;
using PaginationWebAPICRUD.PaginationResult;

namespace PaginationWebAPICRUD.Interfaces
{
    public interface IProductService
    {
        Task<List<gadet>> GetItem();
        Task<gadet> gadetById(int id);
        Task<PageResult<gadet>> GetItems(int pageNumber, int pageSize);
        Task<gadet> CreateItems(gadet req);
        Task<PageResult<gadet>> SearchItems(RequestModel req);
        Task<gadet> UpdateItem(ItemUpdateRequest  req);

        Task<bool> DeleteItem(int id);





    }
}
