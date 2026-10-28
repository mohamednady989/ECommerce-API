using ECommerce.Domain.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specification
{
    public  class OrderSpecification:BaseSpeification<Order,Guid>
    {
        public OrderSpecification(string email):base(x=>x.BuyerEmail==email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddorderBy(O => O.OrderDate);

        }
        public OrderSpecification(Guid id,string email):base(x=>x.BuyerEmail==email&&x.Id==id)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddorderBy(O => O.OrderDate);
        }
    }
}
