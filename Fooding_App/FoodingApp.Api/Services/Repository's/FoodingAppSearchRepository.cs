using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore;
using FoodingApp.Library.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.Api.Services;

public class FoodingAppSearchRepository : ISearchRepository
{
    private readonly FoodingAppDb _db;


    public FoodingAppSearchRepository(FoodingAppDb db)
    {
        _db = db;
     
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

        var items = searchQuery.Desc ?
            await q
            .OrderByDescending(c => c.ItemName)
            .Select(fi=> (FoodItemDto)fi)
            .Skip((searchQuery.Page - 1) * searchQuery.PageSize)
            .Take(searchQuery.PageSize)
        
            .ToArrayAsync(ct)
            :  // <-----
             await q
             .OrderBy(c => c.ItemName)
             .Select(fi => (FoodItemDto)fi)
             .Skip((searchQuery.Page - 1) * searchQuery.PageSize)
             .Take(searchQuery.PageSize)
             .ToArrayAsync(ct);
        return new PagedResultDto<FoodItemDto>
        {
            Count = totalCount,
            Page = searchQuery.Page,
            items = items,
            PageSize = searchQuery.PageSize,
        };
    }
    }
