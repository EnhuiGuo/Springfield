using Springfield.DBModels;
using System;

namespace Springfield.Models.ItemModel
{
    public class ItemImageViewModel
    {

        public ItemImageViewModel() { }

        public ItemImageViewModel(ItemImage img)
        {
            this.Id = img.Id;
            this.ItemId = img.ItemId;
            this.Path = img.Path;

        }

        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Path { get; set; }
    }
}