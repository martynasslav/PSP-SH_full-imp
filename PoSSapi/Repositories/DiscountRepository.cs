using Classes;
using PoSSapi.Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DbEntities _dbEntities;

        public DiscountRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Discount> GetDiscounts()
        {
            return _dbEntities.Discounts.AsEnumerable();
        }

        public Discount? GetDiscountById(string id)
        {
            return _dbEntities.Discounts.FirstOrDefault(x => x.Id == id);
        }

        public void InsertDiscount(Discount discount)
        {
            _dbEntities.Discounts.Add(discount);
        }

        public void UpdateDiscount(Discount discount)
        {
            var item = _dbEntities.Discounts.First(x => x.Id == discount.Id);
            item.Type = discount.Type;
            item.Amount = discount.Amount;
            item.TargetType = discount.TargetType;
            item.TargetId = discount.TargetId;
        }

        public void DeleteDiscount(Discount discount)
        {
            _dbEntities.Discounts.Remove(discount);
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }
    }
}
