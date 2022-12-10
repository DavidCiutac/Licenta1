using Microsoft.EntityFrameworkCore;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Repository
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ApplicationDbContext context;
        public readonly IStationsRepository stationsRepository;
        public ApplicationDbContext getContext()
            {
            return context;
            }

        public GenericRepository(ApplicationDbContext context, IStationsRepository stationsRepository)
        {
            this.context = context;
            this.stationsRepository = stationsRepository;
        }

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> MultipleAddAsync(List<T> entities)
        {
            foreach(var entity in entities)
            {
                await context.AddAsync(entity);
            }
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
             context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        

       
    }
}
