using Credit_Management_System.ViewModels.Product;

namespace Credit_Management_System.ViewModels.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCategoryName { get; set; }

    }
}
