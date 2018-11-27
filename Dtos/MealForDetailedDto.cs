namespace backend.Dtos
{
    public class MealForDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
    }
}