using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/{msg}")]
        public IActionResult Get(string msg){
            return Ok(msg);
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")] // The path/router that go to be send to here.
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber)) 
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber); 
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid Input.");
        }

        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public IActionResult GetSub(string firstNumber, string secondNumber) {
            
            decimal result;
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber)) {
                result = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(result.ToString());
            }

            return BadRequest("Invalid input");
        }

        [HttpGet("mul/{firstNumber}/{secondNumber}")]
        public IActionResult GetMul(string firstNumber, string secondNumber){
            
            decimal result;
            
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber)) {
                result = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("div/{firstNumber}/{secondNumber}")]
        public IActionResult GetDiv(string firstNumber, string secondNumber){

            if(secondNumber == "0"){
                throw new DivideByZeroException();
            }

            decimal result;
            
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber)) {
                result = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(result.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("med/{firstNumber}/{secondNumber}")]
        public IActionResult GetMed(string firstNumber, string secondNumber) {
            
            decimal sum;
            decimal media;

            if(IsNumeric(firstNumber) && IsNumeric(secondNumber)) {
                sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                media = sum/2;
                return Ok(media.ToString()); 
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("sqrt/{firstNumber}")]
        public IActionResult GetSqrt(string firstNumber) {

            decimal result;

            if(IsNumeric(firstNumber)){
                result = ConvertToDecimal(firstNumber);
                double num = Math.Sqrt(Convert.ToDouble(result));
                return Ok(num.ToString());
            }        

            return BadRequest("Invalid Input");
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

    }
}
