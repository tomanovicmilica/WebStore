using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.RequestHelpers.Extensions;

namespace API.Controllers
{
    public class ProductSizeController: BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public ProductSizeController(StoreContext context, IMapper mapper) {

            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UpdateProductSizeDto>>> GetProductSizes() {
            var  sizes = await _context.ProductSizes!.Select(p => p.Size).ToListAsync();

            var productSizes = await _context.ProductSizes!
                .ProjectSizeToProductSize()
                .ToListAsync();
            return productSizes;
        }

        [HttpGet("{id}", Name = "GetProductSize")]
        public async Task<ActionResult<UpdateProductSizeDto>> GetProductSize(int id) {

            var productSize = await _context!.ProductSizes!.ProjectSizeToProductSize().FirstOrDefaultAsync(x => x.Id == id);

            if(productSize==null) return NotFound();

            return productSize;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductSize>> CreateProductSize([FromForm] ProductSizeDto productSizeDto)
        { 

            var productSize = _mapper.Map<ProductSize>(productSizeDto);

            _context.ProductSizes!.Add(productSize);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetProductSize", new { Id = productSize.Id }, productSize);

            return BadRequest(new ProblemDetails { Title = "Problem creating new product size" });

        }

    }
}