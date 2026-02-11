namespace PaginationWebAPICRUD.Models
{
    public class RequestModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? item_name { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
