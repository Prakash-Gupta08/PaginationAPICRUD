using System.ComponentModel.DataAnnotations;

namespace PaginationWebAPICRUD.Data
{
    public class gadet
    {

       

        [Key]
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_use { get; set; }
        public int item_price { get; set; }
      
    }
}
