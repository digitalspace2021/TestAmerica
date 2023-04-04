using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICiudadBusiness
    {
        Task<PagedListDto<CiudadDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<CiudadDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<CiudadDto> GetById(string id);
        Task<Ciudad> Save(CiudadDto entity);
        Task Update(string id, CiudadDto entity);
        Task Delete(string id);
    }
}
