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
    public class VendedorData : IVendedorData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public VendedorData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Vendedor.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VendedorDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.VENDEDOR ORDER BY CODVEND ASC";
            var IEn = await this.context.QueryAsync<VendedorDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 CODVEND,
                                 NOMBRE AS TextoMostrar  
                             FROM dbo.VENDEDOR
                             ORDER BY CODVEND ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<VendedorDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 CODVEND,
                                 NOMBRE 
                            FROM  dbo.VENDEDOR
                            (UPPER(CONCAT(CODVEND, NOMBRE)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "CODVEND") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<VendedorDto> items = await context.QueryAsync<VendedorDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<VendedorDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Vendedor> GetById(int id)
        {
            var sql = @"SELECT * FROM dbo.VENDEDOR WHERE CODVEND = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Vendedor>(sql, new { Id = id });
        }

        public async Task<Vendedor> Save(Vendedor entity)
        {
            context.Vendedor.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Vendedor entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
