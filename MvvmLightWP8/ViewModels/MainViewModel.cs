using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLightWP8.Models;
using MvvmLightWP8.Services;

namespace MvvmLightWP8.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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

        public MainViewModel() : 
            this(DesignerProperties.IsInDesignTool ? (IDataService)new Design.DesignFriendsService() : new DataService(), 
                 new NavigationService(), 
                 new DialogService())
        {
            Friends = new ObservableCollection<Friend>();
            if (IsInDesignMode || DesignerProperties.IsInDesignTool)
            {
                GetFriendsCommandExecute();
                SelectedFriend = Friends[0];
            }
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

        #region RelayCommands

        private RelayCommand _getFriendsCommand;
        private RelayCommand<Friend> _showDetailsCommand;
        

        public RelayCommand GetFriendsCommand
        {
            get { return _getFriendsCommand ?? (_getFriendsCommand = new RelayCommand(GetFriendsCommandExecute)); }
        }

        public RelayCommand<Friend> ShowDetailsCommand
        {
            get { return _showDetailsCommand ?? (_showDetailsCommand = new RelayCommand<Friend>(ShowDetailsCommandExecute)); }
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
            //SelectedFriend = (Friend) friend;

            Messenger.Default.Send((Friend)friend);
            _navigationService.NavigateTo(new Uri("/DetailsPage.xaml", UriKind.Relative));
        }

        #endregion
    }
}