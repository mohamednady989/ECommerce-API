using AutoMapper;
using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Baskets;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entites.Basket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Services
{
    public class BasketService : IBasketService
    {
        public IBasketRepository BasketRepository { get; }
        public IMapper Mapper { get; }

        public BasketService(IBasketRepository basketRepository ,IMapper mapper)
        {
            BasketRepository = basketRepository;
            Mapper = mapper;
            
        }

        public async Task<Result<BasketDTO>> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var basket=await BasketRepository.GetBasketAsync(basketId, ct);
            return basket == null ? Result<BasketDTO>.Fail(Error.Failure("Failure", "cannot find basket")) : Result<BasketDTO>.Ok(Mapper.Map<BasketDTO>(basket));
        }

        public async Task<Result<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basket, TimeSpan? TLV = null, CancellationToken ct = default)
        {
            var customerBasket = Mapper.Map<CustomerBasket>(basket);
            var basketResult = await BasketRepository.CraeteOrUpdateBasketAsync(customerBasket, TLV, ct);
            return basketResult == null ? Result<BasketDTO>.Fail(Error.Failure("Failure", "cannot create or update basket")) : Result<BasketDTO>.Ok(basket);

        }

      

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
           var result= await BasketRepository.DeleteBasketAsync(basketId, ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Failure", "cannot delete basket"));
        }
    }
}
