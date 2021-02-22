using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationFiling.DAL.Repositories
{
    public class CorrespondenceTypeRepo : ICorrespondenceTypeRepo
    {
        readonly CommFilingContext _context;

        public CorrespondenceTypeRepo(CommFilingContext context)
        {
            _context = context;
        }

        public CorrespondenceType Get(long id)
        {
            return _context.CorrespondenceTypes
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CorrespondenceType> Get()
        {
            return _context.CorrespondenceTypes
                .Include(x => x.Audit)
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<CorrespondenceType> Get(Expression<Func<CorrespondenceType, bool>> predicate)
        {
            return _context.CorrespondenceTypes
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<CorrespondenceType> GetPaging(Expression<Func<CorrespondenceType, bool>> predicate, int page, int size, Expression<Func<CorrespondenceType, bool>> filterAttribute, bool descending)
        {
            var query = _context.CorrespondenceTypes.AsQueryable();

            query = query.Where(predicate);

            if (descending)
            {
                query = query.OrderByDescending(filterAttribute);
            }
            else
            {
                query = query.OrderBy(filterAttribute);
            }

            return query.Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        public long Create(CorrespondenceType entity)
        {
            _context.CorrespondenceTypes.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.CorrespondenceTypes.Count();
        }

        public long Count(Expression<Func<CorrespondenceType, bool>> predicate)
        {
            return _context.CorrespondenceTypes.Count(predicate);
        }

        public void Delete(CorrespondenceType entity)
        {
            _context.CorrespondenceTypes.Remove(entity);
        }

        public void Update(CorrespondenceType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
