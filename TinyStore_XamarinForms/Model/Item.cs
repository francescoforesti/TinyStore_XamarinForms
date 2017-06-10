using System;

namespace TinyStore_XamarinForms.Model
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Double Price { get; set; }
        public string Image { get; set; }


        public Item()
        {
        }
    }
}
