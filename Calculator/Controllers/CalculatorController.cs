using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessObject.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{

    [ApiController]
    public class CalculatorController : BaseController
    {
        private readonly ICalculator _calculator;


        public CalculatorController(ICalculator calculator)
        {
            _calculator = calculator;
        }

        /// <summary>
        /// AddNumber
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("AddNumber")]
        public async Task<IActionResult> add(int a, int b)
        {
             return Ok (await _calculator.add(a, b));
        }

    }
}