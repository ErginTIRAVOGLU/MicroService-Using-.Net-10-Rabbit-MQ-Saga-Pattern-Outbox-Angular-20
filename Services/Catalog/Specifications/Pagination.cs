namespace Catalog.Specifications;

public sealed class Pagination<T>  
{
    public Pagination() { }
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyCollection<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyCollection<T> Data { get; set; }= new List<T>();

     public Pagination<TResult> Map<TResult>(Func<T, TResult> selector) 
    {
        return new Pagination<TResult>(
            PageIndex,
            PageSize,
            Count,
            Data.Select(selector).ToList()
        );
    }
}
