using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmLightWP8.Helpers;

namespace MvvmLightWP8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.Equals(_name, value))
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand _loadCommand;

        public DelegateCommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new DelegateCommand(LoadCommandExecute)); }
        }

        private void LoadCommandExecute()
        {
            Name = "Wassim AZIRAR";
        }
    }
}