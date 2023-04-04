using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface IClienteData
    {
        Task<PagedListDto<ClienteDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<ClienteDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Cliente> GetById(int id);
        Task<Cliente> Save(Cliente entity);
        Task Update(Cliente entity);
        Task Delete(int id);
    }
}
