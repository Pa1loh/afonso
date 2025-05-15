using Dominio.Entidades;
using FluentValidation;

namespace Dominio.Interfaces
{
    public interface IServicoBase<TEntity> where TEntity : EntidadeBase
    {
        TOutputModel Create<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        TOutputModel CreateAsync<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        void Delete(int id);
        IEnumerable<TOutputModel> GetAll<TOutputModel>() where TOutputModel : class;
        Task<IEnumerable<TOutputModel>> GetAllAsync<TOutputModel>() where TOutputModel : class;
        TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class;
        Task<TOutputModel> GetByIdAsync<TOutputModel>(int id) where TOutputModel : class;
        TOutputModel ObterOfertas<TOutputModel>(List<Produto> produtos) where TOutputModel : class;

    }
}
