using System;

namespace Domain.Abstractions
{
    public sealed record Error
    {
        public readonly string _code;
        public readonly string? _description;

        public Error(string Code, string? Description)
        {
            _code = Code;
            _description = Description;
        }

        public static readonly Error None = new(string.Empty, null);
    }
}
