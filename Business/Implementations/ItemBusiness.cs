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
    public class ItemBusiness : IItemBusiness
    {
        private readonly IItemData data;
        public ItemBusiness(IItemData data)
        {
            this.data = data;
        }
        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<ItemDto> GetById(int id)
        {
            Item item = await this.data.GetById(id);
            ItemDto itemDto = new ItemDto();

            itemDto.NumPedido = item.Id;
            itemDto.CodProd = item.CodProd;
            itemDto.Precio = item.Precio;
            itemDto.Cantidad= item.Cantidad;
            itemDto.Subtotal = item.Subtotal;

            return itemDto;
        }

        public async Task<PagedListDto<ItemDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<IEnumerable<ItemDto>> GetSalesByDepartamento(DateTime fechaInicial, DateTime fechaFinal)
        {
            return await this.data.GetSalesByDepartamento(fechaInicial, fechaFinal);
        }

        public async Task<Item> Save(ItemDto entity)
        {
            Item item = new Item();
            item = this.mapearDatos(item, entity);

            return await this.data.Save(item);
        }

        public async Task Update(int id, ItemDto entity)
        {
            Item item = await this.data.GetById(id);
            if (item == null)
            {
                throw new Exception("Registro no encontrado");
            }
            item = this.mapearDatos(item, entity);

            await this.data.Update(item);
        }

        private Item mapearDatos(Item data, ItemDto dataDto)
        {
            data.Id = dataDto.NumPedido;
            data.CodProd = dataDto.CodProd;
            data.Precio = dataDto.Precio;
            data.Cantidad = dataDto.Cantidad;
            data.Subtotal = dataDto.Subtotal;
            return data;
        }
    }
}
