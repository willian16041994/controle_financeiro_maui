using CommunityToolkit.Mvvm.Messaging;
using ControleFinanceiro.Services;
using ControleFinanceiro.Utils;
using ControleFinanceiro.ViewModel;
using System.Text;

namespace ControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionService _service;
    public TransactionAdd(ITransactionService service, TransactionViewModel vm)
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

    private void Button_Clicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
            return;

        SaveTransactionDatabase();
        CloseKeyboard.HideKeyboardAndroid();
        Navigation.PopModalAsync();
        WeakReferenceMessenger.Default.Send<string>(string.Empty);
        
        //var count = _repository.GetAll().Count;
        //App.Current.MainPage.DisplayAlert("Mensagem!", $"Existem {count} registros! salvos", "OK");
    }

    private void SaveTransactionDatabase()
    {
        TransactionViewModel vm = new TransactionViewModel()
        {
            EntryName = EntryName.Text,
            Type = (RadioIncome.IsChecked) ? Enums.TransactionType.Income : Enums.TransactionType.Expenses,
            DatePickerDate = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text),
            EntryValue = EntryValue.Text
        };
        _service.Add(vm);
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