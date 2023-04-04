using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IItemBusiness
    {
        Task<PagedListDto<ItemDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<ItemDto>> GetAll();
        Task<IEnumerable<ItemDto>> GetSalesByDepartamento(DateTime fechaInicial, DateTime fechaFinal);
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<ItemDto> GetById(int id);
        Task<Item> Save(ItemDto entity);
        Task Update(int id, ItemDto entity);
        Task Delete(int id);
    }
}
