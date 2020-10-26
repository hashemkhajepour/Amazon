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
