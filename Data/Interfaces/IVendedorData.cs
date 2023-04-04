using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface IVendedorData
    {
        Task<PagedListDto<VendedorDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<VendedorDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Vendedor> GetById(int id);
        Task<Vendedor> Save(Vendedor entity);
        Task Update(Vendedor entity);
        Task Delete(int id);
    }
}
