using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;

namespace ToLetBdInterface
{
    public interface IPostRepository : IRepository<Post>
    {
    
        List<Post> GetPostByArea(String area);
        List<Post> GetByUserId(int id);
        List<Post> GetPendingPost();
        List<Post> GetPublishPost();
        int DisablePost(int id);
        int PublishPost(int id);

    }
}
