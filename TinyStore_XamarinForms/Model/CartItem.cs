using System;
using Realms;

namespace TinyStore_XamarinForms.Model
{
    public class CartItem : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
		public long ItemId { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public Double Price { get; set; }
		public string Image { get; set; }
        public Int32 Quantity { get; set; }

        public CartItem()
        {
        }

        public CartItem(Item item)
        {
            Quantity = 1;
            Name = item.Name;
            Category = item.Category;
            Description = item.Description;
            Image = item.Image;
            Price = item.Price;
            ItemId = item.Id;
        }

        public CartItem(Item item, Int32 quantity) : this(item)
		{
            Quantity = quantity;
		}
    }
}
