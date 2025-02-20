using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.ServiceResultsDtos
{
    public class RequestResult<T>
    {
        public bool IsSuccess { get; } = false;

        public string Message { get; }

        public T? Data { get; }


        private RequestResult(bool isSuccess, string message, T? data = default)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
        }

        public static RequestResult<T> Success(string message, T? data = default)
        {
            return new RequestResult<T>(true, message, data);
        }

        public static RequestResult<T> Failed(string message)
        {
            return new (false, message, default);
        }
    }
}
