using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Springfield.DBModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Springfield.Models.ItemModel;
using System.Net;
using System.IO;
using Springfield.Repository;

namespace Springfield.Controllers
{
    [Authorize(Roles = "Manager,Seller")]
    public class ItemController : Controller
    {
        private MyContext db = new MyContext();
        // GET: Item
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var items = db.Items.Where(x=>x.UserId == userId).ToList();

            var model = new List<ItemViewModel>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    var itemView = new ItemViewModel(item);
                    model.Add(itemView);
                }
            }

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.UserId = User.Identity.GetUserId();

            var model = new ItemCreateModel();

            model.Catalogue = ItemRepository.GetSelectCatalogue();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Price,Description,CanDelivery,QQ,Phone,WeChat,CatalogId")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.UserId = User.Identity.GetUserId();
                item.PostDate = DateTime.Now;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = User.Identity.GetUserId();
            return View(item);
        }

        public ActionResult Detail(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ItemViewModel();
            var item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                model = new ItemViewModel(item);
            }
            return View(model);
        }

        public JsonResult AddImage(Guid Id)
        {
            var model = new ItemImageViewModel();

            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + fileContent.FileName;
                        var path = Path.Combine(Server.MapPath("~/imgs/"), fileName);
                        //var path = Path.Combine("http://www.liar114.com/imgs/", fileName);

                        fileContent.SaveAs(path);

                        var itemImage = new ItemImage();
                        {
                            itemImage.ItemId = Id;
                            itemImage.Path = Path.Combine("http://www.liar114.com/imgs/", fileName);
                        }

                        var img = db.ItemImages.Add(itemImage);

                        db.SaveChanges();

                        model = new ItemImageViewModel(img);
                    }
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {

                return Json(e.InnerException);
            }
        }

        public JsonResult GetItemImages(Guid itemId)
        {
            var imgModels = new List<ItemImageViewModel>();

            try
            {
                var imgs = db.ItemImages.Where(x => x.ItemId == itemId).ToList();

                if (imgs != null && imgs.Count > 0)
                {
                    foreach (var img in imgs)
                    {
                        var imgModel = new ItemImageViewModel(img);
                        imgModels.Add(imgModel);
                    }
                }
            }
            catch
            {

            }

            return Json(imgModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteImage(Guid id)
        {
            try
            {
                var img = db.ItemImages.Find(id);
                if (img != null)
                {
                    db.ItemImages.Remove(img);
                    db.SaveChanges();

                    var path = img.Path;

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult _Edit(ItemViewModel model)
        {
            model.Catalogue = ItemRepository.GetSelectCatalogue();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(ItemEditModel model)
        {
            try
            {
                var item = db.Items.Find(model.Id);
                var updateItem = new Item(model);
                updateItem.PostDate = item.PostDate;
                updateItem.OnSell = item.OnSell;
                db.Entry(item).CurrentValues.SetValues(updateItem);
                db.SaveChanges();

            }
            catch(Exception e)
            {
                var s = e;
            }
            return RedirectToAction("Detail", new { id = model.Id });
        }

        [HttpPost]
        public JsonResult DeleteItem(Guid id)
        {
            try
            {
                var item = db.Items.Find(id);

                var itemImages = item.ItemImages.ToList();

                db.Items.Remove(item);

                db.SaveChanges();

                foreach (var image in itemImages)
                {
                    var path = image.Path;

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                return Json("true");
            }
            catch
            {
                return Json("false");
            }
        }

        [HttpPost]
        public JsonResult SetOnSell(Guid id, bool onSell)
        {
            try
            {
                var item = db.Items.Find(id);
                item.OnSell = onSell;

                db.SaveChanges();

                return Json("true");
            }
            catch
            {
                return Json("false");
            }
        }

        //public List<SelectListItem> GetSelectCatalogue()
        //{
        //    var catalogue = db.Catalogue.ToList();

        //    var selectList = new List<SelectListItem>();

        //    if (catalogue != null)
        //    {
        //        foreach (var catalog in catalogue)
        //        {
        //            var list = new SelectListItem();
        //            {
        //                list.Text = catalog.Name;
        //                list.Value = catalog.Id.ToString();
        //            }
        //            selectList.Add(list);
        //        }
        //    }

        //    return selectList;
        //}
    }
}