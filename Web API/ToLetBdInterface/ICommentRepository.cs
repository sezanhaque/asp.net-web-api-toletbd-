using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;

namespace ToLetBdInterface
{
    public interface ICommentRepository:IRepository<Comment>
    {
        List<Comment> GetByPostId(int id);
    }
}
