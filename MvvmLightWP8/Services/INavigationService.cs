using System;

namespace MvvmLightWP8.Services
{
    public interface INavigationService
    {
        void NavigateTo(Uri uri);

        void NavigateTo(Uri uri, object state);

        void GoBack();

    }
}
