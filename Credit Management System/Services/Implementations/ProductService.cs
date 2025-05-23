using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Product;

namespace Credit_Management_System.Services.Implementations
{
    public class ProductService : GenericService<ProductVM, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
            : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ProductVM>> GetAllWithCategoryAsync()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

      

        public async Task<ProductVM?> GetByIdWithCategoryAsync(int id)
        {
            if (id <= 0)
                return null;

            var product = await _productRepository.GetByIdWithCategoryAsync(id);
            return product != null ? _mapper.Map<ProductVM>(product) : null;
        }

        public async Task<IEnumerable<ProductVM>> GetProductsByCategoryIdAsync(int categoryId)
        {
            if (categoryId <= 0)
                return Enumerable.Empty<ProductVM>();

            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<ProductUpdateVM> UpdateWithCategoryAsync(ProductUpdateVM productUpdateVM)
        {
            return productUpdateVM == null ? null : _mapper.Map<ProductUpdateVM>(await _productRepository.UpdateAsync(_mapper.Map<Product>(productUpdateVM)));
        }
        public async Task<ProductCreateVM> CreateWithCategoryAsync(ProductCreateVM productCreateVM)
        {
            return productCreateVM == null ? null : _mapper.Map<ProductCreateVM>(await _productRepository.AddAsync(_mapper.Map<Product>(productCreateVM)));
        }
        public async Task<ProductDetailsVM?> GetByIdVMWithCategoryAsync(int id)
        {
            var data = await _productRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<ProductDetailsVM>(data);
        }

        public async Task<ProductUpdateVM?> GetUpdateVMByIdAsync(int id)
        {
           var data = await _productRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<ProductUpdateVM>(data);
        }

        public async Task<ProductCreateVM> CreateWithImageAsync(ProductCreateVM vm)
        {
            if (vm == null) return null;
           
         
            if (vm.Image != null && vm.Image.Length > 0)
            {
               
                
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder); 
                }
               
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(fileStream);
                }

                vm.ImageUrl = $"/images/products/{uniqueFileName}";
            }

            var productEntity = _mapper.Map<Product>(vm);
            var created = await _productRepository.AddAsync(productEntity);
            return _mapper.Map<ProductCreateVM>(created);
        }

        public async Task<ProductUpdateVM> UpdateWithImageAsync(ProductUpdateVM updateVM)
        {
            if (updateVM == null || updateVM.Id <= 0)
                return null;

            var product = await _productRepository.GetByIdAsync(updateVM.Id);
            updateVM.ImageUrl = product.ImageUrl; 
            if (product == null)
                return null;

            if (updateVM.Image != null && updateVM.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(oldImagePath))
                        File.Delete(oldImagePath);
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(updateVM.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateVM.Image.CopyToAsync(stream);
                }

                product.ImageUrl = $"/images/products/{uniqueFileName}";
            }

            
            product.Name = updateVM.Name;
            product.Description = updateVM.Description;
            product.Price = updateVM.Price;
            product.Quantity = updateVM.Quantity; 
            product.IsAvailable = updateVM.IsAvailable;
            product.CategoryId = updateVM.CategoryId;

            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductUpdateVM>(product);
        }

        public async Task<bool> DeleteWithImageAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

          
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            await _productRepository.DeleteAsync(id);
            return true;
        }
    }

}
