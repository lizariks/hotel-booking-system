namespace WebApplication2.DTO;
using WebApplication2.Enteties;
public class RoomFilterDto
{
    public RoomType? RoomType { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? IsAvailable { get; set; }
    public string SortBy { get; set; } = "price"; 
    public string SortDirection { get; set; } = "asc"; 
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}