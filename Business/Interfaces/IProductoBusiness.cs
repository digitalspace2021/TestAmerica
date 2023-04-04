using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProductoBusiness
    {
        Task<PagedListDto<ProductoDto>> GetDatatable(QueryFilterDto filters);
        Task<IEnumerable<ProductoDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<ProductoDto> GetById(int id);
        Task<Producto> Save(ProductoDto entity);
        Task Update(int id, ProductoDto entity);
        Task Delete(int id);
    }
}
