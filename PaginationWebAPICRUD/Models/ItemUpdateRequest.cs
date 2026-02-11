namespace PaginationWebAPICRUD.Models
{
    public class ItemUpdateRequest
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public int item_price { get; set; }
    }
}
