using Microsoft.EntityFrameworkCore;
using store_api.Models;

namespace store_api.Services
{
    public interface IDatabaseService<T>
    {
        public T DbContext { get; set; }
    }

    public class DatabaseService : IDatabaseService<NorthwindContext>
    {
        public NorthwindContext DbContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}