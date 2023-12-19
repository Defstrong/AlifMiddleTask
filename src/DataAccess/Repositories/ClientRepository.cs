using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class ClientRepository : BaseRepository<DbClient>, IClientRepository
{
    private readonly AlifDbContext _db;
    public ClientRepository(AlifDbContext db) : base(db) => _db = db;

    public Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default)
        => _db.Clients.AnyAsync(client => client.Id == id, cancellationToken);
}