using Classes;
using PoSSapi.Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbEntities _dbEntities;

        public CategoryRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _dbEntities.Categories.AsEnumerable();
        }

        public Category? GetCategoryById(string id)
        {
            return _dbEntities.Categories.FirstOrDefault(x => x.Id == id);
        }

        public void InsertCategory(Category category)
        {
            _dbEntities.Categories.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            var item = _dbEntities.Categories.First(x => x.Id == category.Id);
            item.Name = category.Name;
            item.ClientId = category.ClientId;
        }

        public void DeleteCategory(Category category)
        {
            _dbEntities.Categories.Remove(category);
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }
    }
}
