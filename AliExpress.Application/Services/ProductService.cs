﻿using AliExpress.Application.Contract;
using AliExpress.Dtos.Category;
using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Product;
using AliExpress.Dtos.ViewResult;
using AliExpress.Models;
using AutoMapper;
using Azure;
using System;
using System.Buffers;
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
        public async Task<ResultView<CreateUpdateDeleteProductDto>> Create(CreateUpdateDeleteProductDto productDto)
        {

            var products = await _productRepository.SearchByName(productDto.Title);
            if (products.Count==0)
            {
                var product = _mapper.Map<CreateUpdateDeleteProductDto, Product>(productDto);
                var createdProduct = await _productRepository.CreateAsync(product);
                var createdProductDto = _mapper.Map<Product, CreateUpdateDeleteProductDto>(createdProduct);
                return new ResultView<CreateUpdateDeleteProductDto> { Entity = createdProductDto, IsSuccess = true, Message = "Create success" };
            }
            return new ResultView<CreateUpdateDeleteProductDto> { Entity = null, IsSuccess = false, Message = "Product Already Exist before Please change Product Name" };

        }
        public async Task<ResultView<CreateUpdateDeleteProductDto>> Update(CreateUpdateDeleteProductDto productDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productDto.Id);
            if (existingProduct == null)
            {
                // Handle the case where the product with the given Id doesn't exist
                return new ResultView<CreateUpdateDeleteProductDto> { IsSuccess = false, Message = "Product not found." };
            }

            _mapper.Map(productDto, existingProduct); // Update existing product with new data

            await _productRepository.UpdateAsync(existingProduct);

            return new ResultView<CreateUpdateDeleteProductDto> { IsSuccess = true, Message = "Product updated successfully." };
        }
        public async Task<PaginationResult<ProductViewDto>> GetAllProducts(string searchValue,string category, int page, int pageSize)
        {
            var products = await _productRepository.GetAllAsync(searchValue, category, page, pageSize);
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewDto>>(products, opts =>
            {
                opts.AfterMap((src, dest) =>
                {
                    foreach (var productDto in dest)
                    {
                        var product = src.FirstOrDefault(p => p.Id == productDto.Id);
                        if (product != null)
                        {
                            productDto.Image = product.Images.Select(img => img.Url).ToList();
                        }
                    }
                });
            });

            var result = new PaginationResult<ProductViewDto>(
                isSuccess: true,
                message: "data return success",
                entities: productsDto,
                pageIndex: page,
                pageSize: pageSize,
                totalCount: products.Count()
            );

            return result;
        }
        public async Task<ResultView<CreateUpdateDeleteProductDto>> GetOne(int Id)
        {
            var product=await _productRepository.GetByIdAsync(Id);
            var ProductDto = _mapper.Map<Product, CreateUpdateDeleteProductDto>(product);
            ProductDto.Images = product.Images.Select(img => img.Url).ToList();

            return new ResultView<CreateUpdateDeleteProductDto> { Entity = ProductDto, IsSuccess = true, Message = "create success" };
        }
        public async Task<ResultView<CreateUpdateDeleteProductDto>> Delete(int id)
        {
            var prod = await _productRepository.GetByIdAsync(id);
            if (prod == null)
            {
                return new ResultView<CreateUpdateDeleteProductDto> { IsSuccess = false, Message = "product not found" };
            }

            await _productRepository.DeleteAsync(prod);
            return new ResultView<CreateUpdateDeleteProductDto> { IsSuccess = true, Message = "product deleted successfully" };
        }
        public async Task<int>countProducts()
        {
            return await _productRepository.CoutProducts();
        }
        //public async Task<List<ProductViewDto>> Search(string name)
        //{
        //    var products = await _productRepository.GetAllAsync(name, 1, 24);
        //    var pdto=_mapper.Map<Product,ProductViewDto>(products.ToList());
        //}

    }
}
