using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Shared.Enums;

namespace TicTacToe.Shared.Responses
{
    public class Result<T>
    {
        public ResultType Type { get; set; }
        //public T? Data { get; set; }
        public string? Message { get; set; }

        public static Result<T> ok(string message) => 
            new Result<T>{ Type = ResultType.Success, Message=message };

        public static Result<T> fail(ResultType result, string message) =>
            new Result<T>{ Type = result, Message = message};
    }
}