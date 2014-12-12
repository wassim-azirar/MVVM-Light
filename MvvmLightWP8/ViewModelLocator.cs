using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmLightWP8.Design;
using MvvmLightWP8.Services;
using MvvmLightWP8.ViewModels;

namespace MvvmLightWP8
{
    public class ViewModelLocator
    {
        public ViewModelLocator ()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register(() => new MainViewModel(DataService, NavigationService, DialogService));
            SimpleIoc.Default.Register(() => new DetailsViewModel(DataService, DialogService), true);
        }

        public MainViewModel MainViewModel
        {
            get { return SimpleIoc.Default.GetInstance<MainViewModel>(); }
        }

        public DetailsViewModel DetailsViewModel
        {
            get { return SimpleIoc.Default.GetInstance<DetailsViewModel>(); }
        }

        public INavigationService NavigationService
        {
            get { return SimpleIoc.Default.GetInstance<INavigationService>(); }
        }

        public IDialogService DialogService
        {
            get { return SimpleIoc.Default.GetInstance<IDialogService>(); }
        }

        public IDataService DataService
        {
            get { return SimpleIoc.Default.GetInstance<IDataService>(); }
        }
    }
}
