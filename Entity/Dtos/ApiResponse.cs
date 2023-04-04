using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Propiedad para establecer el estado de la petición True | False
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Devuelve un mensaje personalizado
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Devuelve una data de una entidad o listado de la misma
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Información extra, usado en su momento para paginación
        /// </summary>
        public MetaDataDto Meta { get; set; }
        /// <summary>
        /// Constructor vacío para establecer valores por defecto
        /// </summary>
        public ApiResponse()
        {
            Status = true;
            Message = "Ok";
            Data = default(T);
            Meta = null;
        }
        /// <summary>
        /// Constructor con información
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="meta"></param>
        public ApiResponse(T data, bool status, string message, MetaDataDto meta)
        {
            Data = data;
            Status = status;
            Message = message;
            Meta = meta;
        }
    }
}
    