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
    public class PedidoBusiness : IPedidoBusiness
    {
        private readonly IPedidoData data;
        public PedidoBusiness(IPedidoData data)
        {
            this.data = data;
        }
        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<PedidoDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<PedidoDto> GetById(int id)
        {
            Pedido producto = await this.data.GetById(id);
            PedidoDto productoDto = new PedidoDto();

            productoDto.NumPedido = producto.Id;
            productoDto.Fecha = producto.Fecha;
            productoDto.CodCli = producto.CodCli;
            productoDto.CodVend = producto.CodVend;

            return productoDto;
        }

        public async Task<IEnumerable<PedidoDto>> GetComisionByVendedor(int year, int month)
        {
            return await this.data.GetComisionByVendedor(year, month);
        }

        public async Task<PagedListDto<PedidoDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<Pedido> Save(PedidoDto entity)
        {
            Pedido producto = new Pedido();
            producto = this.mapearDatos(producto, entity);

            return await this.data.Save(producto);
        }

        public async Task Update(int id, PedidoDto entity)
        {
            Pedido producto = await this.data.GetById(id);
            if (producto == null)
            {
                throw new Exception("Registro no encontrado");
            }
            producto = this.mapearDatos(producto, entity);

            await this.data.Update(producto);
        }

        private Pedido mapearDatos(Pedido data, PedidoDto dataDto)
        {
            data.Id = dataDto.NumPedido;
            data.Fecha = dataDto.Fecha;
            data.CodCli = dataDto.CodCli;
            data.CodVend = dataDto.CodVend;
            return data;
        }
    }
}
