namespace backend.Dtos
{
    public class MealForCreationDto
    {
        public string Name { get; set; }
        public string Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
    }
}