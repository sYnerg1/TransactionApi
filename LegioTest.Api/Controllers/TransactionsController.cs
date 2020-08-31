using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using LegioTest.Api.ModelValidation;
using LegioTest.Data.Models;
using LegioTest.Domain.ModelsDTO;
using LegioTest.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace LegioTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transService;

        public TransactionsController(ITransactionService transService)
        {
            _transService = transService;
        }

        /// <summary>
        /// Get transactions after filtering from DB.
        /// </summary>
        /// <response code="200">List of transactions</response> 
        /// <response code="400">If filter isn't correct</response> 
        /// <response code="401">If JWT isn't correct</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> Get([FromQuery] FilterDTO filter)
        {
            var validator = new FilterDTOValidator();
            var validationResult = await validator.ValidateAsync(filter);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);


            try
            {
                var result = await _transService.Find(filter);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get transactions after filtering from DB.
        /// </summary>
        /// <response code="200">Download exel file with transactions after filtering</response> 
        /// <response code="400">If filter isn't correct</response> 
        /// <response code="401">If JWT isn't correct</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetExcel([FromQuery] ExcelFilterDTO filter)
        {

            var validator = new ExcelFilterValidator();
            var validationResult = await validator.ValidateAsync(filter);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                byte[] result = await _transService.CreateCSV(filter);

                return File(result, "text/csv","transactins.csv");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>
        /// Add transactions from .csv file.
        /// </summary>
        /// <response code="201">Transactions added</response> 
        /// <response code="400">If file is null</response> 
        /// <response code="401">If JWT isn't correct</response> 
        /// <response code="500">Data base error</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> Post(IFormFile myFile)
        {
            if(myFile == null)
            {
                return BadRequest("File not exist");
            }

            try
            {
                bool result = await _transService.ReadFile(myFile);

                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>
        /// Edit transaction.
        /// </summary>
        /// <response code="200">Transaction edited</response> 
        /// <response code="400">If id doesn't match</response> 
        /// <response code="401">If JWT isn't correct</response> 
        /// <response code="500">Data base error</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TransactionDTO transactionDTO)
        {
            if(transactionDTO.Id!=id)
            {
                return BadRequest("Id doesn't match");
            }

            try
            {                
                bool result = await _transService.EditAsync(id, transactionDTO);

                if(result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }                
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete transaction.
        /// </summary>
        /// <response code="200">Transaction deleted</response> 
        /// <response code="400">If id isn't correct</response> 
        /// <response code="401">If JWT isn't correct</response> 
        /// <response code="500">Data base error</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool result = await _transService.DeleteAsync(id);
                if(result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Transaction doesn't exist");
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
