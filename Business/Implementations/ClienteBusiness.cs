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
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly IClienteData data;
        public ClienteBusiness(IClienteData data)
        {
            this.data = data;
        }
        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<ClienteDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<ClienteDto> GetById(int id)
        {
            Cliente vendedor = await this.data.GetById(id);
            ClienteDto vendedorDto = new ClienteDto();

            vendedorDto.CodCli = vendedor.Id;
            vendedorDto.Nombre = vendedor.Nombre;
            vendedorDto.Direccion = vendedor.Direccion;
            vendedorDto.Telefono = vendedor.Telefono;
            vendedorDto.Cupo = vendedor.Cupo;
            vendedorDto.FechaCreacion = vendedor.FechaCreacion;
            vendedorDto.Canal = vendedor.Canal;
            vendedorDto.CodVend = vendedor.CodVend;
            vendedorDto.CodCiu = vendedor.CodCiu;
            return vendedorDto;
        }

        public async Task<PagedListDto<ClienteDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<Cliente> Save(ClienteDto entity)
        {
            Cliente vendedor = new Cliente();
            vendedor = this.mapearDatos(vendedor, entity);

            return await this.data.Save(vendedor);
        }

        public async Task Update(int id, ClienteDto entity)
        {
            Cliente vendedor = await this.data.GetById(id);
            if (vendedor == null)
            {
                throw new Exception("Registro no encontrado");
            }
            vendedor = this.mapearDatos(vendedor, entity);

            await this.data.Update(vendedor);
        }

        private Cliente mapearDatos(Cliente data, ClienteDto dataDto)
        {
            data.Id = dataDto.CodVend;
            data.Nombre = dataDto.Nombre;
            data.Direccion = dataDto.Direccion;
            data.Telefono = dataDto.Telefono;
            data.Cupo = dataDto.Cupo;
            data.FechaCreacion = dataDto.FechaCreacion;
            data.Canal = dataDto.Canal;
            data.CodVend = dataDto.CodVend;
            data.CodCiu = dataDto.CodCiu;
            return data;
        }
    }
}
