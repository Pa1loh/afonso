using Dominio.Entidades;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dominio;

public interface IRepositorioBase<TEntity> where TEntity : EntidadeBase
{
    void Create(TEntity obj);
    Task<EntityEntry<TEntity>> CreateAsync(TEntity obj);
    void Update(TEntity obj);
    bool Delete(int id);
    TEntity GetById(int id);
    public TEntity GetByIdAsNoTracking(int id);
    Task<TEntity> GetByIdAsync(int id);
    IQueryable<TEntity> GetAll();
    Task<IList<TEntity>> GetAllAsync();
    void SaveChanges();
    Task SaveChangesAsync();

}
