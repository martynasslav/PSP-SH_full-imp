using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbEntities _dbEntities;

        public PaymentRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _dbEntities.Payments;
        }

        public Payment GetPayment(string id)
        {
            return _dbEntities.Payments.Find(id);
        }

        public void CreatePayment(Payment payment)
        {
            _dbEntities.Payments.Add(payment);
            _dbEntities.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            var _payment = _dbEntities.Payments.Find(payment.Id);
            _payment.Id = payment.Id;
            _payment.FinancialDocuments = payment.FinancialDocuments;
            _payment.OrderId = payment.OrderId;
            _payment.CompletionDate = payment.CompletionDate;
            _payment.CustomerId = payment.CustomerId;
            _payment.Type = payment.Type;
            _dbEntities.Payments.Update(_payment);
            _dbEntities.SaveChanges();
        }

        public void DeletePayment(Payment payment)
        {
            _dbEntities.Payments.Remove(payment);
            _dbEntities.SaveChanges();
        }
    }
}
