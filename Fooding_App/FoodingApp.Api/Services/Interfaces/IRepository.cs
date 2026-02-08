namespace FoodingApp.Api.Services.Interfaces
{
    public interface IRepository<TEntity, TPresentationDto, TManipulationDto>
        where TEntity : class, new() where TPresentationDto : class, new()
    {

        Task<IEnumerable<TPresentationDto>> GetAllAsync(CancellationToken ct);
        Task<TPresentationDto> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TManipulationDto entity);
        Task UpdateAsync(int id, TManipulationDto entity);
        Task SoftDeleteAsync(int id);

    }
}