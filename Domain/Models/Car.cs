using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Car : IHaveId
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Owner { get; set; }
        public string PlateNumber { get; set; }
        public float Weight { get; set; }
        public CarColor Color { get; set; }
    }
}
