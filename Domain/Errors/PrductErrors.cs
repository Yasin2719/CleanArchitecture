using Domain.Abstractions;

namespace Domain.Errors
{
    public static class ProductErrors
    {
        public static readonly Error InvalidPayload = new("Products.InvalidPayload", "The payload is invalid");
        public static readonly Error NotFound = new("Products.NotFound", "The product is not foud");
    }
}
