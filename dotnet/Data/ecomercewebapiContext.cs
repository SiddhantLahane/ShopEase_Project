using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ecomercewebapi.Models;
using ecomercewebapi.Dtos;

namespace ecomercewebapi.Data
{
    public class ecomercewebapiContext : DbContext
    {
        public ecomercewebapiContext (DbContextOptions<ecomercewebapiContext> options)
            : base(options)
        {
        }

        
        public DbSet<ecomercewebapi.Models.Admin> Admin { get; set; } = default!;
        public DbSet<ecomercewebapi.Models.Users> Users { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.OrderDTO> OrderDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.OrderItemDTO> OrderItemDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.ProductDTO> ProductDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.CategoryDTO> CategoryDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.SubCategoryDTO> SubCategoryDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.SellerDTO> SellerDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.ShipperDTO> ShipperDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.AddressDTO> AddressDTO { get; set; } = default!;
        public DbSet<ecomercewebapi.Dtos.UserloginDto> UserloginDto { get; set; } = default!;
    }
}
