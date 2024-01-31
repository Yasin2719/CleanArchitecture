using Domain.Abstractions;
namespace Domain.Errors
{
    public static class CategoryErrors
    {
        public static readonly Error InvalidPayload = new("Category.InvalidPayload", "The payload is invalid");
        public static readonly Error NotFound = new("Category.NotFound", "The category was not found");
    }
}
