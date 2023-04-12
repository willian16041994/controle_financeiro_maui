using ControleFinanceiro.ViewModel;

namespace ControleFinanceiro.Services
{
    public interface ITransactionService
    {
        List<TransactionViewModel> GetAll();
        void Add(TransactionViewModel transaction);
        void Delete(TransactionViewModel transaction);
        void Update(TransactionViewModel transaction);
    }
}
