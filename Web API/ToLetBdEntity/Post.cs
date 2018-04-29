using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToLetBdEntity
{
    public class Post : Entity
    {
        public int Id { get; set; }
        public DateTime PostDateTime { get; set; }

        [Required]
        public double RoomRent { get; set; }

        [Required]
        public int NoOfRoom { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ShortDesc { get; set; }

        public string Status { get; set; }
        public int Views { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public ICollection<Comment> Comments { get; set; }
        public List<PostImage> PostImages { get; set; }
        public User User { get; set; }
        


    }
}
