using System.Collections.Generic;

namespace FitCenter.Models.Model
{
    public class User : Entity
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Weight { get; set; }
        public int TargetWeight { get; set; }
        public int Height { get; set; }
        public int BF { get; set; }
        public int TargetBF { get; set; }
        public int BicepCircuit { get; set; }
        public int ForearmCircuit { get; set; }
        public int ChestCircuit { get; set; }
        public int HipCircuit { get; set; }
        public int WaistCircuit { get; set; }
        public int CalfCircuit { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Meal> Meals { get; set; }

    }
}