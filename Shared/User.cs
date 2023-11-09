using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFullStackCrud.Shared
{
    public class User
    {
        public int ID { get; set; }
        public string firstname { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;


        // A collection of WaterIntake objects related to this user
        public List<WaterIntake> WaterIntakes { get; set; } = new List<WaterIntake>();

        // Constructor for the User class
        public User()
        {
            WaterIntakes = new List<WaterIntake>();
        }
    }
}
