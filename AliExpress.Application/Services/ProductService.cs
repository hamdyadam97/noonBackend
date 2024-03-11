using AliExpress.Application.Contract;
using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Product;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository ,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<CreateUpdateDeleteProductDto> Create(CreateUpdateDeleteProductDto productDto)
        {
            var product = _mapper.Map<CreateUpdateDeleteProductDto, Product>(productDto);
            var NewProduct= await _productRepository.CreateAsync(product);
            var CreatedProductDto = _mapper.Map<Product, CreateUpdateDeleteProductDto>(NewProduct);
            return CreatedProductDto;
        }

        public async Task<CreateUpdateDeleteProductDto> Update(CreateUpdateDeleteProductDto productDto)
        {
            var product = _mapper.Map<CreateUpdateDeleteProductDto, Product>(productDto);
            var NewProduct = await _productRepository.UpdateAsync(product);
            var UpdatedProductDto = _mapper.Map<Product, CreateUpdateDeleteProductDto>(NewProduct);
            return UpdatedProductDto;
        }
      
        public async Task<PaginationResult<ProductViewDto>> GetAllProducts(string searchValue, int page, int pageSize)
        {
            var products = await _productRepository.GetAllAsync(searchValue, page, pageSize);
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewDto>>(products);

            var result = new PaginationResult<ProductViewDto>(
                entities: productsDto,
                pageIndex: page,
                pageSize: pageSize,
                totalCount: products.Count()
            );

            return result;
        }
        public async Task<CreateUpdateDeleteProductDto> GetOne(int Id)
        {
            var product=await _productRepository.GetByIdAsync(Id);
            var ProductDto = _mapper.Map<Product, CreateUpdateDeleteProductDto>(product);
            return ProductDto;
        }
        public async Task Delete(CreateUpdateDeleteProductDto productDto)
        {
            var product = _mapper.Map<CreateUpdateDeleteProductDto,Product>(productDto);
             await _productRepository.DeleteAsync(product);
        }

        
    }
}
