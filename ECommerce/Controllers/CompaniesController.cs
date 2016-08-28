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
    [Authorize(Roles = "Admin")]
    public class CompaniesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Companies
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.City).Include(c => c.Departament);
            return View(companies.OrderBy(c => c.Name).ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name");
            return View();
        }

        // POST: Companies/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {               
                db.Companies.Add(company);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    if (company.LogoFile != null)
                    {
                        var folder = "~/Content/Logos";
                        var file = string.Format("{0}.jpg", company.CompanyId);
                        var responses = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                        if (responses)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            company.Logo = pic;
                            db.Entry(company).State = EntityState.Modified;
                            //db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);         
                                
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(company.DepartamentId), "CityId", "Name", company.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", company.DepartamentId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(company.DepartamentId), "CityId", "Name", company.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", company.DepartamentId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    if (company.LogoFile != null)
                    {
                        var pic = string.Empty;
                        var folder = "~/Content/Logos";
                        var file = string.Format("{0}.jpg", company.CompanyId);
                        var responses = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                        if (responses)
                        {
                            pic = string.Format("{0}/{1}", folder, file);
                            company.Logo = pic;
                        }
                    }
                    //UsersHelper.CreateUserASP(customer.UserName, "Customer");
                    //TODO: validar el cambio del email por cliente
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(company.DepartamentId), "CityId", "Name", company.CityId);
            ViewBag.DepartamentId = new SelectList(CombosHelper.GetDepartments(), "DepartamentId", "Name", company.DepartamentId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            //Mensaje de adverntecnia al eliminar en cascada
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, response.Message);
            return View(company);
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
