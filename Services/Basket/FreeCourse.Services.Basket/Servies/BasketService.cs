using freeCourse.Shared.Dtos;
using FreeCourse.Services.Basket.Dtos;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Servies
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.getDb().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);

        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.getDb().StringGetAsync(userId);

            if(string.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Basket not founr", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);

        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basket)
        {
            var status = await _redisService.getDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not save or update", 500);
        }
    }
}
