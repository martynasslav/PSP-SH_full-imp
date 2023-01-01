using Classes;

namespace PoSSapi.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        Payment GetPayment(string id);
        void CreatePayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(Payment payment);
    }
}
