namespace store_api.Utils
{
    public static class Pagination<T>
    {
        public static IEnumerable<T> Paginate(IEnumerable<T> ls, int page, int pageSize = 10)
        {
            int left = (page - 1) * pageSize;
            int numberOfProduct = ls.Count();
            int totalPage = numberOfProduct / pageSize + 1;

            if (page > totalPage) return null;

            if ((left + pageSize) > numberOfProduct)
            {
                pageSize = numberOfProduct - left;
            }

            return ls.Skip(left).Take(pageSize);
        }
    }
}