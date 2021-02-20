using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationFiling.DAL.Repositories
{
    public class FilingRepo : IFilingRepo
    {
        readonly CommFilingContext _context;

        public FilingRepo(CommFilingContext context)
        {
            _context = context;
        }

        public Filing Get(long id)
        {
            return _context.Filings
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Filing> Get()
        {
            return _context.Filings
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Filing> Get(Expression<Func<Filing, bool>> predicate)
        {
            return _context.Filings
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Filing> GetPaging(Expression<Func<Filing, bool>> predicate, int page, int size, Expression<Func<Filing, bool>> filterAttribute, bool descending)
        {
            var query = _context.Filings.AsQueryable();

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

        public long Create(Filing entity)
        {
            if (entity.CorrespondenceTypeId > 0)
            {
                entity.Consecutive = GenerateConsecutive(entity.CorrespondenceTypeId);
            }
            _context.Filings.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.Filings.Count();
        }

        public long Count(Expression<Func<Filing, bool>> predicate)
        {
            return _context.Filings.Count(predicate);
        }

        public void Delete(Filing entity)
        {
            _context.Filings.Remove(entity);
        }

        public void Update(Filing entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private string GenerateConsecutive(long correspTypeId)
        {
            long countFilings = 0;
            string tmpConsecutive = "";
            bool isNewConsecutive = false;
            var correspType = _context.CorrespondenceTypes.AsNoTracking()
                                .FirstOrDefault(x => x.Id == correspTypeId);
            countFilings = _context.Filings.Where(x => x.CorrespondenceTypeId == correspTypeId)
                            .AsNoTracking().Count();
            while (isNewConsecutive == false)
            {
                tmpConsecutive = correspType.Code.Trim() + string.Format("{0, 0:D8}", (countFilings + 1));
                var existConsecutive = _context.Filings.AsNoTracking()
                                        .FirstOrDefault(x => x.Consecutive.Equals(tmpConsecutive));
                if (existConsecutive == null || existConsecutive.Id == 0)
                {
                    isNewConsecutive = true;
                }
                else
                {
                    countFilings += 1;
                }
            }
            return tmpConsecutive;
        }
    }
}
