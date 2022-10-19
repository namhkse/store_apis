using store_api.Models;
using store_api.Utils;

namespace store_api.Services {
    public class OrderService : IOrderService {
        private readonly NorthwindContext _context;

        public OrderService(NorthwindContext context)
        {
            _context = context;
        }

        public Order FindOrder(int id)
        {
            return _context.Orders
                    .FirstOrDefault(o => o.OrderId == id);
        }

        public List<Order> GetOrdersWithPaging(int page, int pageSize) {
            return Pagination<Order>.Paginate(_context.Orders, page, pageSize).ToList();
        }
    }
}