using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Adys.Core.DTOs
{
    public class CustomNoResponseDto
    {
        public List<String> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public static CustomNoResponseDto Succes(int statusCode)
        {
            return new CustomNoResponseDto { StatusCode = statusCode };
        }
        public static CustomNoResponseDto Fail(int statusCode, List<String> errors)
        {
            return new CustomNoResponseDto { StatusCode = statusCode, Errors = errors };
        }
        public static CustomNoResponseDto Fail(int statusCode, string errors)
        {
            return new CustomNoResponseDto { StatusCode = statusCode, Errors = new List<String> { errors } };
        }
    }
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<String> Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public static CustomResponseDto<T> Succes(int statusCode,T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Succes(int statusCode)
        {
            return new CustomResponseDto<T> {StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode,List<String> errors)
        {
            return new CustomResponseDto<T> { StatusCode=statusCode,Errors=errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<String> { errors } };
        }
    }
}
