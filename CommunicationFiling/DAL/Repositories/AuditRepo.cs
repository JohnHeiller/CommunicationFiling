using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationFiling.DAL.Repositories
{
    public class AuditRepo : IAuditRepo
    {
        readonly CommFilingContext _context;

        public AuditRepo(CommFilingContext context)
        {
            _context = context;
        }

        public Audit Get(long id)
        {
            return _context.Audits
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Audit> Get()
        {
            return _context.Audits
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Audit> Get(Expression<Func<Audit, bool>> predicate)
        {
            return _context.Audits
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Audit> GetPaging(Expression<Func<Audit, bool>> predicate, int page, int size, Expression<Func<Audit, bool>> filterAttribute, bool descending)
        {
            var query = _context.Audits.AsQueryable();

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

        public long Create(Audit entity)
        {
            _context.Audits.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.Audits.Count();
        }

        public long Count(Expression<Func<Audit, bool>> predicate)
        {
            return _context.Audits.Count(predicate);
        }

        public void Delete(Audit entity)
        {
            _context.Audits.Remove(entity);
        }

        public void Update(Audit entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
