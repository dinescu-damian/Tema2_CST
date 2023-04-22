using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;

        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(x => x.Username == username);
        }
    }
}
