using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Extensions;
using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.Api.Services;

public class CategoryRepository : ICategoryRepository
{
    private readonly FoodingAppDb _context;

    public CategoryRepository(FoodingAppDb context)
    {
        _context = context;
    }
    public async Task<FoodCategory> AddAsync(FoodCategoryForManipulationDto entity)
    {
        FoodCategory category = entity.ToEntity();
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;


    }

    public async Task<IEnumerable<FoodCategoryDto>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.PrimaryGroup)
            .Include(c => c.SubCategory)
            .ToCategoryListDto()
            .ToArrayAsync();
    }

    public async Task<FoodCategoryDto> GetByIdAsync(int id)
    {
        var item = await _context.Categories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.PrimaryGroup)
            .Include(c => c.SubCategory).Where(x => x.Id == id).ToCategoryListDto()
            .FirstOrDefaultAsync();


        return item!;

    }

    public async Task SoftDeleteAsync(int id)
    {
        FoodCategory? item = await _context.Categories.SingleOrDefaultAsync(cat => cat.Id == id);
        if (item is null)
            throw new CategoryException($"No Food Category with id {id} exists");


        _context.Entry(item).Property("is_deleted").CurrentValue = true;

    }

    public async Task UpdateAsync(int id, FoodCategoryForManipulationDto entity)
    {
      FoodCategory? objectToUpdate = 
            await   _context.Categories
            .SingleOrDefaultAsync(x => x.Id == id) ??
             throw new CategoryException("No object to update found");


        PrimaryCategory?  nav1 = 
            await _context.PrimaryCategories
            .SingleOrDefaultAsync(pc=>pc.Id == entity.PrimaryGroupId) ??
             throw new CategoryException("No Primary Category with this id exists in db");


        SubCategory? nav2 = 
            await _context.SubCategories
            .SingleOrDefaultAsync(pc=>pc.Id == entity.SubCategoryId) ??
             throw new CategoryException("No Secondary Category with this id exists in db");


      

        objectToUpdate.PrimaryGroup = nav1;
        objectToUpdate.SubCategory = nav2;
      
    }

    public async Task<IEnumerable<FoodCategoryDto>> GetPagedAsync(int page, int pageSize)
    {
        List<FoodCategoryDto> list = await _context.Categories
               .Include(c => c.PrimaryGroup)
               .Include(c => c.SubCategory)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToCategoryListDto()
               .ToListAsync();
        return list;
    }

    public async Task<IEnumerable<FoodItemDto>> GetFoodItemsFromCategoryIdAsync(int categoryId)
    {
        return await _context.Categories
               .AsNoTracking()
               .AsSplitQuery()
               .Include(c => c.FoodItems)
               .Where(c => c.Id == categoryId)
               .SelectMany(c => c.FoodItems)
               .ToFoodItemDto().ToListAsync() 
               ?? throw new CategoryException("No such category with this id was founded");

    }
}
