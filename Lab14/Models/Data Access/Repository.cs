using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        
        //Connects to the database and names it dbset
        
        protected ContactContext context { get; set; }
        private DbSet<T> dbset { get; set; }

        public Repository(ContactContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        
        
        
        //List method (returns a list of records)
        
        
        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.ToList();
        }
        






        //Get Methods (each get a single record)

        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }

        public virtual T Get(int id) => dbset.Find(id);








        //Insert, Update, Delete Methods

        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);

        
        
        
        //Save Method

        public virtual void Save() => context.SaveChanges();






        //BuildQuery Method (used by other methods in this class [get() and list()])

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;

            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
            if (options.HasOrderBy)
                query = query.OrderBy(options.OrderBy);

            return query;
        }
    }
}
