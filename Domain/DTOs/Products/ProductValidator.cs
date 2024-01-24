namespace Domain.DTOs.Products
{
    public class ProductValidator
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool Validate()
        {
            return
                !string.IsNullOrEmpty(Label) &&
                !string.IsNullOrWhiteSpace(Label) &&
                !string.IsNullOrEmpty(Description) &&
                !string.IsNullOrWhiteSpace(Description) &&
                Price > 0;
        }

        public bool ValidateForUpdate()
        {
            return
                (
                    !string.IsNullOrEmpty(Label) &&
                    !string.IsNullOrWhiteSpace(Label)
                ) ||
                (
                    !string.IsNullOrEmpty(Description) &&
                    !string.IsNullOrWhiteSpace(Description)
                ) ||
                Price > 0;
        }
    }
}