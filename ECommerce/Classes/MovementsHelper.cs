using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class MovementsHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();

        public void Dispose()
        {
            db.Dispose();
        }

        public static Response NewOrder(NewOrderView view, string userName)
        {
            using (var transacction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
                    var order = new Order
                    {
                        CompanyId = user.CompanyId,
                        CustomerId = view.CustomerId,
                        Date = view.Date,
                        Remarks = view.Remarks,
                        StateId = DBHelper.GetState("Created", db),
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();
                    var details = db.OrderDetailTmps.Where(odt => odt.UserName == userName).ToList();

                    foreach (var detail in details)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Description = detail.Description,
                            OrderId = order.OrderId,
                            Price = detail.Price,
                            ProductId = detail.ProductId,
                            Quantity = detail.Quantity,
                            TaxRate = detail.TaxRate,
                        };
                        db.OrderDetails.Add(orderDetail);
                        db.OrderDetailTmps.Remove(detail);
                    }
                    db.SaveChanges();
                    transacction.Commit();
                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    //devolvemos la transacion en bd
                    transacction.Rollback();
                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };                                                    
                }
            }                
        }
    }
}