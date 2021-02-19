using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface IUserRoleRepo
    {
        UserRole Get(long id);
        IEnumerable<UserRole> Get();
        IEnumerable<UserRole> Get(Expression<Func<UserRole, bool>> predicate);
        IEnumerable<UserRole> GetPaging(Expression<Func<UserRole, bool>> predicate, int page, int size, Expression<Func<UserRole, bool>> filterAttribute, bool descending);
        long Create(UserRole entity);
        long Count();
        long Count(Expression<Func<UserRole, bool>> predicate);
        void Delete(UserRole entity);
        void Update(UserRole entity);
    }
}
