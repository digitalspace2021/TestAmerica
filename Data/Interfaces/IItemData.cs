using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface IItemData
    {
        Task<PagedListDto<ItemDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<ItemDto>> GetAll();
        Task<IEnumerable<ItemDto>> GetSalesByDepartamento(DateTime fechaInicial, DateTime fechaFinal);
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Item> GetById(int id);
        Task<Item> Save(Item entity);
        Task Update(Item entity);
        Task Delete(int id);
    }
}
