using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }

        public string ItemList { get; set; }

        public string Email { get; set; }

        public string State { get; set; }

        public string Address { get; set; }
    }
}
