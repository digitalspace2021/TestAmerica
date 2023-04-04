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
    public class CiudadData : ICiudadData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public CiudadData(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new Exception("Registro no encontrado");
            }
            //entity.DeletedAt = DateTime.Parse(DateTime.Today.ToString());
            context.Ciudad.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CiudadDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.CIUDAD ORDER BY CODCIU ASC";
            var IEn = await this.context.QueryAsync<CiudadDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 CODCIU,
                                 NOMBRE AS TextoMostrar  
                             FROM dbo.CIUDAD
                             ORDER BY CODCIU ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<CiudadDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 CODCIU,
                                 NOMBRE,
                                 DEPARTAMENTO,
                                 D.NOMBRE
                            FROM  dbo.CIUDAD
                            INNER JOIN DEPARTAMENTO AS D ON D.CODDEP = DEPARTAMENTO
                            (UPPER(CONCAT(CODCIU, NOMBRE)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "CODCIU") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<CiudadDto> items = await context.QueryAsync<CiudadDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<CiudadDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Ciudad> GetById(string id)
        {
            var sql = @"SELECT * FROM dbo.CIUDAD WHERE CODCIU = @CODCIU ORDER BY CODCIU ASC";
            return await this.context.QueryFirstOrDefaultAsync<Ciudad>(sql, new { CODCIU = id });
        }

        public async Task<Ciudad> Save(Ciudad entity)
        {
            context.Ciudad.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Ciudad entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

    }
}
