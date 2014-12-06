using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmLightWP8.ViewModels;

namespace MvvmLightWP8
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public static void CleanUp()
        {
            
        }
    }
}
