using OpenAIPoC.API.Domain.Common;

public interface IRepository<T> where T : IEntity
{
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(string id);
}
