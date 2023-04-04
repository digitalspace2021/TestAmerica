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
    public class VendedorBusiness : IVendedorBusiness
    {
        private readonly IVendedorData data;
        public VendedorBusiness(IVendedorData data)
        {
            this.data = data;
        }
        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<VendedorDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<VendedorDto> GetById(int id)
        {
            Vendedor vendedor = await this.data.GetById(id);
            VendedorDto vendedorDto = new VendedorDto();

            vendedorDto.CodVend = vendedor.Id;
            vendedorDto.Nombre = vendedor.Nombre;
            vendedorDto.Estado = vendedor.Estado;

            return vendedorDto;
        }

        public async Task<PagedListDto<VendedorDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<Vendedor> Save(VendedorDto entity)
        {
            Vendedor vendedor = new Vendedor();
            vendedor = this.mapearDatos(vendedor, entity);

            return await this.data.Save(vendedor);
        }

        public async Task Update(int id, VendedorDto entity)
        {
            Vendedor vendedor = await this.data.GetById(id);
            if (vendedor == null)
            {
                throw new Exception("Registro no encontrado");
            }
            vendedor = this.mapearDatos(vendedor, entity);

            await this.data.Update(vendedor);
        }

        private Vendedor mapearDatos(Vendedor data, VendedorDto dataDto)
        {
            data.Id = dataDto.CodVend;
            data.Nombre = dataDto.Nombre;
            data.Estado = dataDto.Estado;
            return data;
        }
    }
}
