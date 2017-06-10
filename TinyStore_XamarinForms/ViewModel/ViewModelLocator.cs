/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TinyStore_XamarinForms"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/
using Xamarin.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using TinyStore_XamarinForms.Service;
using TinyStore_XamarinForms.Model;

namespace TinyStore_XamarinForms.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ICatalogService, CatalogService>();

			SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CatalogViewModel>();
			SimpleIoc.Default.Register<CartViewModel>();
            SimpleIoc.Default.Register<ItemViewModel>();

            //SimpleIoc.Default.Register<INavigation>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

		public CatalogViewModel Catalog
		{
			get
			{
				return ServiceLocator.Current.GetInstance<CatalogViewModel>();
			}
		}

		public ItemViewModel Item
		{
			get
			{
				return ServiceLocator.Current.GetInstance<ItemViewModel>();
			}
		}

		public CartViewModel Cart
		{
			get
			{
                return ServiceLocator.Current.GetInstance<CartViewModel>();
			}
		}
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}