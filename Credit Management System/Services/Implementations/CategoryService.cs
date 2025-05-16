using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Category;

namespace Credit_Management_System.Services.Implementations
{
    public class CategoryService : GenericService<CategoryVM, Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
            : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllWithRelationsAsync()
        {
            var categories = await _categoryRepository.GetAllWithRelationsAsync();
            return _mapper.Map<IEnumerable<CategoryVM>>(categories);
        }

        public async Task<CategoryVM?> GetByIdWithRelationsAsync(int id)
        {
            if (id <= 0)
                return null;

            var category = await _categoryRepository.GetByIdWithRelationsAsync(id);
            return category != null ? _mapper.Map<CategoryVM>(category) : null;
        }

        public async Task<IEnumerable<CategoryVM>> GetByParentCategoryIdAsync(int parentCategoryId)
        {
            if (parentCategoryId <= 0)
                return new List<CategoryVM>();

            var categories = await _categoryRepository.GetByParentCategoryIdAsync(parentCategoryId);
            return _mapper.Map<IEnumerable<CategoryVM>>(categories);
        }

        public async Task<CategoryUpdateVM> UpdateWithRelationsAsync(CategoryUpdateVM categoryUpdateVM)
        {
           return categoryUpdateVM == null ? null : _mapper.Map<CategoryUpdateVM>(await _categoryRepository.UpdateAsync(_mapper.Map<Category>(categoryUpdateVM)));
        }

        public async Task<CategoryUpdateVM?> GetByIdUpdateVMWithRelationsAsync(int id)
        {
            var data = await _categoryRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<CategoryUpdateVM>(data);
        }

        public async Task<CategoryDetailsVM?> GetByIdVMWithRelationsAsync(int id)
        {
            var data = await _categoryRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<CategoryDetailsVM>(data);
        }

        public async Task<CategoryCreateVM> CreateWithRelationsAsync(CategoryCreateVM categoryCreateVM)
        {
            return categoryCreateVM == null ? null : _mapper.Map<CategoryCreateVM>(await _categoryRepository.AddAsync(_mapper.Map<Category>(categoryCreateVM)));
        }

        public async Task<IEnumerable<CategoryVM>> GetAllExceptIdAsync(int id)
        {
            return id <= 0 ? Enumerable.Empty<CategoryVM>() : _mapper.Map<IEnumerable<CategoryVM>>(await _categoryRepository.GetAllExceptIdAsync(id));
        }


    }

}
