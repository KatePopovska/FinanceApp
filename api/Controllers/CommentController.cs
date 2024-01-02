using api.DTOs.Comment;
using api.DTOs.Request;
using api.DTOs.Response;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;
using api.Repository;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentController> _logger;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper, ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetDataResponse<Comment>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();

            var response = new GetDataResponse<Comment>
            {
                Count = comments.Count,
                Data = comments
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CommentDto>(comment);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateCommentRequest request)
        {
            var stock = _mapper.Map<Comment>(request);

            var response = await _commentRepository.CreateAsync(stock);

            var stockDto = _mapper.Map<CommentDto>(response);

            return Ok(stockDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest request)
        {
            var comment = _mapper.Map<Comment>(request);
            var response = await _commentRepository.UpdateAsync(id, comment);

            var stockDto = _mapper.Map<CommentDto>(response);

            return Ok(stockDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _commentRepository.DeleteAsync(id));
        }
    }
}
