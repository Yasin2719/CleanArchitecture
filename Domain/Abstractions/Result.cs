using System;

namespace Domain.Abstractions
{
    public class Result<T>
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSucess = isSuccess;
            Error = error;
        }

        private Result(bool isSuccess, Error error, T data)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSucess = isSuccess;
            Error = error;
            Data = data;
        }

        public bool IsSucess { get; }

        public bool IsFailure => !IsSucess;

        public Error Error { get; }

        public T Data { get; }

        public static Result<T> Success(T data) => new(true, Error.None, data);

        public static Result<T> Failure(Error error, T data) => new(false, error);
    }
}
