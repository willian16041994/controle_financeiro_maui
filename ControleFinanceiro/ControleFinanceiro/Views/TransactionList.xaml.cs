using CommunityToolkit.Mvvm.Messaging;
using ControleFinanceiro.Services;
using ControleFinanceiro.ViewModel;

namespace ControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
    private ITransactionService _service;
    public TransactionList(ITransactionService service)
    {
        _service = service;
        InitializeComponent();
        Reload();

        //Esse aqui ele s� espera e roda quando um cadastro � acionado...
        //integra essa a��o com a tela de TransactionAdd
        WeakReferenceMessenger.Default.Register<string>(
            this,
            (e, msg) =>
            {
                Reload();
            });
    }

    private void Reload()
    {
        var items = _service.GetAll();
        CollectionTransactions.ItemsSource = items;
        double income = items.Where(x => x.Type == Enums.TransactionType.Income).Sum(x => x.Value);
        double expense = items.Where(x => x.Type == Enums.TransactionType.Expenses).Sum(x => x.Value);
        double balance = (income - expense);
        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");
    }

    public void GoTransactionAdd(object sender, EventArgs args)
    {
        var _transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(_transactionAdd);
    }
    
    private void GoToDraw(object sender, EventArgs e)
    {
        var _transactionToDraw = Handler.MauiContext.Services.GetService<TransactionDraw>();
        Navigation.PushModalAsync(_transactionToDraw);
    }

    private void Tapped_To_EditView(object sender, TappedEventArgs e)
    {
        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers.FirstOrDefault();
        TransactionViewModel vm = (TransactionViewModel)gesture.CommandParameter;
        var _transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        _transactionEdit.SetTransactionToEdit(vm);
        Navigation.PushModalAsync(_transactionEdit);
    }

    private async void Delete_File(object sender, TappedEventArgs e)
    {
        await AnimationBorder((Border)sender, true);
        bool isDelete = await  App.Current.MainPage.DisplayAlert("Excluir", "Voc� deseja excluir esse lan�amento?", "Sim", "N�o");

        if (isDelete)
        {
            TransactionViewModel transactionVM = (TransactionViewModel)e.Parameter;
             _service.Delete(transactionVM);
            Reload();
        }
        else
        {
            await AnimationBorder((Border)sender, false);
        }
    }

    private Color _borderOriginalBackGroundColor;
    private string _labelOriginalTexte;
    private async Task AnimationBorder(Border border ,bool isDelete)
    {
        //um elemento dentro da label � contente se for v�rios � children
        var label = (Label)border.Content;
        if (isDelete)
        {
            _borderOriginalBackGroundColor = border.BackgroundColor;
            _labelOriginalTexte = label.Text;
           
            await border.RotateYTo(90, 500);
            border.BackgroundColor = Colors.Red;
           
            //mudando layout aima��o da bolinha de excluir
            label.Text = "X";
            label.TextColor = Colors.White;
            await border.RotateYTo(180, 500);
        }
        else
        {
            await border.RotateYTo(90, 500);
            border.BackgroundColor = _borderOriginalBackGroundColor;
            label.TextColor= Colors.Black;
            label.Text = _labelOriginalTexte;
            await border.RotateYTo(0, 500);
        }
    }
}