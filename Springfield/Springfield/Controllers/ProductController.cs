using Springfield.DBModels;
using System;
using System.Web.Mvc;

namespace Springfield.Controllers
{
    public class ProductController : Controller
    {
        private MyContext db = new DBModels.MyContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(Guid id)
        {
            var item = db.Items.Find(id);

            return View(item);
        }
    }
}