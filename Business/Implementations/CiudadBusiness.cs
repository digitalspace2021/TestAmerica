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
    public class CiudadBusiness : ICiudadBusiness
    {
        private readonly ICiudadData data;
        public CiudadBusiness(ICiudadData data)
        {
            this.data = data;
        }
        public async Task Delete(string id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<CiudadDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<CiudadDto> GetById(string id)
        {
            Ciudad ciudad = await this.data.GetById(id);
            CiudadDto ciudadDto = new CiudadDto();

            ciudadDto.CodDep = ciudad.Id;
            ciudadDto.Nombre = ciudad.Nombre;
            ciudadDto.CodDep = ciudad.CodDep;

            return ciudadDto;
        }

        public async Task<PagedListDto<CiudadDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<Ciudad> Save(CiudadDto entity)
        {
            Ciudad ciudad = new Ciudad();
            ciudad = this.mapearDatos(ciudad, entity);

            return await this.data.Save(ciudad);
        }

        public async Task Update(string id, CiudadDto entity)
        {
            Ciudad ciudad = await this.data.GetById(id);
            if (ciudad == null)
            {
                throw new Exception("Registro no encontrado");
            }
            ciudad = this.mapearDatos(ciudad, entity);

            await this.data.Update(ciudad);
        }

        private Ciudad mapearDatos(Ciudad data, CiudadDto dataDto)
        {
            data.Id = dataDto.CodDep;
            data.Nombre = dataDto.Nombre;
            data.CodDep = dataDto.CodDep;
            return data;
        }
    }
}
