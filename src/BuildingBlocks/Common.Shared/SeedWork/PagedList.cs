namespace Common.Shared.SeedWork
{
    public class PagedList<T>
    {
        public MetaData MetaData { get; set; }
        public List<T> Items { set; get; }

        public PagedList(List<T> items, long count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;
        }
    }
}