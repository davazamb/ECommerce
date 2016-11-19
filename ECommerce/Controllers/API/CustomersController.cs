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
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: api/Customers
        public IHttpActionResult GetCustomers()
        {
            //Propiedad necesaria proxy para que las propiedades virtuales funcionen
            db.Configuration.ProxyCreationEnabled = false;
            var customers = db.Customers.ToList();
            var customersResponse = new List<CustomersResponse>();
            foreach (var customer in customers)
            {
                var customerResponse = new CustomersResponse
                {
                    Address = customer.Address,
                    City = customer.City,
                    CompanyCustomers = customer.CompanyCustomers,
                    CustomerId = customer.CustomerId,
                    Departament = customer.Departament,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Orders = customer.Orders,
                    Phone = customer.Phone,
                    UserName = customer.UserName
                };
                customersResponse.Add(customerResponse);
            }

            return Ok(customersResponse);
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            //Propiedad necesaria proxy para que las propiedades virtuales funcionen
            db.Configuration.ProxyCreationEnabled = false;
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}