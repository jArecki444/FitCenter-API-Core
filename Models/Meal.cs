namespace Backend.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}