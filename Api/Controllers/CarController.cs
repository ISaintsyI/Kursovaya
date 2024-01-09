using Data.Interfaces;
using Domain.Dto;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _cars;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CarController(ICarRepository cars, HttpClient httpClient, IConfiguration configuration)
        {
            _cars = cars;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarDto dto)
        {
            var validationUrl = _configuration["Services:Validation"];
            var jsonRequest = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{validationUrl}dto", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(await response.Content.ReadAsStringAsync());
            }

            var car = new Car
            {
                Id = Guid.NewGuid(),
                Owner = dto.Owner,
                Brand = dto.Brand,
                Color = dto.Color,
                Model = dto.Model,
                PlateNumber = dto.PlateNumber,
                Weight = dto.Weight,
            };

            var createdCar = await _cars.Create(car);

            return Ok(createdCar);
        }

        [HttpGet]
        [Route("{plateNumber}")]
        public async Task<IActionResult> GetByPlateNumber([FromRoute] string plateNumber)
        {
            var validationUrl = _configuration["Services:Validation"];

            var response = await _httpClient.GetAsync($"{validationUrl}plate/{plateNumber}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(await response.Content.ReadAsStringAsync());
            }

            var car = await _cars.GetByPlateNumber(plateNumber);

            if (car is null)
                return NotFound();

            return Ok(car);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCar([FromRoute] Guid id)
        {
            var deletedCar = await _cars.Delete(id);

            return Ok(deletedCar);
        }
    }
}
