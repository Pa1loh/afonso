using AutoMapper;
using Dominio;
using Dominio.Entidades;
using Dominio.Interfaces;
using FluentValidation;

namespace Servico.Servicos
{
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : EntidadeBase
    {
        private readonly IRepositorioBase<TEntity> _baseRepository;

        private readonly IMapper _mapper;
        public ServicoBase(IRepositorioBase<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TOutputModel Create<TInputModel, TOutputModel, TValidator>(TInputModel obj)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(obj);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Create(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;
        }

        public TOutputModel CreateAsync<TInputModel, TOutputModel, TValidator>(TInputModel obj)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(obj);

            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.CreateAsync(entity);
            _baseRepository.SaveChangesAsync();
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;
        }

        public void Delete(int id)
        {
            _baseRepository.Delete(id);
            _baseRepository.SaveChanges();
        }

        public IEnumerable<TOutputModel> GetAll<TOutputModel>() where TOutputModel : class
        {

            var entities = _baseRepository.GetAll().ToList();

            var outputModel = entities.Select(entity => _mapper.Map<TOutputModel>(entity));

            return outputModel;
        }

        public async Task<IEnumerable<TOutputModel>> GetAllAsync<TOutputModel>() where TOutputModel : class
        {

            var entities = await _baseRepository.GetAllAsync();

            var outputModel = entities.Select(entity => _mapper.Map<TOutputModel>(entity));

            return outputModel;
        }

        public TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class
        {

            var entity = _baseRepository.GetById(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<TOutputModel> GetByIdAsync<TOutputModel>(int id) where TOutputModel : class
        {

            var entity = await _baseRepository.GetByIdAsync(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel obj)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(obj);

            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(entity);
            _baseRepository.SaveChanges();
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;
        }

        public static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registro não detectados.");

            validator.ValidateAndThrow(obj);
        }

        public TOutputModel ObterOfertas<TOutputModel>(List<Produto> produtos) where TOutputModel : class
        {
            throw new NotImplementedException();
        }
    }
}
