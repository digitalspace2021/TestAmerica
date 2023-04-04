using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class ProductoBusiness : IProductoBusiness
    {
        private readonly IProductoData data;
        public ProductoBusiness(IProductoData data)
        {
            this.data = data;
        }
        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<ProductoDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<ProductoDto> GetById(int id)
        {
            Producto producto = await this.data.GetById(id);
            ProductoDto productoDto = new ProductoDto();

            productoDto.CodProd = producto.Id;
            productoDto.Nombre = producto.Nombre;
            productoDto.Familia = producto.Familia;
            productoDto.Precio = producto.Precio;

            return productoDto;
        }

        public async Task<PagedListDto<ProductoDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<Producto> Save(ProductoDto entity)
        {
            Producto producto = new Producto();
            producto = this.mapearDatos(producto, entity);

            return await this.data.Save(producto);
        }

        public async Task Update(int id, ProductoDto entity)
        {
            Producto producto = await this.data.GetById(id);
            if (producto == null)
            {
                throw new Exception("Registro no encontrado");
            }
            producto = this.mapearDatos(producto, entity);

            await this.data.Update(producto);
        }

        private Producto mapearDatos(Producto data, ProductoDto dataDto)
        {
            data.Id = dataDto.CodProd;
            data.Nombre = dataDto.Nombre;
            data.Familia = dataDto.Familia;
            data.Precio = dataDto.Precio;
            return data;
        }
    }
}
