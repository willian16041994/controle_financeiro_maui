using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ControleFinanceiro.ViewModel
{
    public partial class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy;
        string title;

        public bool IsBusy
        {
            get => isBusy;

            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !IsBusy;

        public string Title
        {
            get => title;

            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string v = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}
