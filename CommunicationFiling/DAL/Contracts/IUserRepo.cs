using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IUserRepo
    {
        User Get(long id);
        IEnumerable<User> Get();
        IEnumerable<User> Get(Expression<Func<User, bool>> predicate);
        IEnumerable<User> GetPaging(Expression<Func<User, bool>> predicate, int page, int size, Expression<Func<User, bool>> filterAttribute, bool descending);
        long Create(User entity);
        long Count();
        long Count(Expression<Func<User, bool>> predicate);
        void Delete(User entity);
        void Update(User entity);
    }
}
