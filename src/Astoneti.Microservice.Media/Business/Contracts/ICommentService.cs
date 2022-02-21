using Astoneti.Microservice.Media.Business.Models;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Business.Contracts
{
    public interface ICommentService
    {
        List<CommentDto> GetList();
    }
}
