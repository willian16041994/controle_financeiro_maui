using System.Globalization;

namespace ControleFinanceiro.Libraries.Converters
{
    public class RandonColorFilesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.FromArgb("#FFFFFF");

            var random = new Random();
            var color = String.Format("#FF{0:x6}", random.Next(0x1000000));
            return Color.FromArgb(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
