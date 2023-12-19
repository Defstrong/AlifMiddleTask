namespace DataAccess;

public interface IBaseRepository<T>
    where T : class
{
    public Task<bool> CreateAsync(T entity, CancellationToken cancellationToken = default);

    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);

    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    public Task<T?> GetAsync(string id, CancellationToken cancellationToken = default);

    public IAsyncEnumerable<T> GetAsync(CancellationToken cancellationToken = default);
}
