﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.RepositroryInterfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T Entity)
        {
            try
            {
                await _dbSet.AddAsync(Entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding entity", ex);
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all entities", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entity with ID {id}: {ex.Message}", ex);
            }
        }
        public async Task UpdateAsync(T entity)
        {
            try
            {

                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity", ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id) ?? throw new Exception("Wrong Input");
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting entity with ID {id}", ex);
            }
        }
        public async Task<IEnumerable<T>> GetAllByUserIdAsync(int userId)
        {
            try
            {
                return await _dbSet
                    .Where(e => EF.Property<int>(e, "UserId") == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entities for UserId {userId}", ex);
            }
        }

    }
}
