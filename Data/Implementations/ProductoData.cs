using Data.Interfaces;
using Entity.Dtos;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAmerica.Entity.Contexts;

namespace Data.Implementations
{
    public class ProductoData : IProductoData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public ProductoData(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new Exception("Registro no encontrado");
            }
            //entity.DeletedAt = DateTime.Parse(DateTime.Today.ToString());
            context.Producto.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductoDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.PRODUCTO ORDER BY CODPROD ASC";
            var IEn = await this.context.QueryAsync<ProductoDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 CODDEP,
                                 NOMBRE AS TextoMostrar  
                             FROM dbo.PRODUCTO
                             ORDER BY CODPROD ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<ProductoDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 CODDEP,
                                 NOMBRE 
                            FROM  dbo.PRODUCTO
                            (UPPER(CONCAT(CODDEP, NOMBRE)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "CODPROD") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<ProductoDto> items = await context.QueryAsync<ProductoDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<ProductoDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Producto> GetById(int id)
        {
            var sql = @"SELECT * FROM dbo.PRODUCTO WHERE CODPROD = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });
        }

        public async Task<Producto> Save(Producto entity)
        {
            context.Producto.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Producto entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
