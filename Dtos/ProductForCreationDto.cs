namespace backend.Dtos
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
    }
}