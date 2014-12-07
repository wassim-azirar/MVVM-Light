using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;

namespace MvvmLightWP8.Services
{
    public class NavigationService : INavigationService
    {
        private const string QUERY_URI_KEY = "userstate";
        private static Dictionary<string, object> _userStates;
        private PhoneApplicationFrame _mainFrame;

        public void GoBack()
        {
            if (EnsureMainFrame() && _mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }

        public void NavigateTo(Uri pageUri)
        {
            if (EnsureMainFrame())
            {
                _mainFrame.Navigate(pageUri);
            }
        }

        public void NavigateTo(Uri pageUri, object state)
        {
            if (EnsureMainFrame())
            {
                Uri newUri;

                lock (_userStates)
                {
                    var id = Guid.NewGuid().ToString();
                    _userStates.Add(id, state);

                    if (pageUri.OriginalString.IndexOf("?", StringComparison.Ordinal) < 0)
                    {
                        newUri = new Uri(string.Format(
                                            "{0}?{1}={2}",
                                            pageUri.OriginalString,
                                            QUERY_URI_KEY,
                                            id),
                                        pageUri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
                    }
                    else
                    {
                        newUri = new Uri(string.Format(
                                            "{0}&{1}={2}",
                                            pageUri.OriginalString,
                                            QUERY_URI_KEY,
                                            id),
                                        pageUri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
                    }
                }

                NavigateTo(newUri);
            }
        }

        public static object GetAndRemoveState(IDictionary<string, string> query)
        {
            lock (_userStates)
            {
                if (query.ContainsKey(QUERY_URI_KEY) && _userStates.ContainsKey(query[QUERY_URI_KEY]))
                {
                    var state = _userStates[query[QUERY_URI_KEY]];
                    _userStates.Remove(query[QUERY_URI_KEY]);
                    return state;
                }

                return null;
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            _userStates = new Dictionary<string, object>();

            return _mainFrame != null;
        }
    }
}
