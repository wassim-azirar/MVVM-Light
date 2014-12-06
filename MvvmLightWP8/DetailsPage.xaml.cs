using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using MvvmLightWP8.ViewModels;

namespace MvvmLightWP8
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();

            Unloaded += PageUnloaded;
        }

        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
            ((DetailsViewModel)DataContext).Unload();
        }
    }
}