using store_api.Models;

namespace store_api.Services
{
    public interface IOrderService
    {
        List<Order> GetOrdersWithPaging(int page, int pageSize);
        Order FindOrder(int id);
    }
}