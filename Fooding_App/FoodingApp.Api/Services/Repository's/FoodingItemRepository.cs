using Azure;
using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore.Configuration;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Extensions;
using FoodingApp.Library.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FoodingApp.EfCore.Services;

public class FoodingItemRepository : IFoodingItemRepository
{
    private readonly FoodingAppDb _context;



    public FoodingItemRepository(FoodingAppDb context)
    {
        _context = context;
    }

    public async Task UpdateMineralsAsync(int id, MineralsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
         .SingleOrDefaultAsync(e => e.Id == id)
         ?? throw new FoodItemException("No Food item with this id exists");
        if (entity is not null)
        {
            entity.Nutrients.Minerals.UpdateMineralsFromDto(dto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateVitaminsAsync(int id, VitaminsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
          .SingleOrDefaultAsync(e => e.Id == id)
          ?? throw new FoodItemException("No Food item with this id exists");
        if (entity is not null)
        {
            entity.Nutrients.Vitamins.UpdateVitaminsFromDto(dto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateMacrosAsync(int id, MacroNutrientsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
            .SingleOrDefaultAsync(e => e.Id == id)
            ?? throw new FoodItemException("No Food item with this id exists");

        entity.Nutrients.Macros.UpdateMacrosFromDto(dto);
        await _context.SaveChangesAsync();

    }
    public async Task<FoodItemModel> AddAsync(FoodItemForManipulationDto model)
    {
        var item = model.ToEnityModel();
        await _context.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<IEnumerable<FoodItemDto>> GetAllAsync()
    {
        var items = await _context.FoodItems
             .AsNoTrackingWithIdentityResolution()
             .AsSplitQuery()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .ToFoodItemDto()
             .ToListAsync();

        return items;
    }


    public async Task<FoodItemDto> GetByIdAsync(int id)
    {
        FoodItemDto? item = await _context.FoodItems
             .AsNoTracking()
             .AsSplitQuery()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Where(fi => fi.Id == id)
             .ToFoodItemDto()
             .FirstOrDefaultAsync();

        return item;
    }

    public async Task<MacroNutrientsDto> GetMacrosAsync(int foodItemId)
    {
        return await _context.FoodItems.AsNoTracking()
              .AsSplitQuery()
              .Include(fi => fi.Nutrients.Macros)
              .Where(fi => fi.Id == foodItemId)
              .ToMacroNutrientsDto()
              .FirstOrDefaultAsync()
              ?? throw new FoodItemException($"No food item found with this ID ");

    }

    public async Task<MineralsDto> GetMineralsDtoAsync(int foodItemId)
    {
        return await _context.FoodItems.AsNoTracking()
              .AsSplitQuery()
              .Include(fi => fi.Nutrients.Minerals)
              .Where(fi => fi.Id == foodItemId)
              .ToMineralsDto()
              .FirstOrDefaultAsync()
              ?? throw new FoodItemException($"No food item found with this ID ");
    }

    public async Task<IEnumerable<FoodItemDto>> GetPagedAsync(int page, int pageSize)
    {
        IEnumerable<FoodItemDto> foodItemDtos = await _context.FoodItems
             .AsNoTracking()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToFoodItemDto()
             .ToListAsync();

        return foodItemDtos;
    }

    public async Task<VitaminsDto> GetVitaminsAsync(int foodItemId)
    {
        return await _context.FoodItems
                .AsNoTracking()
                .AsSplitQuery()
                .Include(fi => fi.Nutrients.Vitamins)
                .Where(fi => fi.Id == foodItemId)
                .ToVitaminsDto()
                .FirstOrDefaultAsync()
                ?? throw new FoodItemException($"No food item found with this ID ");
    }

    public async Task SoftDeleteAsync(int id)
    {
        var item = await _context.FoodItems
            .SingleOrDefaultAsync(c => c.Id == id);

        if (item is null)
            throw new FoodItemException($"Food item with ID {id} not found");



        _context
           .Entry(item)
           .Property("is_deleted").CurrentValue = true;

        await _context.SaveChangesAsync();



    }

    public async Task UpdateAsync(int id, FoodItemForManipulationDto manipulationModel)
    {
        FoodItemModel? model = await _context.FoodItems
        .AsSplitQuery()
        .Include(f => f.Nutrients.Macros)
        .Include(f => f.Nutrients.Vitamins)
        .Include(f => f.Nutrients.Minerals)
        .SingleOrDefaultAsync(f => f.Id == id);


        if (model is null)
            throw new FoodItemException($"Food item with ID {id} not found");

        model.UpdateFromDto(manipulationModel);

        await _context.SaveChangesAsync();

    }

    public async Task PatchDocumentAsync(int id,FoodItemDto dto)
    {
       FoodItemModel? entity =await  _context.FoodItems.Include(f=>f.Category).SingleOrDefaultAsync(f => f.Id == id) 
            ?? throw new FoodItemException($"Food item with ID {id} not found");   

        FoodCategory? category = await _context.Categories.SingleOrDefaultAsync(c => c.PrimaryGroupId == dto.PrimaryCategoryId && c.SubCategoryId == dto.SubCategoryId) ?? 
            throw new FoodItemException($"No category found with PrimaryGroupId {dto.PrimaryCategoryId} and SubCategoryId {dto.SubCategoryId}");


        entity!.ItemName = dto.ItemName;
        entity.CategoryId = category!.Id;
        entity.Category.PrimaryGroup = category!.PrimaryGroup;
        entity.Category.SubCategory = category.SubCategory;
        await _context.SaveChangesAsync();

    }
}