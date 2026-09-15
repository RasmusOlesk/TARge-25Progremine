namespace TARge25Shop.Models.Spaceship
{
    internal class SpaceshipDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ShipType { get; set; }
        public int Crew { get; set; }
        public int EnginePower { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}