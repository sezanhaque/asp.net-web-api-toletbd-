using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;
using ToLetBdInterface;

namespace ToLetBdRepository
{
    public class CommentRepository : Repository<Comment>,ICommentRepository
    {
        ToLetBdDbContext context = new ToLetBdDbContext();

        public List<Comment> GetByPostId(int id)
        {
            return this.context.Comments.Where(c => c.PostId == id).ToList();
        }


    }
}
