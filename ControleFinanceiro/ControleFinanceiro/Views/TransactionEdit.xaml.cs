using CommunityToolkit.Mvvm.Messaging;
using ControleFinanceiro.Services;
using ControleFinanceiro.Utils;
using ControleFinanceiro.ViewModel;
using System.Text;

namespace ControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
    private TransactionViewModel _transaction;
    private ITransactionService _service;
	public TransactionEdit(ITransactionService service, TransactionViewModel vm)
	{
        _service = service;
        InitializeComponent();
        BindingContext = vm;
	}

    private void CloseView(object sender, TappedEventArgs e)
    {
        CloseKeyboard.HideKeyboardAndroid();
        Navigation.PopModalAsync();
    }

	public void SetTransactionToEdit(TransactionViewModel transaction)
	{
        _transaction = transaction;
		if(transaction.Type == Enums.TransactionType.Income)
			RadioIncome.IsChecked = true;
		else
			RadioExpense.IsChecked = true;

		EntryName.Text = transaction.EntryName;
		DatePickerDate.Date = transaction.DatePickerDate.Date;
        EntryValue.Text = transaction.EntryValue;
    }

    private void Button_Clicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
            return;
        SaveTransactionDatabase();
        CloseKeyboard.HideKeyboardAndroid();
        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private void SaveTransactionDatabase()
    {
        TransactionViewModel vm = new TransactionViewModel()
        {
            Id = _transaction.Id,
            EntryName = EntryName.Text,
            Type = (RadioIncome.IsChecked) ? Enums.TransactionType.Income : Enums.TransactionType.Expenses,
            DatePickerDate = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text),
            EntryValue = EntryValue.Text
        };
        _service.Update(vm);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder sb = new StringBuilder();
        if (String.IsNullOrEmpty(EntryName.Text) || String.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
        }
        if (String.IsNullOrEmpty(EntryValue.Text) || String.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido!");
            valid = false;
        }
        double result;
        if (!String.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }

        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
        }
        return valid;
    }
}