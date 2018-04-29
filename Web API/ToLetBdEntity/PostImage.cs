using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToLetBdEntity
{
    public class PostImage : Entity
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]

        public Post Post { get; set; }
        
    }
}
