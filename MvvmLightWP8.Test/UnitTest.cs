using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MvvmLightWP8.Models;
using MvvmLightWP8.Services;
using MvvmLightWP8.ViewModels;

namespace MvvmLightWP8.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestShowingErrorWhenSaving()
        {
            var dialogService = new TestDialogService();

            var vm = new MainViewModel(new TestDataService(), new TestNavigationService(), dialogService);

            Assert.IsNull(dialogService.MessageShown);

            vm.SaveFriendCommand.Execute(new Friend { Id = 1 });

            Assert.AreEqual(TestDataService.ErrorMessage, dialogService.MessageShown);
        }
    }

    public class TestDataService : IDataService
    {
        public const string ErrorMessage = "This is a test error message";

        public Task<IEnumerable<Friend>> GetFriends()
        {
            return Task.FromResult(Enumerable.Empty<Friend>());
        }

        public Task<string> Save(Friend updatedFriend)
        {
            throw new Exception(ErrorMessage);
        }

    }

    public class TestNavigationService : INavigationService
    {
        public void GoBack()
        {
        }

        public void NavigateTo(Uri uri)
        {
        }
    }

    public class TestDialogService : IDialogService
    {
        public string MessageShown
        {
            get;
            private set;
        }

        public void ShowMessage(string message)
        {
            MessageShown = message;
        }
    }
}
