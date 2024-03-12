using AliExpress.Dtos.Category;
using AliExpress.Dtos.Images;
using AliExpress.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface IImagesService
    {
        Task<IEnumerable<ImagesDto>> GetAllCategory();
        Task<ResultView<ImagesDto>> Create(ImagesDto iImageDto);
        Task<ResultView<ImagesDto>> Update(ImagesDto imageDto);
        Task Delete(ImagesDto imageDto);
        Task<ResultView<ImagesDto>> GetOne(int Id);
    }
}
