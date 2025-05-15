using Dominio;
using Dominio.Entidades;
using InfraEstrutura.BancoDeDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace InfraEstrutura;

public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : EntidadeBase
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public RepositorioBase(ApiDBContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual void Create(TEntity obj)
    {
        _dbSet.Add(obj);
        _context.SaveChanges();
        _context.Entry(obj).State = EntityState.Detached;

    }

    public virtual async Task<EntityEntry<TEntity>> CreateAsync(TEntity obj)
    {
        return await _dbSet.AddAsync(obj);
    }

    public virtual void Update(TEntity obj)
    {
        _dbSet.Entry(obj).State = EntityState.Modified;
    }

    public virtual bool Delete(int id)
    {
        var obj = GetByIdAsNoTracking(id);
        if (obj == null)
            return false;

        _dbSet.Remove(obj);

        return true;
    }

    public virtual TEntity GetById(int id)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public virtual TEntity GetByIdAsNoTracking(int id)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
