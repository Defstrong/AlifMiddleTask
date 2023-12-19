namespace DataAccess;

public interface IClientRepository : IBaseRepository<DbClient>
{
    Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default);
}