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
    [Authorize(Roles ="User")]
    public class CustomersController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Customers
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var customers = db.Customers.Where(c => c.CompanyId == user.CompanyId).Include(c => c.City).Include(c => c.Departament);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name");
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var customer = new Customer { CompanyId = user.CompanyId, };
            return View(customer);
        }

        // POST: Customers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                var response = DBHelper.SaveChanges(db);
                if(response.Succeeded)
                {
                    UsersHelper.CreateUserASP(customer.UserName, "Customer");
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartamentId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", customer.DepartamentId);

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartamentId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", customer.DepartamentId);

            return View(customer);
        }

        // POST: Customers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //UsersHelper.CreateUserASP(customer.UserName, "Customer");
                    //TODO: validar el cambio del email por cliente
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);

            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartamentId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", customer.DepartamentId);

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                UsersHelper.DeleteUser(customer.UserName);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, response.Message);
            return View(customer);
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
