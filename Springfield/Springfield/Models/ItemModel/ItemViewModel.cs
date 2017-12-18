using Springfield.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Springfield.Models.ItemModel
{
    public class ItemViewModel
    {
        public ItemViewModel() { }

        public ItemViewModel(Item item)
        {
            this.Id = item.Id;
            this.UserId = item.UserId;
            //this.Name = item.Name;
            this.Price = item.Price;
            //this.Quantity = item.Quantity;
            this.Description = item.Description;
            this.UserName = item.User.UserName;
            this.CanDelivery = item.CanDelivery;
            this.OnSell = item.OnSell;
            this.QQ = item.QQ;
            this.WeChat = item.WeChat;
            this.Phone = item.Phone;
            this.Catalog = item.Catalog.Name;
            this.CatalogId = item.CatalogId;
            

            if (item.ItemImages != null && item.ItemImages.Count > 0)
                this.ProfileImagePath = item.ItemImages.FirstOrDefault().Path;
            else
                this.ProfileImagePath = "/Imgs/diaochan.jpg";
        }

        public Guid Id { get; set; }
        public string UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "名字不能为空。")]
        //[Display(Name = "产品名称")]
        //public string Name { get; set; }
        [Required(ErrorMessage = "价格不能为空。")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0, 1000000, ErrorMessage = "不能大于1000000")]
        [Display(Name = "产品价格")]
        public float Price { get; set; }
        //[Required(ErrorMessage = "数量不能为空。")]
        //[Display(Name = "数量")]
        //public int Quantity { get; set; }
        [Display(Name = "产品描述")]
        public string Description { get; set; }
        public string ProfileImagePath { get; set; }
        [Display(Name = "可送货?")]
        public bool CanDelivery { get; set; }
        public bool OnSell { get; set; }
        public string WeChat { get; set; }
        public string QQ { get; set; }
        public string Phone { get; set; }
        public string Catalog { get; set; }
        [Display(Name = "类别")]
        [Required(ErrorMessage = "类别不能为空。")]
        public Guid CatalogId { get; set; }
        public IEnumerable<SelectListItem> Catalogue { get; set; }
    }
}