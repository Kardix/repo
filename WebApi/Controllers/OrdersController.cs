using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Core.Model;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        [Route("api/orders/list")]
        public IHttpActionResult GetOrders()
        {
            var orders = db.Orders.ToList().Select(o => o.ConvertToDTO()).ToList();
            var serializedOrders = JsonConvert.SerializeObject(orders);
            return Ok(serializedOrders);
        }

        [HttpGet]
        [Route("api/orders/order")]
        public IHttpActionResult GetOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(i => i.id == id);
            if (order == null)
            {
                return NotFound();
            }

            var serializedItem = JsonConvert.SerializeObject(order.ConvertToDTO());
            return Ok(serializedItem);
        }

        [HttpPost]
        [Route("api/orders/order")]
        public IHttpActionResult PostOrder(int id, string itemlist, string email, string adress)
        {
            string state = "Created";
            string[] itemset = itemlist.Split(';');
            foreach (var listing in itemset)
            {
                string[] values = listing.Split(',');
                var item = db.Items.FirstOrDefault(i => i.id == Convert.ToInt32(values[0]));
                if (item.remainder < Convert.ToInt32(values[1]))
                {
                    state = "Not In Stock";
                }
                else if (state != "Not In Stock")
                {
                    state = "Processed";
                }
            }
            if(state != "NotInStock")
                foreach (var listing in itemset)
                {
                    string[] values = listing.Split(',');
                    var item = db.Items.FirstOrDefault(i => i.id == Convert.ToInt32(values[0]));
                    db.Items.Remove(item);
                    item.remainder -= Convert.ToInt32(values[1]);
                    db.Items.Add(item);
                }

            db.Orders.Add(new Order() {id = id, itemsList = itemlist, email = email, adress = adress, state = state});
            db.SaveChanges();
            return Ok("");
        }
    }
}
