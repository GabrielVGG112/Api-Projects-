using AutoMapper;
using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.EfCore.Services;

public class FoodingItemRepository : IFoodingItemRepository
{
    private readonly FoodingAppDb _context;
    private readonly IMapper _mapper;

    public FoodingItemRepository(FoodingAppDb context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task UpdateMineralsAsync(int id, MineralsDto dto)
    {
           
        FoodItemModel? entity = await _context.FoodItems
         .SingleOrDefaultAsync(e => e.Id == id)
         ?? throw new FoodItemException("No Food item with this id exists");
  
            _mapper.Map( dto, entity.Nutrients.Minerals);
            await _context.SaveChangesAsync();
        
    }

    public async Task UpdateVitaminsAsync(int id, VitaminsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
          .SingleOrDefaultAsync(e => e.Id == id)
          ?? throw new FoodItemException("No Food item with this id exists");
        if (entity is not null)
        {
            _mapper.Map(dto, entity.Nutrients.Vitamins);
       
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateMacrosAsync(int id, MacroNutrientsDto dto)
    {
        FoodItemModel? entity = await _context.FoodItems
            .SingleOrDefaultAsync(e => e.Id == id)
            ?? throw new FoodItemException("No Food item with this id exists");

        _mapper.Map(dto, entity.Nutrients.Minerals);
        await _context.SaveChangesAsync();

    }
    public async Task<FoodItemModel> AddAsync(FoodItemForManipulationDto model)
    {
        var item = _mapper.Map<FoodItemModel>(model);
        await _context.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<IEnumerable<FoodItemDto>> GetAllAsync()
    {
        IEnumerable<FoodItemModel> items = await _context.FoodItems
             .AsNoTrackingWithIdentityResolution()
             .AsSplitQuery()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .ToListAsync();

        IEnumerable<FoodItemDto> dto = _mapper.Map<IEnumerable<FoodItemDto>>(items);

        return dto;
    }


    public async Task<FoodItemDto> GetByIdAsync(int id)
    {
        FoodItemModel? entity = await _context.FoodItems
             .AsNoTracking()
             .AsSplitQuery()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Where(fi => fi.Id == id)
             .FirstOrDefaultAsync();
              
        FoodItemDto dto = _mapper.Map<FoodItemDto>(entity);

        return dto;
    }

    public async Task<MacroNutrientsDto> GetMacrosAsync(int foodItemId)
    {
        FoodItemModel entity = await _context.FoodItems.AsNoTracking()
              .AsSplitQuery()
              .Include(fi => fi.Nutrients.Macros)
              .Where(fi => fi.Id == foodItemId)
              .FirstOrDefaultAsync()
              ?? throw new FoodItemException($"No food item found with this ID ");

        return  _mapper.Map<MacroNutrientsDto>(entity.Nutrients.Macros);

    }

    public async Task<MineralsDto> GetMineralsDtoAsync(int foodItemId)
    {
        FoodItemModel entity=  await _context.FoodItems.AsNoTracking()
              .AsSplitQuery()
              .Include(fi => fi.Nutrients.Minerals)
              .Where(fi => fi.Id == foodItemId)
              .FirstOrDefaultAsync()
              ?? throw new FoodItemException($"No food item found with this ID ");
        return _mapper.Map<MineralsDto>(entity.Nutrients.Minerals);
    }

    public async Task<IEnumerable<FoodItemDto>> GetPagedAsync(int page, int pageSize)
    {
        IEnumerable<FoodItemModel> foodItemDtos = await _context.FoodItems
             .AsNoTracking()
             .Include(fi => fi.Category.PrimaryGroup)
             .Include(fi => fi.Category.SubCategory)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

        return _mapper.Map<IEnumerable<FoodItemDto>>(foodItemDtos);
    }

    public async Task<VitaminsDto> GetVitaminsAsync(int foodItemId)
    {
        FoodItemModel entity = await _context.FoodItems
                .AsNoTracking()
                .AsSplitQuery()
                .Include(fi => fi.Nutrients.Vitamins)
                .Where(fi => fi.Id == foodItemId)
                .FirstOrDefaultAsync()
                ?? throw new FoodItemException($"No food item found with this ID ");
        return _mapper.Map<VitaminsDto>(entity.Nutrients.Vitamins);
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
        FoodItemModel? entity = await _context.FoodItems
        .AsSplitQuery()
        .Include(f => f.Nutrients.Macros)
        .Include(f => f.Nutrients.Vitamins)
        .Include(f => f.Nutrients.Minerals)
        .SingleOrDefaultAsync(f => f.Id == id);


        if (entity is null)
            throw new FoodItemException($"Food item with ID {id} not found");

        _mapper.Map(dto, entity.Nutrients.Minerals);

        await _context.SaveChangesAsync();

    }

    public async Task PatchDocumentAsync(int id,FoodItemDto dto)
    {
       FoodItemModel? entity =await  _context.FoodItems
            .Include(f=>f.Category)
            .SingleOrDefaultAsync(f => f.Id == id) 
            ?? throw new FoodItemException($"Food item with ID {id} not found");   

        FoodCategory? category = await _context.Categories
            .SingleOrDefaultAsync(c => c.PrimaryGroupId == dto.PrimaryCategoryId && c.SubCategoryId == dto.SubCategoryId) ?? 
            throw new CategoryException($"No category found with PrimaryGroupId {dto.PrimaryCategoryId} and SubCategoryId {dto.SubCategoryId}");


        entity!.ItemName = dto.ItemName;
        entity.CategoryId = category!.Id;
        entity.Category.PrimaryGroup = category!.PrimaryGroup;
        entity.Category.SubCategory = category.SubCategory;
        await _context.SaveChangesAsync();

    }
}