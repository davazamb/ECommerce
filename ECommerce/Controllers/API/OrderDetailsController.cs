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

namespace ECommerce.Controllers
{
    public class OrderDetailsController : ApiController
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: api/OrderDetails
        public IHttpActionResult GetOrderDetails()
        {
            var orderDetails = db.OrderDetails.ToList();
            var orderDetailsResponse = new List<OrderDetailResponse>();
            foreach (var orderDetail in orderDetails)
            {
                var orderDetailResponse = new OrderDetailResponse
                {
                    Description = orderDetail.Description,
                    Order = orderDetail.Order,
                    OrderDetailId = orderDetail.OrderDetailId,
                    Price = orderDetail.Price,
                    Product = orderDetail.Product,
                    Quantity = orderDetail.Quantity,
                    TaxRate = orderDetail.TaxRate
                };
                orderDetailsResponse.Add(orderDetailResponse);
            }
            return Ok(orderDetailsResponse);
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult GetOrderDetail(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            db.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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

        // POST: api/OrderDetails
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult PostOrderDetail(OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult DeleteOrderDetail(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();

            return Ok(orderDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderDetailId == id) > 0;
        }
    }
}