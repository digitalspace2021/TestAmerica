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
    public class PedidoData : IPedidoData
    {
        private readonly ApplicationDbContext context;

        protected readonly IConfiguration configuration;

        public PedidoData(ApplicationDbContext context, IConfiguration configuration)
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
            context.Pedido.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PedidoDto>> GetAll()
        {
            var sql = @"SELECT * FROM dbo.PEDIDO ORDER BY NUMPEDIDO ASC";
            var IEn = await this.context.QueryAsync<PedidoDto>(sql);
            return IEn;
        }

        public async Task<IEnumerable<PedidoDto>> GetComisionByVendedor(int year, int month)
        {
            var sql = @"  SELECT 
                                V.NOMBRE AS Vendedor, 
                                SUM(I.SUBTOTAL) * 0.01 AS Comision 
                                FROM PEDIDO AS P
                                inner join ITEMS AS I ON I.NUMPEDIDO = P.NUMPEDIDO
                                inner join CLIENTE AS C ON C.CODCLI = P.CLIENTE
                                inner join VENDEDOR AS V ON V.CODVEND = P.VENDEDOR
                                inner join CIUDAD AS CIU ON CIU.CODCIU = C.CIUDAD
                                inner join DEPARTAMENTO AS DEP ON DEP.CODDEP = CIU.DEPARTAMENTO
                                WHERE MONTH(P.FECHA) = @MONTH AND YEAR(P.FECHA) = @YEAR
                                GROUP BY V.NOMBRE";
            var IEn = await this.context.QueryAsync<PedidoDto>(sql, new { YEAR = year, MONTH = month });
            return IEn;
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            var sql = @"SELECT 
                                 CODDEP,
                                 NOMBRE AS TextoMostrar  
                             FROM dbo.PEDIDO
                             ORDER BY NUMPEDIDO ASC";
            return await this.context.QueryAsync<DataSelectDto>(sql);
        }

        public async Task<PagedListDto<PedidoDto>> GetDatatable(QueryFilterDto filter)
        {
            int pageNumber = (filter.PageNumber == 0) ? Int32.Parse(configuration["Pagination:DefaultPageNumber"]) : filter.PageNumber;
            int pageSize = (filter.PageSize == 0) ? Int32.Parse(configuration["Pagination:DefaultPageSize"]) : filter.PageSize;

            var sql = @"SELECT
                                 NUMPEDIDO,
                                 FECHA,
                                 CLIENTE,
                                 C.NOMBRE AS NombreCliente,
                                 VENDEDOR,
                                 V.NOMBRE AS NombreVendedor
                            FROM  dbo.PEDIDO
                            INNER JOIN CLIENTE AS C ON C.CODCLI = CLIENTE
                            INNER JOIN VENDEDOR AS V ON V.CODVEND = VENDEDOR
                            (UPPER(CONCAT(NUMPEDIDO, FECHA)) LIKE UPPER(CONCAT('%', @filter, '%'))) 
                            ORDER BY '" + (filter.ColumnOrder ?? "NUMPEDIDO") + "' " + (filter.DirectionOrder ?? "asc");

            IEnumerable<PedidoDto> items = await context.QueryAsync<PedidoDto>(sql, new { Filter = filter.Filter });

            var pagedItems = PagedListDto<PedidoDto>.Create(items, pageNumber, pageSize);

            return pagedItems;
        }

        public async Task<Pedido> GetById(int id)
        {
            var sql = @"SELECT * FROM dbo.PEDIDO WHERE NUMPEDIDO = @Id ORDER BY Id ASC";
            return await this.context.QueryFirstOrDefaultAsync<Pedido>(sql, new { Id = id });
        }

        public async Task<Pedido> Save(Pedido entity)
        {
            context.Pedido.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Pedido entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }


    }
}
