using System.ComponentModel;

namespace RPDemo.Models
{
    public class OrderUpdateModel
    {
        public int Id { get; set; }
        
        [DisplayName("Updated Order Name")]
        public string OrderName { get; set; }
    }
}
