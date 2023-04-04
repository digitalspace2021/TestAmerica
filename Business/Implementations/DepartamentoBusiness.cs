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
    public class DepartamentoBusiness : IDepartamentoBusiness
    {
        private readonly IDepartamentoData data;
        public DepartamentoBusiness(IDepartamentoData data)
        {
            this.data = data;
        }
        public async Task Delete(int id)
        {
            await this.data.Delete(id);
        }

        public async Task<IEnumerable<DepartamentoDto>> GetAll()
        {
            return await this.data.GetAll();
        }

        public async Task<IEnumerable<DataSelectDto>> GetAllSelect()
        {
            return await this.data.GetAllSelect();
        }

        public async Task<DepartamentoDto> GetById(int id)
        {
            Departamento departamento = await this.data.GetById(id);
            DepartamentoDto departamentoDto = new DepartamentoDto();

            departamentoDto.CodDep = departamento.Id;
            departamentoDto.Nombre = departamento.Nombre;

            return departamentoDto;
        }

        public async Task<PagedListDto<DepartamentoDto>> GetDatatable(QueryFilterDto filters)
        {
            return await this.data.GetDatatable(filters);
        }

        public async Task<Departamento> Save(DepartamentoDto entity)
        {
            Departamento departamento = new Departamento();
            departamento = this.mapearDatos(departamento, entity);

            return await this.data.Save(departamento);
        }

        public async Task Update(int id, DepartamentoDto entity)
        {
            Departamento departamento = await this.data.GetById(id);
            if (departamento == null)
            {
                throw new Exception("Registro no encontrado");
            }
            departamento = this.mapearDatos(departamento, entity);

            await this.data.Update(departamento);
        }

        private Departamento mapearDatos(Departamento data, DepartamentoDto dataDto)
        {
            data.Id = dataDto.CodDep;
            data.Nombre = dataDto.Nombre;
            return data;
        }
    }
}
