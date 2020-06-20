using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw13.DTOs;
using cw13.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw13.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {


        private readonly MyContext _context;

        public OrderController(MyContext context)
        {
            this._context = context;
        }

        //zad1
        [HttpGet("order/{lastname?}")]
        public ActionResult<IEnumerable<Order>> GetOrderResoponses(string lastname = null)
        {
            List<Order> orders = new List<Order>();

            if (lastname != null)
            {
                Client client = _context.Clients.FirstOrDefault(c => c.LastName.Equals(lastname));
                if (client != null)
                {
                    orders = _context.Orders.Where(o => o.IdClient == client.IdClient).Include(x => x.OrderConfectioneries).ToList();
                }
                else
                {
                    return NotFound("Nie znaleziono takiego nazwiska");
                }
            }
            else
            {
                orders = _context.Orders.Include(x => x.OrderConfectioneries).ToList();
            }


            List<OrderResponse> orderResponses = new List<OrderResponse>();
            orders.ForEach(
                order =>
                {
                    List<string> names = new List<string>();
                    order.OrderConfectioneries.ForEach(
                        x =>
                        {
                            names.Add(_context.Confectioneries.Where(c => c.IdConfectionery == x.IdConfectionery).Select(x => x.Name).First());
                        }
                    );
                    orderResponses.Add(new OrderResponse {
                        Id = order.IdOrder,
                        Names = names });
                }
            );


            return Ok(orderResponses);
        }

        [HttpPost("clients/{idClient}/orders")]
        public ActionResult AddOrder(int idClient, OrderRequest orderRequest)
        {
            if (!_context.Clients.Any(x => x.IdClient == idClient)) return BadRequest("Podany klient nie istnieje");

            bool anyExists = false;

            orderRequest.Confectioneries.ForEach(
                x =>
                {
                    Console.WriteLine(x.Name);
                    if (!_context.Confectioneries.Any(c => c.Name.Equals(x.Name)))
                    {
                        anyExists = true;
                    }
                }
                );

            if (anyExists)
            {
                return BadRequest("Podany wybór nie istnieje");
            }
            
            using(var transaction = _context.Database.BeginTransaction()) {

                var newOrder = new Order
                {
                    DateOfAdmission = orderRequest.DateOfAdmission,
                    Comments = orderRequest.Comments,
                    DateOfRealization = DateTime.Now,
                    IdClient = idClient,
                    IdEmployee = 1
                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                orderRequest.Confectioneries.ForEach(
                    x =>
                    {
                        Confectionery c = _context.Confectioneries.Where(c => c.Name.Equals(x.Name)).FirstOrDefault();
                        var orderConfectionery = new OrderConfectionery
                        {
                            IdConfectionery = c.IdConfectionery,
                            IdOrder = newOrder.IdOrder,
                            Quantity = x.Quantity
                        };

                        _context.OrderConfectioneries.Add(orderConfectionery);
                    }
                );

                _context.SaveChanges();

                transaction.Commit();
            }

            return Ok("Dodano dane");
        }
    }
}
