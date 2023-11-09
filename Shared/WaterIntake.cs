namespace BlazorFullStackCrud.Shared
{
    public class WaterIntake
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime IntakeDate { get; set; }
        public int ConsumedWater { get; set; } 

        // Navigation property to User
       public User? User { get; set; }
    }
}
