using freeCourse.Shared.ControllerBase;
using freeCourse.Shared.Dtos;
using FreeCourse.Service.PhotoStock.CustomBase;
using FreeCourse.Service.PhotoStock.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Service.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if(photo != null )
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + photo.FileName;

                PhotoClass photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoClass>.Success(photoDto, 200));
            }

            return CreateActionResultInstance(Response<PhotoClass>.Fail("photo is empty",400));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if(!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));


            }
            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success((204)));
        }
    }
}
