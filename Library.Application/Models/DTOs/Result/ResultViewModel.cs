using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models.DTOs.Result
{

    public class ResultViewModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResultViewModel<T> Ok(T data, string message = "") =>
            new ResultViewModel<T> { Success = true, Message = message, Data = data };

        public static ResultViewModel<T> Fail(string message) =>
            new ResultViewModel<T> { Success = false, Message = message };
    }

}

