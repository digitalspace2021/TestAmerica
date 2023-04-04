using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface IDepartamentoData
    {
        Task<PagedListDto<DepartamentoDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<DepartamentoDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Departamento> GetById(int id);
        Task<Departamento> Save(Departamento entity);
        Task Update(Departamento entity);
        Task Delete(int id);
    }
}
