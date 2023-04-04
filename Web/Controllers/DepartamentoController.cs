using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoBusiness business;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="business"></param>
        public DepartamentoController(IDepartamentoBusiness business)
        {
            this.business = business;
        }
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("datatable")]
        public async Task<ActionResult> Get([FromQuery] QueryFilterDto filters)
        {
            try
            {
                var data = await business.GetDatatable(filters);

                var metadata = new MetaDataDto
                {
                    TotalCount = data.TotalCount,
                    PageSize = data.PageSize,
                    CurrentPage = data.CurrentPage,
                    TotalPages = data.TotalPages,
                    HasNextPage = data.HasNextPage,
                    HasPreviousPage = data.HasPreviousPage
                };

                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(data, true, "Ok", metadata);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtener todo
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DepartamentoDto>>>> GetAll()
        {
            try
            {
                var data = await business.GetAll();

                if (data == null)
                {
                    var responseNull = new ApiResponse<IEnumerable<DepartamentoDto>>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(data, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Obtener todo select
        /// </summary>
        /// <param name="GetAllSelect"></param>
        /// <returns></returns>
        [HttpGet("AllSelect")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DataSelectDto>>>> GetAllSelect()
        {
            try
            {
                var data = await business.GetAllSelect();

                if (data == null)
                {
                    var responseNull = new ApiResponse<IEnumerable<DataSelectDto>>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<IEnumerable<DataSelectDto>>(data, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<DataSelectDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DepartamentoDto>>> Get(int id)
        {
            try
            {
                var data = await business.GetById(id);

                if (data == null)
                {
                    var responseNull = new ApiResponse<DepartamentoDto>(null, false, "Registro no encontrado", null);
                    return NotFound(responseNull);
                }

                var response = new ApiResponse<DepartamentoDto>(data, true, "Ok", null);

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DepartamentoDto dataDto)
        {
            try
            {
                Departamento data = await business.Save(dataDto);
                var response = new ApiResponse<Departamento>(null, true, "Registro almacenado exitosamente", null);

                return new CreatedAtRouteResult(new { id = dataDto.CodDep }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<Departamento>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DepartamentoDto dataDto)
        {
            try
            {
                await business.Update(id, dataDto);

                var response = new ApiResponse<DepartamentoDto>(null, true, "Registro actualizado exitosamente", null);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await business.Delete(id);
                return Ok(new ApiResponse<DepartamentoDto>(null, true, "Registro eliminado exitosamente", null));
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<DepartamentoDto>>(null, false, ex.Message.ToString(), null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
