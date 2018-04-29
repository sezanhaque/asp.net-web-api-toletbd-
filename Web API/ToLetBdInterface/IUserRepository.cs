using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;

namespace ToLetBdInterface
{
    public interface IUserRepository : IRepository<User>
    {
        int ChangePassById(int id, String pass);
        User GetByEmailAndPass(User u);
        List<User> GetByUserTypeId(int id);
        int UpdateWithImg(User user);
        int totalHouseOwner();
        int totalRenter();


    }
}
