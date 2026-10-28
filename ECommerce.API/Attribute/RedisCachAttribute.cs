using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attribute
{
    public class RedisCachAttribute:ActionFilterAttribute
    {
        public int DurationInsec;

        public RedisCachAttribute(int durationInsec=60 )
        {
            DurationInsec = durationInsec;
        }
        public override  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
         {
            var cashService =context.HttpContext.RequestServices.GetService<ECommerce.Application.Contracts.ICacheService>();
            // Implement caching logic here
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var data = await  cashService.GetDAtaAsync(cacheKey);
            if (!string.IsNullOrEmpty(data))
            {
                context.Result = new ContentResult()
               {
                    Content = data,
                   ContentType = "application/json",
                   StatusCode = StatusCodes.Status200OK
               };
                
                return;
            }
            var executedContext = await next.Invoke();

            if (executedContext.Result is OkObjectResult {Value:not null }ok)
            {
                await cashService.SetDataAsync(cacheKey,ok.Value,TimeSpan.FromMinutes(60));
            }

            await base.OnActionExecutionAsync(context, next);
            return;
         }
        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            if (request.Query.Any())
            {
                key.Append(request.QueryString.Value);
            }
            foreach (var (K,V) in request.Query.OrderBy(x=>x.Key))
            {
                key.Append(K).Append("=").Append(V).Append('&');

            }
            return key.ToString();

        }

    }
}
