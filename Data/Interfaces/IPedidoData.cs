using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface IPedidoData
    {
        Task<PagedListDto<PedidoDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<PedidoDto>> GetAll();

        Task<IEnumerable<PedidoDto>> GetComisionByVendedor(int year, int month);

        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Pedido> GetById(int id);
        Task<Pedido> Save(Pedido entity);
        Task Update(Pedido entity);
        Task Delete(int id);
    }
}
