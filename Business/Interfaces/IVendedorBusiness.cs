using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IVendedorBusiness
    {
        Task<PagedListDto<VendedorDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<VendedorDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<VendedorDto> GetById(int id);
        Task<Vendedor> Save(VendedorDto entity);
        Task Update(int id, VendedorDto entity);
        Task Delete(int id);
    }
}
