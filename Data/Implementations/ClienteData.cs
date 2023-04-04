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
    public class ClienteData : IClienteData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public ClienteData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Cliente.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClienteDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.CLIENTE ORDER BY CODCLI ASC";
            var IEn = await this.context.QueryAsync<ClienteDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 CODCLI,
                                 NOMBRE AS TextoMostrar  
                             FROM dbo.CLIENTE
                             ORDER BY CODCLI ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<ClienteDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 CODCLI,
                                 NOMBRE 
                            FROM  dbo.CLIENTE
                            (UPPER(CONCAT(CODCLI, NOMBRE)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "CODCLI") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<ClienteDto> items = await context.QueryAsync<ClienteDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<ClienteDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Cliente> GetById(int id)
        {
            var sql = @"SELECT * FROM dbo.CLIENTE WHERE CODCLI = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
        }

        public async Task<Cliente> Save(Cliente entity)
        {
            context.Cliente.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Cliente entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
