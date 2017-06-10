using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using TinyStore_XamarinForms.Model;
using TinyStore_XamarinForms.ViewModel;

namespace TinyStore_XamarinForms.View
{
    public partial class CatalogPage : ContentPage
    {
        CatalogViewModel _viewModel;

        public CatalogPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as CatalogViewModel;
            _viewModel.Navigation = Navigation;
        }

        public void OnSelectItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Item;
            if(item != null)
            {
				_viewModel.Navigation.PushAsync(new ItemDetailPage(item.Id));
				CatalogList.SelectedItem = null;
            }
        }

        void OnCategoryChanged(object sender, System.EventArgs e)
        {
            var picker = sender as Picker;
            Task.Run(() => _viewModel.loadItems(picker.SelectedItem as string));
        }
    }

}
