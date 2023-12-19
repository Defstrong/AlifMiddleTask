using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;


public class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    private readonly AlifDbContext _db;

    public BaseRepository(AlifDbContext db) => _db = db;

    public async Task<bool> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _db.Set<T>().AddAsync(entity, cancellationToken);
        int createdResult = await _db.SaveChangesAsync(cancellationToken);
        return createdResult > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        T? entity = await _db.Set<T>().FindAsync(new object?[] { id }, cancellationToken);
        if (entity is null)
            return false;

        _db.Set<T>().Remove(entity);
        int deletedResult = await _db.SaveChangesAsync(cancellationToken);
        return deletedResult > 0;
    }

    public async Task<T?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        T? entity = await _db.Set<T>().FindAsync(new object?[] { id }, cancellationToken);
        return entity;
    }

    public async IAsyncEnumerable<T> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<T> entities = await _db.Set<T>().ToListAsync(cancellationToken);

        foreach (T? entity in entities)
            yield return entity;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellatoken = default)
    {
        _db.Entry(entity).State = EntityState.Modified;
        int updateResult = await _db.SaveChangesAsync(cancellatoken);
        return updateResult > 0;
    }
}