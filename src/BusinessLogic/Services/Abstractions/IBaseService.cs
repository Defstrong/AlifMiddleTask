namespace BusinessLogic;

public interface IBaseService<T>
    where T : class
{
    public Task<bool> CreateAsync(T model, CancellationToken cancellationToken = default);

    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);

    public Task<bool> UpdateAsync(T model, CancellationToken cancellationToken = default);

    public Task<T?> GetAsync(string id, CancellationToken cancellationToken = default);

    public IAsyncEnumerable<T> GetAsync(CancellationToken cancellationToken = default);
}
