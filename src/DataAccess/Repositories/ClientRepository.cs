namespace DataAccess;

public sealed class ClientRepository : BaseRepository<DbClient>, IClientRepository
{
    public ClientRepository(AlifDbContext db) : base(db) { }
}