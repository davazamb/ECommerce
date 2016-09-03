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
            var qry = (from cu in db.Customers
                       join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                       join co in db.Companies on cc.CompanyId equals co.CompanyId
                       where co.CompanyId == user.CompanyId
                       select new { cu }).ToList();
            var customers = new List<Customer>();
            foreach (var item in qry)
            {
                customers.Add(item.cu);
            }
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
            return View();
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
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Customers.Add(customer);
                        var response = DBHelper.SaveChanges(db);
                        if (!response.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                            transaction.Rollback();
                            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartamentId), "CityId", "Name", customer.CityId);
                            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", customer.DepartamentId);
                            return View(customer);
                        }
                        UsersHelper.CreateUserASP(customer.UserName, "Customer");
                        var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                        var companyCustomer = new CompanyCustomer
                        {
                            CompanyId = user.CompanyId,
                            CustomerId = customer.CustomerId,
                        };
                        db.CompanyCustomers.Add(companyCustomer);
                        db.SaveChanges();
                        transaction.Commit();
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
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
            var customer = db.Customers.Find(id);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var companyCustomer = db.CompanyCustomers.Where(cc => cc.CompanyId == user.CompanyId && cc.CustomerId == customer.CustomerId).FirstOrDefault();
            using (var transaction = db.Database.BeginTransaction())
            {
                db.CompanyCustomers.Remove(companyCustomer);
                db.Customers.Remove(customer);
                var response = DBHelper.SaveChanges(db);
                if(response.Succeeded)
                {
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
                transaction.Rollback();
                ModelState.AddModelError(string.Empty, response.Message);
                return View(customer); 
            }
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
