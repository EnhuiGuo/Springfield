using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Springfield.Models.ItemModel
{
    public class ItemCreateModel
    {
        //[Required(ErrorMessage = "名字不能为空。")]
        //[Display(Name = "产品名称")]
        //public string Name { get; set; }

        public ItemCreateModel() {
            this.Catalogue = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "价格不能为空。")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0, 1000000, ErrorMessage = "不能大于1000000")]
        [Display(Name = "产品价格")]
        public float Price { get; set; }

        //[Required(ErrorMessage = "数量不能为空。")]
        //[Display(Name = "数量")]
        //public int Quantity { get; set; }
        [Required(ErrorMessage = "不能为空。")]
        [Display(Name = "产品描述")]
        public string Description { get; set; }
        [Display(Name = "可送货?")]
        public bool CanDelivery { get; set; }
        public string WeChat { get; set; }
        public string QQ { get; set; }
        public string Phone { get; set; }
        [Display(Name = "类别")]
        [Required(ErrorMessage = "类别不能为空。")]
        public Guid CatalogId { get; set; }
        public IEnumerable<SelectListItem> Catalogue { get; set; }

    }
}