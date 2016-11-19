using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Classes;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "User")]
    public class WareHousesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: WareHouses
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var wareHouses = db.WareHouses.Where(w => w.CompanyId == user.CompanyId).Include(w => w.City).Include(w => w.Departament);
            return View(wareHouses.ToList());
        }

        // GET: WareHouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wareHouse = db.WareHouses.Find(id);
            if (wareHouse == null)
            {
                return HttpNotFound();
            }
            return View(wareHouse);
        }

        // GET: WareHouses/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name");
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var warehouse = new WareHouse { CompanyId = user.CompanyId, };
            return View(warehouse);
        }

        // POST: WareHouses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                db.WareHouses.Add(wareHouse);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo valor");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(wareHouse.DepartamentId), "CityId", "Name", wareHouse.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", wareHouse.DepartamentId);
            return View(wareHouse);
        }

        // GET: WareHouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wareHouse = db.WareHouses.Find(id);
            if (wareHouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(wareHouse.DepartamentId), "CityId", "Name", wareHouse.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", wareHouse.DepartamentId);
            return View(wareHouse);
        }

        // POST: WareHouses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wareHouse).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo valor");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(wareHouse.DepartamentId), "CityId", "Name", wareHouse.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", wareHouse.DepartamentId);
            return View(wareHouse);
        }

        // GET: WareHouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wareHouse = db.WareHouses.Find(id);
            if (wareHouse == null)
            {
                return HttpNotFound();
            }
            return View(wareHouse);
        }

        // POST: WareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WareHouse wareHouse = db.WareHouses.Find(id);
            db.WareHouses.Remove(wareHouse);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, response.Message);
            return View(wareHouse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
