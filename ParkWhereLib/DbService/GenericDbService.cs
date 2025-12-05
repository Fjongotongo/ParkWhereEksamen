using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkWhereLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib.DbService
{
    public class GenericDbService<T> where T : class
    {
        private readonly DbContextOptions<MyDbContext> _options;
        public GenericDbService(DbContextOptions<MyDbContext> options)
        {
            _options = options;
        }
       


        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new MyDbContext(_options))
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }
        public async Task AddObjectAsync(T obj)
        {
            using (var context = new MyDbContext(_options))
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }
        public async Task SaveObjects(List<T> objs)
        {
            using (var context = new MyDbContext(_options))
            {
                context.Set<T>().AddRange(objs);
                await context.SaveChangesAsync();
            }
        }

    }
}
