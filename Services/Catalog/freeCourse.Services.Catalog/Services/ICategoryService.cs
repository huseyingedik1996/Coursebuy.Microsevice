using freeCourse.Services.Catalog.Dtos;
using freeCourse.Services.Catalog.Model;
using freeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace freeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);

        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
