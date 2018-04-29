using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;
using ToLetBdInterface;

namespace ToLetBdRepository
{
    public class PostImageRepository :Repository<PostImage> ,IPostImageRepository
    {
        ToLetBdDbContext context = new ToLetBdDbContext();
       
        

        public PostImage GetImgByPostId(int id)
        {
            return this.context.PostImages.First(p => p.PostId == id);
        }

        public List<PostImage> GetAllImgByPostId(int id)
        {
            return this.context.PostImages.Where(x => x.PostId == id).ToList();
        }


   
    }
}
