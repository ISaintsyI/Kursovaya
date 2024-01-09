using Domain.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Validation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase
    {
        private readonly IValidator<CarDto> _validator;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ValidationController> _logger;

        public ValidationController(IValidator<CarDto> validator, IConfiguration configuration, ILogger<ValidationController> logger)
        {
            _validator = validator;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("dto")]
        public async Task<IActionResult> ValidateDto([FromBody] CarDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            _logger.LogInformation("Validated dto object");

            if (validationResult.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

        [HttpGet]
        [Route("plate/{plateNumber}")]
        public IActionResult ValidatePlateNumber([FromRoute] string plateNumber)
        {
            var regex = new Regex($"{_configuration["PlateRegex"]}");
            var match = regex.Match(plateNumber);

            _logger.LogInformation("Validated plate number");

            if(match.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
