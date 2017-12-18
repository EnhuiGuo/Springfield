using System;

namespace Springfield.Models.ItemModel
{
    public class ItemEditModel : ItemCreateModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        //public Guid CatalogId { get; set; }
    }
}