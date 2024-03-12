using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Category;
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
        public async Task<CategoryDto> Create(CategoryDto categoryDto)
        {
           var category=_mapper.Map<CategoryDto,Category>(categoryDto);
           var NewCategory= await _categoryRepository.CreateAsync(category);
           var NewCategoryDto = _mapper.Map<Category, CategoryDto>(NewCategory);
           return NewCategoryDto;

        }

        public async Task Delete(CategoryDto categoryDto)
        {
            var category=_mapper.Map<CategoryDto,Category>(categoryDto);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            var categories= await _categoryRepository.GetAllAsync();
            var categoriesDto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<CategoryDto> GetOne(int Id)
        {
            var category =await _categoryRepository.GetByIdAsync(Id);
            var ReturnedCategoryDto=_mapper.Map<Category, CategoryDto>(category);
            return ReturnedCategoryDto;
        }

        public async Task<CategoryDto> Update(CategoryDto categoryDto)
        {
            var category=_mapper.Map<CategoryDto,Category>(categoryDto);
            var updatedCategory=await _categoryRepository.UpdateAsync(category);
            var updatedCategoryDto=_mapper.Map<Category,CategoryDto>(updatedCategory);
            return updatedCategoryDto;
        }
    }
}
