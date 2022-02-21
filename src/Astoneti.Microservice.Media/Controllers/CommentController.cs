using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<CommentModel>), StatusCodes.Status200OK)]
        public IActionResult GetList()
        {
            return Ok(
                _mapper.Map<IList<CommentModel>>(
                    _commentService.GetList()
                )
            );
        }
    }
}
