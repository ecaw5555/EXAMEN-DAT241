
using Microsoft.AspNetCore.Mvc;

namespace calculadora2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            var app = builder.Build();
            app.MapControllers();
            app.Run();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly Calculadora _evaluator;

        public CalculatorController()
        {
            _evaluator = new Calculadora();
        }

        [HttpGet("infija")]
        public IActionResult EvaluaInfijo([FromQuery] string expression)
        {
            try
            {
                var result = _evaluator.infija(expression);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("prefija")]
        public IActionResult EvaluaPrefijo([FromQuery] string expression)
        {
            try
            {
                var result = _evaluator.prefija(expression);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}















