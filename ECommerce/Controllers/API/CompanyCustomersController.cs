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
    public class CompanyCustomersController : ApiController
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: api/CompanyCustomers
        public IHttpActionResult GetCompanyCustomers()
        {
            var companyCustomers = db.CompanyCustomers.ToList();
            var companyCustomersResponse = new List<CompanyCustomerResponse>();
            //Recorremos la consulta 
            foreach (var companyCustomer in companyCustomers)
            {
                var companyCustomerResponse = new CompanyCustomerResponse
                {
                    Company = companyCustomer.Company,
                    CompanyCustomerId = companyCustomer.CompanyCustomerId,
                    Customer = companyCustomer.Customer
                    
                };
                companyCustomersResponse.Add(companyCustomerResponse);
            }
            return Ok(companyCustomersResponse);
        }

        // GET: api/CompanyCustomers/5
        [ResponseType(typeof(CompanyCustomer))]
        public IHttpActionResult GetCompanyCustomer(int id)
        {
            CompanyCustomer companyCustomer = db.CompanyCustomers.Find(id);
            if (companyCustomer == null)
            {
                return NotFound();
            }

            return Ok(companyCustomer);
        }

        // PUT: api/CompanyCustomers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyCustomer(int id, CompanyCustomer companyCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyCustomer.CompanyCustomerId)
            {
                return BadRequest();
            }

            db.Entry(companyCustomer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyCustomerExists(id))
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

        // POST: api/CompanyCustomers
        [ResponseType(typeof(CompanyCustomer))]
        public IHttpActionResult PostCompanyCustomer(CompanyCustomer companyCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompanyCustomers.Add(companyCustomer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyCustomer.CompanyCustomerId }, companyCustomer);
        }

        // DELETE: api/CompanyCustomers/5
        [ResponseType(typeof(CompanyCustomer))]
        public IHttpActionResult DeleteCompanyCustomer(int id)
        {
            CompanyCustomer companyCustomer = db.CompanyCustomers.Find(id);
            if (companyCustomer == null)
            {
                return NotFound();
            }

            db.CompanyCustomers.Remove(companyCustomer);
            db.SaveChanges();

            return Ok(companyCustomer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyCustomerExists(int id)
        {
            return db.CompanyCustomers.Count(e => e.CompanyCustomerId == id) > 0;
        }
    }
}