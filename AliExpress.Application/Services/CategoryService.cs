using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Category;
using AliExpress.Dtos.Product;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<CategoryDto>> Create(CategoryDto categoryDto)
        {
           var category=_mapper.Map<CategoryDto,Category>(categoryDto);
           var NewCategory= await _categoryRepository.CreateAsync(category);
            var NewCategoryDto = _mapper.Map<Category, CategoryDto>(NewCategory);
            return new ResultView<CategoryDto> { Entity = NewCategoryDto, IsSuccess = true, Message = "create success" };
            //return NewCategoryDto;

        }

        public async Task<ResultView<CategoryDto>> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ResultView<CategoryDto> { IsSuccess = false, Message = "Category not here found" };
            }

            await _categoryRepository.DeleteAsync(category);
            return new ResultView<CategoryDto> { IsSuccess = true, Message = "Category deleted successfully" };
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            var categories= await _categoryRepository.GetAllAsync();
            var categoriesDto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<ResultView<CategoryDto>> GetOne(int Id)
        {
            
            var category =await _categoryRepository.GetByIdAsync(Id);
            var ReturnedCategoryDto=_mapper.Map<Category, CategoryDto>(category);

            ReturnedCategoryDto.Specification = category.Specifications.Select(spec => spec.Name).ToList();
            return new ResultView<CategoryDto> { Entity = ReturnedCategoryDto, IsSuccess = true, Message = "create success" };

        }

        public async Task<ResultView<CategoryDto>> Update(CategoryDto categoryDto)
        {
            var category=_mapper.Map<CategoryDto,Category>(categoryDto);
            var updatedCategory=await _categoryRepository.UpdateAsync(category);
            var updatedCategoryDto=_mapper.Map<Category,CategoryDto>(updatedCategory);
            return new ResultView<CategoryDto> { Entity = updatedCategoryDto, IsSuccess = true, Message = "create success" };


        }

      
    }
}
