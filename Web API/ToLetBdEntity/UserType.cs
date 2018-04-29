using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToLetBdEntity
{
    public class UserType : Entity
    {
        public int Id { get; set; }
        public string  TypeName { get; set; }

        public List<User> Users { get; set; }
    }
}
