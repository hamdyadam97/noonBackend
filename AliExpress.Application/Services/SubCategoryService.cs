using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Category;
using AliExpress.Dtos.Subcategory;
using AliExpress.Dtos.ViewResult;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository,
            IMapper mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<SubCategoryDto>> Create(SubCategoryDto subcategoryDto)
        {
            var subCat=_mapper.Map<SubCategoryDto, Subcategory>(subcategoryDto);
               var newsubCat= await _subCategoryRepository.CreateAsync(subCat);
            var newsubCatDto=_mapper.Map<Subcategory,SubCategoryDto>(newsubCat);
            return new ResultView<SubCategoryDto> { Entity = newsubCatDto, IsSuccess = true, Message = "create success" };
            
        }

        public async Task Delete(SubCategoryDto subcategoryDto)
        {
            var subCat = _mapper.Map<SubCategoryDto, Subcategory>(subcategoryDto);
            await _subCategoryRepository.DeleteAsync(subCat);
        }

        public async Task<ResultList<SubCategoryDto>> GetAllCategory()
        {
            var subCats =await _subCategoryRepository.GetAllAsync();
            var subCatsDto = _mapper.Map<IEnumerable<Subcategory>, IEnumerable<SubCategoryDto>>(subCats);
            ResultList<SubCategoryDto> resultList = new ResultList<SubCategoryDto>();
            resultList.Entities = subCatsDto.ToList();
            resultList.Count = subCatsDto.Count();
            return resultList;
          

        }

        public async Task<ResultView<SubCategoryDto>> GetOne(int Id)
        {
            var subCat = await _subCategoryRepository.GetByIdAsync(Id);
            var subCatDto = _mapper.Map<Subcategory, SubCategoryDto>(subCat);
            return new ResultView<SubCategoryDto> { Entity = subCatDto, IsSuccess = true, Message = "create success" };

        }

        public async Task<ResultView<SubCategoryDto>> Update(SubCategoryDto subcategoryDto)
        {
            var subCat = _mapper.Map<SubCategoryDto, Subcategory>(subcategoryDto);
            var updatedsubCat =await _subCategoryRepository.UpdateAsync(subCat);
            var updatedsubCatDto = _mapper.Map<Subcategory, SubCategoryDto>(updatedsubCat);
            return new ResultView<SubCategoryDto> { Entity = updatedsubCatDto, IsSuccess = true, Message = "create success" };
        }
    }
}
