using Astoneti.Microservice.Media.Data.Entities;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Data.Contracts
{
    public interface ICommentRepository
    {
        List<CommentEntity> GetList();
    }
}
