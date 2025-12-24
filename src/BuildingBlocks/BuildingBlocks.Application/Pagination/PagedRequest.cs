namespace BuildingBlocks.Application.Pagination;

public record PagedRequest
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    public int Page { get; init; } = 1;
    public int PageSize 
    {

        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    
    }  // may be reading it from appsettings


}
