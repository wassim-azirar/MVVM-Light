using System;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLightWP8.Models;
using MvvmLightWP8.Services;

namespace MvvmLightWP8.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        #region Properties

        private Friend _friend;
        
        public Friend Friend
        {
            get { return _friend; }
            set
            {
                if (_friend == value)
                {
                    return;
                }

                _friend = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Services

        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService; 

        #endregion

        #region Ctor

        public DetailsViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;

#if DEBUG
            if (IsInDesignMode || DesignerProperties.IsInDesignTool)
            {
                GetFriendOnDesignTime();
            }
#endif
            else
            {
                Messenger.Default.Register<Friend>(
                    this,
                    friend =>
                    {
                        Friend = friend;
                    });
            }
        }

        #endregion

        #region RelayCommands

        private RelayCommand _saveFriendCommand;

        public RelayCommand SaveFriendCommand
        {
            get { return _saveFriendCommand ?? (_saveFriendCommand = new RelayCommand(SaveFriendCommandExecute)); }
        }

        #endregion

        #region Methods

        private async void SaveFriendCommandExecute()
        {
            try
            {
                var service = _dataService;
                var result = await service.Save(Friend);

                var id = int.Parse(result);

                if (id > 0)
                {
                    Friend.Id = id;
                }
                else
                {
                    _dialogService.ShowMessage("Error");
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        private async void GetFriendOnDesignTime()
        {
            var friends = await _dataService.GetFriends();

            if (friends != null)
            {
                Friend = friends.FirstOrDefault();
            }
        }

        #endregion
    }
}
