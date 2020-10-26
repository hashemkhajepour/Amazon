using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        MyAmazon_Context db = new MyAmazon_Context();

        private Generic_Repo_Amazon<Users> _userRepository;
        public Generic_Repo_Amazon<Users> UserRepository
        {
            get
            {
                if(_userRepository == null)
                {
                    _userRepository = new Generic_Repo_Amazon<Users>(db);
                }
                return _userRepository;
            }
        }

        private Generic_Repo_Amazon<Roles> _rolesRepository;
        public Generic_Repo_Amazon<Roles> RolesRepository
        {
            get
            {
                if (_rolesRepository == null)
                {
                    _rolesRepository = new Generic_Repo_Amazon<Roles>(db);
                }
                return _rolesRepository;
            }
        }

        private IUserRepository _usersRepository;
        public IUserRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UserRepository(db);
                }
                return _usersRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
