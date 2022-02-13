using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Controllers
{
    [Route("news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<NewsModel>), StatusCodes.Status200OK)]
        public IActionResult GetList()
        {
            return Ok(
                _mapper.Map<IList<NewsModel>>(
                    _newsService.GetList()
                )
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NewsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var item = _newsService.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(
                _mapper.Map<NewsModel>(
                    item
                )
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(NewsModel), StatusCodes.Status201Created)]
        public IActionResult Post(NewsPostModel model)
        {
            var item = _newsService.Add(model);

            return CreatedAtAction(
                nameof(Get),
                new { id = item.Id },
                _mapper.Map<NewsModel>(
                    item
                )
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Put(int id, NewsPutModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var item = _newsService.Edit(model);

            if (item == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var result = _newsService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
