using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Data.Contracts;
using AutoMapper;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Business
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public List<CommentDto> GetList()
        {
            var items = _commentRepository.GetList();

            return _mapper.Map<List<CommentDto>>(
                items
            );
        }
    }
}
