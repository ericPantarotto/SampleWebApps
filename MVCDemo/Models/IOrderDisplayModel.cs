using DataLibraryRepo.Models;

namespace MVCDemo.Models
{
    public interface IOrderDisplayModel
    {
        OrderModel Order { get; set; }
        string ItemPurchased { get; set; }
    }
}