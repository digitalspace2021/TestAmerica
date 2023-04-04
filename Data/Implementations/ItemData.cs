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
    public class ItemData : IItemData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public ItemData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Item.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemDto>> GetSalesByDepartamento(DateTime fechaInicial, DateTime fechaFinal)
        {
            var sql = @"select SUM(I.SUBTOTAL) as TotalVentas, DEP.NOMBRE as Departamento  from ITEMS I
                                inner join PEDIDO AS PED ON PED.NUMPEDIDO = I.NUMPEDIDO
                                inner join CLIENTE AS C ON C.CODCLI = PED.CLIENTE
                                inner join CIUDAD AS CIU ON CIU.CODCIU = C.CIUDAD
                                inner join DEPARTAMENTO AS DEP ON DEP.CODDEP = CIU.DEPARTAMENTO
                                WHERE PED.FECHA between (@fechaInicial) and (@fechaFinal)
                                GROUP BY DEP.NOMBRE
                                ORDER BY Departamento ASC";

            var IEn = await this.context.QueryAsync<ItemDto>(sql, new { fechaInicial = fechaInicial , fechaFinal = fechaFinal });
            return IEn;
        }

        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.ITEMS ORDER BY NUMPEDIDO ASC";
            var IEn = await this.context.QueryAsync<ItemDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 NUMPEDIDO,
                                 CONCAT(P.NOMBRE , ' - ', PRECIO )AS TextoMostrar  
                             FROM dbo.ITEMS
                            INNER JOIN PRODUCTO AS P ON P.CODPROD = PRODUCTO
                             ORDER BY NUMPEDIDO ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<ItemDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 NUMPEDIDO,
                                 NOMBRE,
                                 P.NOMBRE,
                                 PRECIO,
                                 CANTIDAD,
                                 SUBTOTAL
                            FROM  dbo.ITEMS
                            INNER JOIN PRODUCTO AS P ON P.CODPROD = PRODUCTO
                            (UPPER(CONCAT(CODDEP, NOMBRE)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "NUMPEDIDO") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<ItemDto> items = await context.QueryAsync<ItemDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<ItemDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Item> GetById(int id)
        {
            var sql = @"SELECT * FROM dbo.ITEMS WHERE NUMPEDIDO = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Item>(sql, new { Id = id });
        }

        public async Task<Item> Save(Item entity)
        {
            context.Item.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Item entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

    }
}
