
using AutoMapper;
using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using FoodingApp.Library.Nutrition;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.EfCore.Services;

public class FoodingItemRepository : IFoodingItemRepository
{
    private readonly FoodingAppDb _context;
    private readonly IMapper _mapper;

    public FoodingItemRepository(FoodingAppDb context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task UpdateMineralsAsync(int id, MineralsDto dto)
    {

        FoodItemModel? entity = await _context.FoodItems
         .SingleOrDefaultAsync(e => e.Id == id)
         ?? throw new FoodItemException("No Food item with this id exists");

        entity.Nutrients.Minerals = (Minerals)dto;
        await _context.SaveChangesAsync();

    }

    public async Task UpdateVitaminsAsync(int id, VitaminsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
          .SingleOrDefaultAsync(e => e.Id == id)
          ?? throw new FoodItemException("No Food item with this id exists");

        entity.Nutrients.Vitamins = (Vitamins)dto;

        await _context.SaveChangesAsync();

    }

    public async Task UpdateMacrosAsync(int id, MacroNutrientsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
            .SingleOrDefaultAsync(e => e.Id == id)
            ?? throw new FoodItemException("No Food item with this id exists");

        entity.Nutrients.Macros = (MacroNutrients)dto;
        await _context.SaveChangesAsync();

    }
    public async Task<FoodItemModel> AddAsync(FoodItemForManipulationDto model)
    {
        var item = (FoodItemModel)model;
        await _context.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<IEnumerable<FoodItemDto>> GetAllAsync(CancellationToken ct)
    {
        IEnumerable<FoodItemDto> dtos = await _context.FoodItems
             .AsNoTrackingWithIdentityResolution()
             .AsSplitQuery()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Select(f => (FoodItemDto)f)
             .ToListAsync(ct);

        return dtos;
    }


    public async Task<FoodItemDto> GetByIdAsync(int id)
    {
        FoodItemDto? dto = await _context.FoodItems
             .AsNoTracking()
             .AsSplitQuery()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Where(fi => fi.Id == id)
             .Select(f => (FoodItemDto)f)
             .FirstOrDefaultAsync()
             ?? throw new FoodItemException("Nu such food item with this id found");



        return dto;
    }

    public async Task<MacroNutrientsDto> GetMacrosAsync(int foodItemId)
    {
        MacroNutrientsDto dto = await _context.FoodItems.AsNoTracking()
              .AsSplitQuery()
              .Include(fi => fi.Nutrients.Macros)
              .Where(fi => fi.Id == foodItemId)
              .Select(fi => (MacroNutrientsDto)fi)
              .FirstOrDefaultAsync()
              ?? throw new FoodItemException($"No food item found with this ID ");

        return dto;

    }

    public async Task<MineralsDto> GetMineralsAsync(int foodItemId)
    {
        MineralsDto dto = await _context.FoodItems.AsNoTracking()
             .AsSplitQuery()
             .Include(fi => fi.Nutrients.Minerals)
             .Where(fi => fi.Id == foodItemId)
             .Select(fi => (MineralsDto)fi)
             .FirstOrDefaultAsync()
             ?? throw new FoodItemException($"No food item found with this ID ");
        return dto;
    }

    public async Task<IEnumerable<FoodItemDto>> GetPagedAsync(int page, int pageSize)
    {
        IEnumerable<FoodItemDto> foodItemDtos = await _context.FoodItems
             .AsNoTracking()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Select(fi => (FoodItemDto)fi)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

        return foodItemDtos;
    }

    public async Task<VitaminsDto> GetVitaminsAsync(int foodItemId)
    {
        VitaminsDto dto = await _context.FoodItems
                .AsNoTracking()
                .AsSplitQuery()
                .Include(fi => fi.Nutrients.Vitamins)
                .Where(fi => fi.Id == foodItemId)
                .Select(fi => (VitaminsDto)fi)
                .FirstOrDefaultAsync()
                ?? throw new FoodItemException($"No food item found with this ID ");
        return dto;
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

    public async Task UpdateAsync(int id, FoodItemForManipulationDto dto)
    {
        FoodItemModel? entity =
            await _context.FoodItems
        .AsSplitQuery()
        .Include(f => f.Nutrients.Macros)
        .Include(f => f.Nutrients.Vitamins)
        .Include(f => f.Nutrients.Minerals)
        .SingleOrDefaultAsync(f => f.Id == id);


        if (entity is null)
            throw new FoodItemException($"Food item with ID {id} not found");

        entity = (FoodItemModel)dto;

        await _context.SaveChangesAsync();

    }
    public async Task PatchDocumentAsync(int id, FoodItemDto dto)
    {
        var entity = await _context.FoodItems
            .SingleOrDefaultAsync(f => f.Id == id)
            ?? throw new FoodItemException($"Food item with ID {id} not found");

        var category = await _context.Categories
            .SingleOrDefaultAsync(c =>
                c.PrimaryGroupId == dto.PrimaryCategoryId &&
                c.SubCategoryId == dto.SubCategoryId)
            ?? throw new CategoryException(
                $"No category found with PrimaryGroupId {dto.PrimaryCategoryId} and SubCategoryId {dto.SubCategoryId}");

        entity.ItemName = dto.ItemName;
        entity.CategoryId = category.Id;

        await _context.SaveChangesAsync();
    }

}