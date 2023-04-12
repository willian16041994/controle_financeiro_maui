using ControleFinanceiro.Models;

namespace ControleFinanceiro.ViewModel.Mappings
{
    public class Mapper
    {
        public TransactionViewModel MapperTransactionToViewModel(Transaction transaction)
        {
            TransactionViewModel vm = new TransactionViewModel
            {
                Id = transaction.Id,
                Value = transaction.Value,
                EntryValue = transaction.Value.ToString(),
                EntryName = transaction.Name,
                DatePickerDate = transaction.Date,
                Type = transaction.Type
            };
            return vm;
        }

        public List<TransactionViewModel> MapperListTransactionToViewModel(IEnumerable<Transaction> transactions)
        {
            List<TransactionViewModel> vms = new ();
            foreach (var item in transactions)
            {
                vms.Add(MapperTransactionToViewModel(item));
            }
            return vms; 
        }

        public Transaction MapperViewModelToTransaction(TransactionViewModel vm)
        {
            Transaction model = new Transaction
            {
                Id = vm.Id,
                Value = Convert.ToDouble(vm.EntryValue),
                Name = vm.EntryName,
                Date = vm.DatePickerDate,
                Type = vm.Type
            };
            return model;
        }
    }
}
