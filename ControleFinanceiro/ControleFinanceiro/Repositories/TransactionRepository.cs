using ControleFinanceiro.Models;
using LiteDB;

namespace ControleFinanceiro.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LiteDatabase _database;
        //nome da minha coleção, o Bd é NoSql por isso se não colocar nome na coleção fica só os objetos.
        private readonly string collectionName = "transactions";
        public TransactionRepository(LiteDatabase database)
        {
            _database = database;
        }

        public List<Transaction> GetAll()
        {
            return _database
                .GetCollection<Transaction>(collectionName)
                .Query()
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public void Add(Transaction transaction)
        {
            var row = _database.GetCollection<Transaction>(collectionName);
            row.Insert(transaction);
            row.EnsureIndex(x => x.Date);
        }

        public void Delete(Transaction transaction) {
            var row = _database.GetCollection<Transaction>(collectionName);
            row.Delete(transaction.Id);
        }
        public void Update(Transaction transaction) {
            var row = _database.GetCollection<Transaction>(collectionName);
            row.Update(transaction);
        }
    }
}
