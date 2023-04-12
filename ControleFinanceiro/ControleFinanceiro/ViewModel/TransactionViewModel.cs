using ControleFinanceiro.Enums;
using System.Text;

namespace ControleFinanceiro.ViewModel
{
    public class TransactionViewModel : BaseViewModel
    {
        string entryName;
        DateTimeOffset datePickerDate;
        string entryValue;
        public string EntryName 
        {
            get => entryName;
            set 
            {
                if (entryName == value)
                    return;
                entryName = value;
                IsValidName(entryName);
            } 
        }
        public DateTimeOffset DatePickerDate {
            get => datePickerDate;
            set
            {
                if (datePickerDate == value)
                    return;
                datePickerDate = value;
                OnPropertyChanged(nameof(DatePickerDate));
            }
        }
        public string EntryValue
        {
            get => entryValue;
            set
            {
                if (entryValue == value)
                    return;
                entryValue = value;
                IsValidValue(entryValue);
            }
        }

        public int Id { get; set; }
        public double Value { get; set; }
        public TransactionType Type { get; set; }

        #region validations
        private void IsValidName(string name)
        {
            bool valid = true;
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
            {
                sb.AppendLine("O campo 'Nome' deve ser preenchido!");
                valid = false;
            }

            if (valid)
                OnPropertyChanged(nameof(EntryName));
            else
            {
                ReturnMessage(sb.ToString());
            }
        }

        private void IsValidValue(string value)
        {
            bool valid = true;
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
            {
                sb.AppendLine("O campo 'Valor' deve ser preenchido!");
                valid = false;
            }
            double result;
            if (!String.IsNullOrEmpty(value) && !double.TryParse(value, out result))
            {
                sb.AppendLine("O campo 'Valor' é inválido!");
                valid = false;
            }

            if (valid)
                OnPropertyChanged(nameof(EntryValue));
            else
            {
                ReturnMessage(sb.ToString());
            }
        }

        private void ReturnMessage(string msg)
        {
            Application.Current.MainPage.DisplayAlert("Error!", msg, "OK");
        }
        #endregion
    }
}
