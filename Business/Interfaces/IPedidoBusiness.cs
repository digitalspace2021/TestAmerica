using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPedidoBusiness
    {
        Task<PagedListDto<PedidoDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<PedidoDto>> GetAll();
        Task<IEnumerable<PedidoDto>> GetComisionByVendedor(int year, int month);
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<PedidoDto> GetById(int id);
        Task<Pedido> Save(PedidoDto entity);
        Task Update(int id, PedidoDto entity);
        Task Delete(int id);
    }
}
