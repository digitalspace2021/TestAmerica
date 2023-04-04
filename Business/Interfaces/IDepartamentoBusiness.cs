using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IDepartamentoBusiness
    {
        Task<PagedListDto<DepartamentoDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<DepartamentoDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<DepartamentoDto> GetById(int id);
        Task<Departamento> Save(DepartamentoDto entity);
        Task Update(int id, DepartamentoDto entity);
        Task Delete(int id);
    }
}
