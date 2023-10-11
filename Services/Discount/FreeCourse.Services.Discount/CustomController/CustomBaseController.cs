using freeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.CustomController
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> reponse)
        {
            return new ObjectResult(reponse)
            {
                StatusCode = reponse.StatusCode
            };
        }
    }
}
