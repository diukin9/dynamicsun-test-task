namespace DynamicSunTestTask.Application.ViewModels;

public class PageViewModel
{
    public int ItemCount { get; private set; }
    public int PageNumber { get; private set; } = 1;
    public int PerPage { get; private set; } = 100;

    public int TotalPages => PerPage == 0 ? 0 : (int)Math.Ceiling((double)ItemCount / PerPage);
    public bool HasPreviousPage => (PageNumber > 1);
    public bool HasNextPage => PageNumber < TotalPages;

    public PageViewModel(int itemCount, int pageNumber = 1, int pageSize= 100)
    {
        PageNumber = pageNumber;
        PerPage = pageSize;
        ItemCount = itemCount;
    }
}
