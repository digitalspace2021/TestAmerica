using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Data.Interfaces
{
    public interface IProductoData
    {
        Task<PagedListDto<ProductoDto>> GetDatatable(QueryFilterDto filter);
        Task<IEnumerable<ProductoDto>> GetAll();
        Task<IEnumerable<DataSelectDto>> GetAllSelect();
        Task<Producto> GetById(int id);
        Task<Producto> Save(Producto entity);
        Task Update(Producto entity);
        Task Delete(int id);
    }
}
