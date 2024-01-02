using api.DTOs.Request;
using api.DTOs.Response;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StockController> _logger;
        private readonly IMapper _mapper;

        public StockController(IStockRepository stockRepository, ILogger<StockController> logger, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetDataResponse<Stock>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepository.GetAllAsync(query);

            var response = new GetDataResponse<Stock>
            {
                Count = stocks.Count,
                Data = stocks
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StockDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<StockDto>(stock);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StockDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateStockRequest request)
        {
            var stock = _mapper.Map<Stock>(request);

            var response = await _stockRepository.CreateAsync(stock);

            var stockDto = _mapper.Map<StockDto>(response);

            return Ok(stockDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(StockDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequest request)
        {

            var response = await _stockRepository.UpdateAsync(id, request);

            var stockDto = _mapper.Map<StockDto>(response);

            return Ok(stockDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _stockRepository.DeleteAsync(id));
        }

    }
}
