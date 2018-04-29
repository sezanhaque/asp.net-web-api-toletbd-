using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToLetBdEntity
{
    public class Comment:Entity
    {

        
        public int Id { get; set; }
        public string  Text { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        
       
    }
}
