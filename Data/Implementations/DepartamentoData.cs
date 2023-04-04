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
    public class DepartamentoData : IDepartamentoData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public DepartamentoData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Departamento.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartamentoDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.DEPARTAMENTO ORDER BY CODDEP ASC";
            var IEn = await this.context.QueryAsync<DepartamentoDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 CODDEP,
                                 NOMBRE AS TextoMostrar  
                             FROM dbo.DEPARTAMENTO
                             ORDER BY CODDEP ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<DepartamentoDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 CODDEP,
                                 NOMBRE 
                            FROM  dbo.DEPARTAMENTO
                            (UPPER(CONCAT(CODDEP, NOMBRE)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "CODDEP") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<DepartamentoDto> items = await context.QueryAsync<DepartamentoDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<DepartamentoDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Departamento> GetById(int id)
        {
            var sql = @"SELECT * FROM dbo.DEPARTAMENTO WHERE CODDEP = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Departamento>(sql, new { Id = id });
        }

        public async Task<Departamento> Save(Departamento entity)
        {
            context.Departamento.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Departamento entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
