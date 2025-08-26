using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Errors;
namespace TaskManagement.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure =>!IsSuccess;
        public T?Value { get; }
        public DomainError? Error { get; }
        private Result(T? value)
        {
            IsSuccess = true;
            Value = value;

        }

        private Result(DomainError error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(DomainError error) => new Result<T>(error);
    }
}
