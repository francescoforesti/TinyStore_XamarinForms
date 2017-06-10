using System.Collections.ObjectModel;
using TinyStore_XamarinForms.Model;
using TinyStore_XamarinForms.Service;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyStore_XamarinForms.ViewModel
{
    public class CatalogViewModel : BaseViewModel
    {
        private static readonly string DEFAULT_CATEGORY = "All";
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();

		private ICatalogService service;

		public CatalogViewModel()
        {
            
            this.service = SimpleIoc.Default.GetInstance<ICatalogService>();

            if (service != null)
            {
                Task.Run(() => this.loadItems()).Wait();
                Task.Run(() => this.loadCategories()).Wait();
            }
        }

        public async Task loadItems(string filter = null)
        {
            Items.Clear();
            List<Item> items = await service.FindItems(ValidCategory(filter) ? filter : null);
            foreach(Item i in items)
            {
                Items.Add(i);
            }
        }

        private bool ValidCategory(string category)
        {
            return !(string.IsNullOrEmpty(category) || DEFAULT_CATEGORY == category);
        }
       
        private async Task loadCategories()
        {
            List<string> categories = await service.FindCategories();
            Categories.Clear();
            Categories.Add(DEFAULT_CATEGORY);
            foreach(string cat in categories)
            {
                Categories.Add(cat);
            }
        }

    }
}
