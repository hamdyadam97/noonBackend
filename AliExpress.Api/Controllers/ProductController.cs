﻿using AliExpress.Application.Services;
using AliExpress.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts(int page = 1, string searchTerm = null)
        {
            const int pageSize = 10;
            var Prds = await _productService.GetAllProducts(searchTerm,page,pageSize);

            return Ok(Prds);
        }
    }
}
