using CommunicationFiling.DAL.Contracts;
using CommunicationFiling.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommunicationFiling.DAL.Repositories
{
    public class UserRepo : IUserRepo
    {
        readonly CommFilingContext _context;

        public UserRepo(CommFilingContext context)
        {
            _context = context;
        }

        public User Get(long id)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> Get()
        {
            return _context.Users
                .Include(x => x.Audit)
                .Where(x => x.IsValid == true)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> predicate)
        {
            return _context.Users
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<User> GetPaging(Expression<Func<User, bool>> predicate, int page, int size, Expression<Func<User, bool>> filterAttribute, bool descending)
        {
            var query = _context.Users.AsQueryable();

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

        public long Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public long Count()
        {
            return _context.Users.Count();
        }

        public long Count(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Count(predicate);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
