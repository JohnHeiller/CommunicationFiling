using CommunicationFiling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Contracts
{
    public interface ICorrespondenceTypeRepo
    {
        CorrespondenceType Get(long id);
        IEnumerable<CorrespondenceType> Get();
        IEnumerable<CorrespondenceType> Get(Expression<Func<CorrespondenceType, bool>> predicate);
        IEnumerable<CorrespondenceType> GetPaging(Expression<Func<CorrespondenceType, bool>> predicate, int page, int size, Expression<Func<CorrespondenceType, bool>> filterAttribute, bool descending);
        long Create(CorrespondenceType entity);
        long Count();
        long Count(Expression<Func<CorrespondenceType, bool>> predicate);
        void Delete(CorrespondenceType entity);
        void Update(CorrespondenceType entity);
    }
}
