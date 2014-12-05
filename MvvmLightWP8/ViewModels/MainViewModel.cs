using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmLightWP8.DelagateCommand;
using MvvmLightWP8.Models;
using MvvmLightWP8.Services;

namespace MvvmLightWP8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Friend> Friends
        {
            get;
            private set;
        }

        private Friend _selectedFriend;

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }

            set
            {
                if (_selectedFriend == value)
                {
                    return;
                }

                _selectedFriend = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Ctor

        public MainViewModel() : this(new DataService(), new NavigationService(), new DialogService())
        {
            Friends = new ObservableCollection<Friend>();
        }

        public MainViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            Friends = new ObservableCollection<Friend>();
        }

        #endregion

        #region Services

        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        #endregion

        #region DelegateCommand

        private DelegateCommand _getFriendsCommand;

        public DelegateCommand GetFriendsCommand
        {
            get { return _getFriendsCommand ?? (_getFriendsCommand = new DelegateCommand(GetFriendsCommandExecute)); }
        }

        private DelegateCommand<Friend> _showDetailsCommand;

        public DelegateCommand<Friend> ShowDetailsCommand
        {
            get { return _showDetailsCommand ?? (_showDetailsCommand = new DelegateCommand<Friend>(ShowDetailsCommandExecute)); }
        }

        #endregion

        #region Methods

        private async void GetFriendsCommandExecute()
        {
            Friends.Clear();

            var friends = await _dataService.GetFriends();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        private void ShowDetailsCommandExecute(object friend)
        {
            SelectedFriend = (Friend) friend;
            _navigationService.NavigateTo(new Uri("/DetailsPage.xaml", UriKind.Relative));
        }

        #endregion
    }
}