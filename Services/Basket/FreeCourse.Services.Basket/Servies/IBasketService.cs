using freeCourse.Shared.Dtos;
using FreeCourse.Services.Basket.Dtos;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Servies
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basket);

        Task<Response<bool>> Delete(string userId);
    }
}
