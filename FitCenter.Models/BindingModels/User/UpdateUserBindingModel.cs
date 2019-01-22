using System;
using System.Collections.Generic;
using System.Text;

namespace FitCenter.Models.BindingModels.User
{
    public class UpdateUserBindingModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string BodyType { get; set; }
        public string Gender { get; set; }
        public string WeightTarget { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int TotalDailyEnergyExpenditure { get; set; }
        public int BicepCircuit { get; set; }
        public int ForearmCircuit { get; set; }
        public int ChestCircuit { get; set; }
        public int HipCircuit { get; set; }
        public int WaistCircuit { get; set; }
        public int CalfCircuit { get; set; }
    }
}
