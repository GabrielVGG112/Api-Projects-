namespace FoodingApp.Api.Services.Interfaces
{
    public interface IRepository<TEntity, TPresentationDto, TManipulationDto>
        where TEntity : class, new() where TPresentationDto : class, new()
    {

        public Task<IEnumerable<TPresentationDto>> GetAllAsync();
        public Task<TPresentationDto> GetByIdAsync(int id);
        public Task<TEntity> AddAsync(TManipulationDto entity);
        public Task UpdateAsync(int id, TManipulationDto entity);
        public Task SoftDeleteAsync(int id);

    }
}