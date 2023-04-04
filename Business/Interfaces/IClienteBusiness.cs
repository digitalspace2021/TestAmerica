using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IClienteBusiness
    {
        Task<PagedListDto<ClienteDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<ClienteDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<ClienteDto> GetById(int id);
        Task<Cliente> Save(ClienteDto entity);
        Task Update(int id, ClienteDto entity);
        Task Delete(int id);
    }
}
