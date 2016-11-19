using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ECommerce.Models;
using ECommerce.Classes;

namespace ECommerce.Controllers.API
{
    public class DepartamentsController : ApiController
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: api/Departaments
        public IHttpActionResult GetDepartaments()
        {
            var departaments = db.Departaments.ToList();
            var departamentsResponse = new List<DepartmentResponse>();
            foreach (var department in departaments)
            {
                var departmentResponse = new DepartmentResponse
                {
                    Cities = department.Cities,
                    Customers = department.Customers,
                    DepartamentId = department.DepartamentId,
                    Name = department.Name
                };
                departamentsResponse.Add(departmentResponse);
            }
            return Ok(departamentsResponse);
        }

        // GET: api/Departaments/5
        [ResponseType(typeof(Departament))]
        public IHttpActionResult GetDepartament(int id)
        {
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return NotFound();
            }

            return Ok(departament);
        }

        // PUT: api/Departaments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartament(int id, Departament departament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departament.DepartamentId)
            {
                return BadRequest();
            }

            db.Entry(departament).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departaments
        [ResponseType(typeof(Departament))]
        public IHttpActionResult PostDepartament(Departament departament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departaments.Add(departament);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departament.DepartamentId }, departament);
        }

        // DELETE: api/Departaments/5
        [ResponseType(typeof(Departament))]
        public IHttpActionResult DeleteDepartament(int id)
        {
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return NotFound();
            }

            db.Departaments.Remove(departament);
            db.SaveChanges();

            return Ok(departament);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartamentExists(int id)
        {
            return db.Departaments.Count(e => e.DepartamentId == id) > 0;
        }
    }
}