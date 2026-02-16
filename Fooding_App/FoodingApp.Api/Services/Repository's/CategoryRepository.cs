using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
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
        FoodCategory category = (FoodCategory)entity;
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;


    }
    public async Task<IEnumerable<PrimaryCategory>> GeAllPrimaryCategoriesAsync(CancellationToken ct) 
    {
      return await  _context.PrimaryCategories.ToArrayAsync(ct);
    }
    public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(CancellationToken ct)
    {
        return await _context.SubCategories.ToArrayAsync(ct);
    }
    public async Task<IEnumerable<FoodCategoryDto>>GetAllSubcategoriesFromOnePrimaryAsync(int id , CancellationToken ct) 
    {
      var categories = await  _context.Categories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.PrimaryGroup)
            .Include(c => c.SubCategory)
            .Where(c => c.PrimaryGroup.Id == id)
            .Select(c=>(FoodCategoryDto)c)
            .ToArrayAsync(ct)
            ?? throw new CategoryException("No primary category with this id exists");
        return categories;
    }
    public async Task<IEnumerable<FoodCategoryDto>> GetAllAsync(CancellationToken ct)
    {
        IEnumerable<FoodCategoryDto> dtos = await _context.Categories
              .AsNoTracking()
              .AsSplitQuery()
              .Include(c => c.PrimaryGroup)
              .Include(c => c.SubCategory)
              .Select(c=>(FoodCategoryDto)c)
              .ToArrayAsync(ct);
        return dtos;

    }

    public async Task<FoodCategoryDto> GetByIdAsync(int id)
    {
        var item = await _context.Categories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.PrimaryGroup)
            .Include(c => c.SubCategory).Where(x => x.Id == id)
            .Select(c=>(FoodCategoryDto)c)
            .FirstOrDefaultAsync() ??
            throw new CategoryException("No category with this id exists");


        return item;

    }

    public async Task SoftDeleteAsync(int id)
    {
        FoodCategory? item = await _context.Categories.SingleOrDefaultAsync(cat => cat.Id == id)
        ??  throw new CategoryException($"No Food Category with id {id} exists");


        _context.Entry(item).Property("is_deleted").CurrentValue = true;

    }

    public async Task UpdateAsync(int id, FoodCategoryForManipulationDto entity)
    {
        FoodCategory? objectToUpdate =
              await _context.Categories
              .SingleOrDefaultAsync(x => x.Id == id) ??
               throw new CategoryException("No object to update found");


        PrimaryCategory? nav1 =
            await _context.PrimaryCategories
            .SingleOrDefaultAsync(pc => pc.Id == entity.PrimaryGroupId) ??
             throw new CategoryException("No Primary Category with this id exists in db");


        SubCategory? nav2 =
            await _context.SubCategories
            .SingleOrDefaultAsync(pc => pc.Id == entity.SubCategoryId) ??
             throw new CategoryException("No Secondary Category with this id exists in db");




        objectToUpdate.PrimaryGroup = nav1;
        objectToUpdate.SubCategory = nav2;

    }

    public async Task<IEnumerable<FoodCategoryDto>> GetPagedAsync(int page, int pageSize)
    {
        IEnumerable<FoodCategoryDto> dtos = await _context.Categories
                .Include(c => c.PrimaryGroup)
                .Include(c => c.SubCategory)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c=> (FoodCategoryDto)c)
                .ToArrayAsync();
        return dtos;
    }

    public async Task<IEnumerable<FoodItemDto>> GetFoodItemsFromCategoryIdAsync(int categoryId)
    {
        IEnumerable<FoodItemDto> dtos  = await _context.FoodItems
        .AsNoTracking()
        .Include(f => f.Category)
        .Where(f => f.CategoryId == categoryId)
        .Select(f => (FoodItemDto)f)
        .ToArrayAsync()?? throw new CategoryException("No such category with this id was founded");
        return dtos;

    }
    public async Task<FoodItemDto> GetSingleFoodItemFromCategoryAsync(int categoryId, int foodItemId)
    {
        FoodItemDto? dto = await _context.FoodItems
     .AsNoTracking()
     .Include(f => f.Category)
     .Where(f => f.CategoryId == categoryId && f.Id == foodItemId)
     .Select(f => (FoodItemDto)f)
     .SingleOrDefaultAsync();


        if (dto is null)
        {
            bool categoryExists = await _context.Categories.AsNoTracking().AnyAsync(c => c.Id == categoryId);
            if (!categoryExists) throw new CategoryException("No such category with this id was founded");

            throw new FoodItemException("No such Food Item with this id was found");


        }
        return dto;
    }
}
