namespace SpecificationPatternConsoleApp.Database.Entites
{
    public class CarEntity
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Fuel { get; set; }
        public int Horsepower { get; set; }
    }
}