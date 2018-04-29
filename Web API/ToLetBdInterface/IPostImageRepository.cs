using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;

namespace ToLetBdInterface
{
    public interface IPostImageRepository : IRepository<PostImage>
    {
     
         PostImage GetImgByPostId(int id);
         List<PostImage> GetAllImgByPostId(int id);
    }
}
