using System.Net.WebSockets;
using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;

namespace Credit_Management_System.Services.Implementations
{
    public class GenericService<TVM, T> : IGenericService<TVM, T>
     where TVM : class
     where T : BaseEntity, new()
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TVM>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities != null
                ? _mapper.Map<IEnumerable<TVM>>(entities)
                : new List<TVM>();
        }

        public async Task<TVM?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var entity = await _repository.GetByIdAsync(id);
            return entity != null
                ? _mapper.Map<TVM>(entity)
                : null;
        }

        public async Task<TVM?> CreateAsync(TVM viewModel)
        {
            if (viewModel == null)
                return null;

            var entity = _mapper.Map<T>(viewModel);
            if (entity == null)
                return null;

            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<TVM>(createdEntity);
        }

        public async Task<TVM?> UpdateAsync(TVM viewModel)
        {
            if (viewModel == null)
                return null;

            var updatedEntity = _mapper.Map<T>(viewModel);
            if (updatedEntity == null)
                return null;

            var result = await _repository.UpdateAsync(updatedEntity);
            return _mapper.Map<TVM>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            var result = await _repository.UpdateAsync(entity);
            return result != null;
        }
    }

}
