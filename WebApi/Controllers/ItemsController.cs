using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Model;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        [Route("api/items/list")]
        public IHttpActionResult GetItems()
        {
            var items = db.Items.ToList().Select(o => o.ConvertToDTO()).ToList();
            var serializedItems = JsonConvert.SerializeObject(items);
            return Ok(serializedItems);
        }

        [HttpGet]
        [Route("api/items/item")]
        public IHttpActionResult GetItem(int id)
        {
            var item = db.Items.FirstOrDefault(i => i.id == id);
            if (item == null)
            {
                return NotFound();
            }

            var serializedItem = JsonConvert.SerializeObject(item.ConvertToDTO());
            return Ok(serializedItem);
        }


        [HttpPatch]
        [Route("api/items/item")]
        public IHttpActionResult PatchItem(int id, int remainder)
        {
            var item = db.Items.FirstOrDefault(i => i.id == id);
            if (item == null)
            {
                return BadRequest();
            }
            db.Items.Remove(item);
            db.Items.Add(new Item() {id = item.id, remainder = item.remainder + remainder});
            db.SaveChanges();
            return Ok("");
        }

        [HttpPost]
        [Route("api/items/item")]
        public IHttpActionResult PostItem(int id, int remainder)
        {
            var item = db.Items.FirstOrDefault(i => i.id == id);
            if (item != null)
            {
                return BadRequest();
            }

            db.Items.Add(new Item { id = id, remainder = remainder });
            db.SaveChanges();
            return Ok("");
        }
    }
}
