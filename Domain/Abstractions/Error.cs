namespace Domain.Abstractions
{
    public sealed record Error
    {
        public readonly string Code;
        public readonly string? Description;

        public Error(string code, string? description)
        {
            Code = code;
            Description = description;
        }

        public static readonly Error None = new(string.Empty, null);

        public object GetJsonFormat()
        {
            return new
            {
                error = new
                {
                    code = Code,
                    description = Description
                }
            };
        }
    }
}
