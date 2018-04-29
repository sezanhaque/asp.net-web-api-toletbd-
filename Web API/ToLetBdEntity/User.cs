using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToLetBdEntity
{
    public class User : Entity
    {
        public int Id { get; set; }
  
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format")]      
        public string Email { get; set; }
        [Required,MinLength(6)]
        public string  Password { get; set; }
    
        public string ImgPath { get; set; }
   
        public string Gender { get; set; }
   
        public string PhnNo { get; set; }

        [Required(ErrorMessage = "Account type must be selected!!")]
        public int UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]

        public UserType UserType { get; set; }
        public List<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

       

    }
}
