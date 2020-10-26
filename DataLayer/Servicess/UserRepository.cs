using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserRepository : IUserRepository
    {
        private MyAmazon_Context db;
        public UserRepository(MyAmazon_Context _context)
        {
            db = _context;
        }

        public List<Users> GetUserByRoles()
        {
            return db.Users.Include(u => u.Roles).ToList();
        }
    }
}
