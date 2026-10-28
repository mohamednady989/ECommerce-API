using AutoMapper;
using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Baskets;
using ECommerce.Application.DTO.Order;
using ECommerce.Application.Specification;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entites.Order;
using ECommerce.Domain.Entites.Prodects;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentGateway paymentGateway;
        private readonly IMapper mapper;
        private readonly PaymentGatewaySettints paymentGatewaySettints;

        public PaymentService(IBasketRepository basketRepository,
                                IUnitOfWork unitOfWork,
                                 IPaymentGateway paymentGateway,
                                 IOptions<PaymentGatewaySettints> Options,
                                 IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.paymentGateway = paymentGateway;
            this.mapper = mapper;
            paymentGatewaySettints = Options.Value;
        }
        public async Task<Result<BasketDTO>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken ct = default)
        {
            // Connect With Stripe
            // Create PaymentIntentId
            var basket = await basketRepository.GetBasketAsync(basketId, ct);
            if (basket == null)
                return Result<BasketDTO>.Fail(Error.NotFound("NotFoundError"));
            if (basket.Items.Count == 0)
                return Result<BasketDTO>.Fail(Error.Validation("Validation"));

            if(!basket.DeliveryMethodId.HasValue)
                return Result<BasketDTO>.Fail(Error.NotFound("DelivaryMethod NotFound Error"));
            var deliveryMethod = await unitOfWork.GetRepostiory<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value, ct);
            if(deliveryMethod==null )
                return Result<BasketDTO>.Fail(Error.NotFound("NotFoundError"));
            basket.ShippinfPrice = deliveryMethod.Price;



            var productIds = basket.Items.Select(X => X.Id).ToHashSet();
            var products = (await unitOfWork.GetRepostiory<Product, int>()
                                        .GetAllAsync(new ProductWithIdSpecification(productIds), ct)).ToDictionary(X => X.Id);
            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<BasketDTO>.Fail(Error.NotFound("Not Found", "Product not found"));
                item.Price = product.Price;
            }
            var subtotal = basket.Items.Sum(I => I.Price * I.Quantity);
            var amount = (long)(subtotal + deliveryMethod.Price)*100;

            if (string.IsNullOrEmpty(basket.PaymentIntenId))
            {
             
                var result = await paymentGateway.CreatePAymentIntentAsync(amount, paymentGatewaySettints.DefaultCurrency, ct);
                basket.PaymentIntenId = result.PaymentIntentId;
                basket.ClientSecret = result.ClientSecret;
            }
            else
            {
                await paymentGateway.UpdatePAymentIntentAsync(amount,basket.PaymentIntenId,ct  );  
            }
            await basketRepository.CraeteOrUpdateBasketAsync(basket, ct: ct);
            return Result<BasketDTO>.Ok(mapper.Map<BasketDTO>(basket));


        }
    }
}
