namespace Backend.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public int CaloriesPerMinute { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}