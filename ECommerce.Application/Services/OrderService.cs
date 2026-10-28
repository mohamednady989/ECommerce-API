using AutoMapper;
using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Order;
using ECommerce.Application.Specification;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entites.Order;
using ECommerce.Domain.Entites.Prodects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork { get; }


        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<Result<OrderToReturnDTO>> CreateOrdedrAsync(OrderDTO order, string email, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(order.BasketId, ct);
            if (basket == null)
                return Result<OrderToReturnDTO>.Fail(Error.NotFound());

            var orderItems = new List<OrderItem>(basket.Items.Count);
            var productIds = basket.Items.Select(X => X.Id).ToHashSet();
            var products = (await UnitOfWork.GetRepostiory<Product, int>()
                                        .GetAllAsync(new ProductWithIdSpecification(productIds), ct)).ToDictionary(X => X.Id);
            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<OrderToReturnDTO>.Fail(Error.NotFound("Not Found", "product Not Found !"));
                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProdectItemOrdered()
                    {
                        PictureUrl = product.PictureUrl,
                        ProductId = product.Id,
                        ProductName = product.Name
                    }
                });

            }
            var orderAddress = mapper.Map<OrderAddress>(order.ShipTpAddress);
            var deliveryMethod = await UnitOfWork.GetRepostiory<DeliveryMethod, int>()
                .GetByIdAsync(order.DeliveryMethodId);
            if (deliveryMethod==null)
            
               return Result<OrderToReturnDTO>.Fail(Error.NotFound("Not Found", "product Not Found"));



            var subTotal = orderItems.Sum(x => x.Quantity * x.Price);

            var Order = new Order(email,orderAddress,orderItems,deliveryMethod,subTotal);
            UnitOfWork.GetRepostiory<Order, Guid>().Add(Order);
            var result = await UnitOfWork.SaveChangesAsync(ct);
            if (result == 0)
              return  Result<OrderToReturnDTO>.Fail(Error.Failure("Not Found", "Basket Not Found"));
            else
            {
                await basketRepository.DeleteBasketAsync(order.BasketId, ct);
                return Result<OrderToReturnDTO>.Ok(mapper.Map<OrderToReturnDTO>(Order));
            }




            foreach (var item in basket.Items)
            {
                var Product = await UnitOfWork.GetRepostiory<Product, int>().GetAllAsync();

            }

        }

        public async Task<Result<IReadOnlyList<OrderToReturnDTO>>> GetAllOrderForUserAsync(string email, CancellationToken ct = default)
        {
            var orders =await UnitOfWork.GetRepostiory<Order, Guid>().GetAllAsync(new OrderSpecification(email),ct);
            if (orders.Any())
            {
                return Result<IReadOnlyList<OrderToReturnDTO>>.Ok(mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders));
            }
            else
            {
                return Result<IReadOnlyList<OrderToReturnDTO>>.Fail(Error.NotFound("Not Found", "Orders Not Found"));
            }
        }

        public async Task<Result<OrderToReturnDTO>> GetOrderByIdAndEmailFromUserAsync(Guid id, string emali, CancellationToken ct = default)
        {
            var order = await UnitOfWork.GetRepostiory<Order, Guid>().GetByIdAsync(new OrderSpecification(id, emali), ct);
            if (order == null)
            {
                return Result<OrderToReturnDTO>.Fail(Error.NotFound("Not Found", "Order Not Found"));
                
            }
            else
            {
                return Result<OrderToReturnDTO>.Ok(mapper.Map < OrderToReturnDTO>(order));
            }
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDTO>>> GetDeliveryMethedsAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await UnitOfWork.GetRepostiory<DeliveryMethod, int>().GetAllAsync(ct);
            if (deliveryMethods.Any())
                return Result<IReadOnlyList<DeliveryMethodDTO>>.Ok(mapper.Map<IReadOnlyList<DeliveryMethodDTO>>(deliveryMethods));
            else
            {
                return Result<IReadOnlyList<DeliveryMethodDTO>>.Fail(Error.NotFound("Not Found", "Delivery Method Not Found "));
            }
        }
    }
}
