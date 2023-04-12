using ControleFinanceiro.ViewModel;
using System.Globalization;
 
namespace ControleFinanceiro.Libraries.Converters
{
    public class TransactionValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          TransactionViewModel transaction = (TransactionViewModel)value;
            if (transaction == null)
            {
                return "";
            }
            if(transaction.Type == Enums.TransactionType.Income)
            {
                return transaction.Value.ToString("C");
            }
            else
            {
                return $"- {transaction.Value.ToString("C")}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
