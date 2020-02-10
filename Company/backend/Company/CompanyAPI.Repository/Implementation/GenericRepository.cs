﻿using CompanyAPI.Database.Context;
using CompanyAPI.Domain.Models;
using CompanyAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompanyAPI.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly CompanyApiContext _context;
        private readonly DbSet<T> _dataset;

        public GenericRepository(CompanyApiContext context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dataset.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void DeleteAsync(T entity)
        {
            try
            {
                _context.Remove(entity);
                await SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _context
                                    .Set<T>()
                                    .AnyAsync(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _context
                                    .Set<T>()
                                    .Where(query)
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _context
                                    .Set<T>()
                                    .FirstOrDefaultAsync(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
