using ControleFinanceiro.Repositories;
using ControleFinanceiro.ViewModel;
using ControleFinanceiro.ViewModel.Mappings;

namespace ControleFinanceiro.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _trRepository;

        public TransactionService(ITransactionRepository trRepository)
        {
            this._trRepository = trRepository;
        }

        public List<TransactionViewModel> GetAll()
        {
            var items = _trRepository.GetAll();
            var itensVM = new Mapper().MapperListTransactionToViewModel(items);
            return itensVM;
        }

        public void Add(TransactionViewModel vm)
        {
            var transaction = new Mapper().MapperViewModelToTransaction(vm);
            _trRepository.Add(transaction);
        }

        public void Delete(TransactionViewModel transactionVM)
        {
            var transaction = new Mapper().MapperViewModelToTransaction(transactionVM);
            _trRepository.Delete(transaction);
        }
        public void Update(TransactionViewModel vm)
        {
            var transaction = new Mapper().MapperViewModelToTransaction(vm);
            _trRepository.Update(transaction);
        }
    }
}
