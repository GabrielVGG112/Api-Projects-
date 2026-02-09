using AutoMapper;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore;
using FoodingApp.Library.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.Api.Services;

public class FoodingAppSearchRepository : ISearchRepository
{
    private readonly FoodingAppDb _db;
    private readonly IMapper _mapper;

    public FoodingAppSearchRepository(FoodingAppDb db,IMapper mapper)
    {
        _db = db;
        _mapper = mapper; 
    }
     public async Task<PagedResultDto<FoodItemDto>> GetFoodItemAsync(FoodSearchQueryDto searchQuery,CancellationToken ct) 
    {
        var q =  _db.FoodItems
            .Include(f => f.Category.PrimaryGroup)
            .Include(f => f.Category.SubCategory)
            .Include(f => f.Nutrients).AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchQuery.Name))
        {
            q = q.Where(f => EF.Functions.Like(f.ItemName.ToLower(), $"%{searchQuery.Name}%"));

        }

        if (searchQuery.PrimaryCategoryId.HasValue)
        {
           q= q.Where(f=>f.Category.PrimaryGroup.Id == searchQuery.PrimaryCategoryId);
        }
       
        if (searchQuery.SubCategoryId.HasValue) 
        {
            q=q.Where (f=>f.Category.SubCategory.Id == searchQuery.SubCategoryId);
        }
        var totalCount = await q.CountAsync(ct);

        var items = await q
            .Skip((searchQuery.Page - 1) * searchQuery.PageSize)
             .Take(searchQuery.PageSize).ToArrayAsync(ct);
        return new PagedResultDto<FoodItemDto>
        {
            Count = totalCount, 
            Page = searchQuery.Page,
            items = _mapper.Map<IEnumerable<FoodItemDto>>(items),
            PageSize = searchQuery.PageSize,
        };
    }
    }
//public class FoodSearchQueryDto
//{
//    public string? Name { get; set; }
//    public string? Category { get; set; }
// public string? SubCategory { get; set; }
//    public int Page { get; set; } = 1;
//    public int PageSize { get; set; } = 20;
//    public string? SortBy { get; set; } = "name";
//    public bool Desc { get; set; } = false;
//}