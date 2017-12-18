using Springfield.Models;
using Springfield.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Springfield.DBModels
{
    [Table("Items")]
    public class Item
    {

        public Item() { }

        public Item(ItemEditModel model)
        {
            this.Id = model.Id;
            this.UserId = model.UserId;
            this.Price = model.Price;
            //this.Quantity = model.Quantity;
            this.Description = model.Description;
            this.CanDelivery = model.CanDelivery;
            this.QQ = model.QQ;
            this.Phone = model.Phone;
            this.WeChat = model.WeChat;
            this.CatalogId = model.CatalogId;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        //public string Name { get; set; }
        public float Price { get; set; }
        //public int Quantity { get; set; }
        public string Description { get; set; }
        public bool CanDelivery { get; set; }
        public bool OnSell { get; set; }
        public DateTime PostDate { get; set; }
        public string WeChat { get; set; }
        public string QQ { get; set; }
        public string Phone { get; set; }
        public Guid CatalogId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("CatalogId")]
        public virtual Catalog Catalog { get; set; }


        public virtual ICollection<ItemImage> ItemImages { get; set; }
    }
}