using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Commen
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public IReadOnlyList<Error> Errors { get; set; }
        public Result(bool isSuccess ,IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
        public static Result Ok(IReadOnlyList<Domain.Entites.Prodects.ProductBrand> data) => new(true, Array.Empty<Error>());
        public static Result Fail(Error error) => new(false, new[] { error });
        public static Result Fail(IReadOnlyList<Error> errors) => new(false, errors);

    }
    public class Result<TValue>:Result
    {
        private readonly TValue _value;
        public TValue data => IsSuccess ? _value : throw new InvalidCastException("Failed to get data");

        private Result(TValue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        private Result(Error error) : base(false, new[] {error })
        {
            _value = default!;

        }
        private Result(IReadOnlyList<Error>errors ):base (false,errors)
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static Result<TValue> Fail(IReadOnlyList<Error> errors) => new Result<TValue>(errors);




    }
}
