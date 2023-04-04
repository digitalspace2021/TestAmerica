using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface ICiudadData
    {
        Task<PagedListDto<CiudadDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<CiudadDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Ciudad> GetById(string id);
        Task<Ciudad> Save(Ciudad entity);
        Task Update(Ciudad entity);
        Task Delete(string id);
    }
}
