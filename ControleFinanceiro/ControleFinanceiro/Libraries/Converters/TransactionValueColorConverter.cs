using System.Globalization;
using ControleFinanceiro.ViewModel;

namespace ControleFinanceiro.Libraries.Converters
{
    public class TransactionValueColorConverter : IValueConverter
    {
        /// <summary>
        /// Troca as cores por tipo de transação, despesas é vermelho e receitas em verde
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransactionViewModel transaction = (TransactionViewModel) value;
            if (transaction == null)
            {
                return Colors.Black;
            }
            if (transaction.Type == Enums.TransactionType.Income)
            {
                return Color.FromArgb("#FF559A90");
            }
            else
            {
                return Colors.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
