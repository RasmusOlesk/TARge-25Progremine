namespace TARge25Shop.Models.Realestate
{
    public class RealestateCreateModifyViewModel
    {
        public Guid? Id { get; set; }
        public string Area { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int RoomNumber { get; set; }
        public string BuildingType { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
