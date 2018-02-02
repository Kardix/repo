using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.DTO;

namespace Core.Model
{
    public static class Converter
    {
        public static OrderDTO ConvertToDTO(this Order order)
        {
            return new OrderDTO
            {
                Address = order.adress,
                Email = order.email,
                ID = order.id,
                ItemList = order.itemsList,
                State = order.state
            };
        }

        public static ItemDTO ConvertToDTO(this Item item)
        {
            return new ItemDTO
            {
                ID = item.id,
                Remainder = item.remainder
            };
        }
    }
}
