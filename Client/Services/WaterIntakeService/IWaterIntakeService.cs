namespace BlazorFullStackCrud.Client.Services.WaterIntakeService
{
    public interface IWaterIntakeService
    {
        List<WaterIntake> WaterIntakeRecords { get; set; }
        List<User> Users { get; set; }

        Task GetWaterIntakeRecords();
        Task<WaterIntake> GetSingleIntakeRecord(int id);
        Task CreateIntakeRecord(WaterIntake intakeRecord);
        Task UpdateIntakeRecord(WaterIntake intakeRecord);
        Task DeleteIntakeRecord(int id);
    }
}
