using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using MvvmLightWP8.ViewModels;

namespace MvvmLightWP8
{
    public partial class DetailsPage
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