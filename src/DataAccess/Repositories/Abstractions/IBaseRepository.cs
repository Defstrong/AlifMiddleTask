namespace DataAccess;

public interface IBaseRepository<T>
    where T : class
{
    public Task<bool> CreateAsync(T entity, CancellationToken token = default);

    public Task<bool> DeleteAsync(string id, CancellationToken token = default);

    public Task<bool> UpdateAsync(T entity, CancellationToken token = default);

    public Task<T?> GetAsync(string id, CancellationToken token = default);

    public IAsyncEnumerable<T> GetAsync(CancellationToken token = default);
}