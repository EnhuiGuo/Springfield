using Springfield.DBModels;
using Springfield.Models.ItemModel;
using Springfield.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Springfield.Controllers
{
    public class HomeController : Controller
    {
        private MyContext db = new DBModels.MyContext();
        public ActionResult Index()
        {
            var modelList = new List<ItemViewModel>();
            try
            {
                var items = db.Items.Where(x=>x.OnSell).ToList().OrderByDescending(x=>x.PostDate);

                foreach (var item in items)
                {
                    var model = new ItemViewModel(item);
                    var selectItems = ItemRepository.GetSelectCatalogue();
                    var allItem = new SelectListItem();
                    {
                        allItem.Text = "全部";
                        allItem.Value = "";
                    }
                    selectItems.Insert(0, allItem);
                    ViewBag.Catalogue = selectItems;
                    modelList.Add(model);
                }
            }
            catch
            {

            }
            return View(modelList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}