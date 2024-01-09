using Domain.Enums;

namespace Domain.Dto
{
    public class CarDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Owner { get; set; }
        public string PlateNumber { get; set; }
        public float Weight { get; set; }
        public CarColor Color { get; set; }
    }
}
