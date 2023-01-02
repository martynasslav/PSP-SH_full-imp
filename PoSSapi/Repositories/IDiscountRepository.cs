using Classes;

namespace PoSSapi.Repositories
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> GetDiscounts();
        Discount? GetDiscountById(string id);
        void InsertDiscount(Discount discount);
        void UpdateDiscount(Discount discount);
        void DeleteDiscount(Discount discount);
        void Save();
    }
}
