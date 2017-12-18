using Springfield.DBModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Springfield.Repository
{
    public class ItemRepository
    {
        private static MyContext db = new MyContext();
        public static List<SelectListItem> GetSelectCatalogue()
        {
            var catalogue = db.Catalogue.ToList();

            var selectList = new List<SelectListItem>();

            if (catalogue != null)
            {
                foreach (var catalog in catalogue)
                {
                    var list = new SelectListItem();
                    {
                        list.Text = catalog.Name;
                        list.Value = catalog.Id.ToString();
                    }
                    selectList.Add(list);
                }
            }

            return selectList;
        }
    }
}