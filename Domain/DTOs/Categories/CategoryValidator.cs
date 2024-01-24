namespace Domain.DTOs.Categories
{
    public class CategoryValidator
    {
        public string Label { get; set; }

        public bool Validate()
        {
            return
                !string.IsNullOrEmpty(Label) &&
                !string.IsNullOrWhiteSpace(Label);
        }
    }
}
