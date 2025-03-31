using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EFC = Microsoft.EntityFrameworkCore.EF;

namespace CollegeUnity.EF.Extensions
{
    public static class RepositoriesExtensions
    {
        public static async Task<PagedList<T>> AsPagedListAsync<T>(this IQueryable<T>? source, QueryStringParameters queryString)
            where T : class
        {
            return await PagedList<T>.ToPagedListAsync(source, queryString);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T>? entity, QueryStringParameters queryString)
            where T : class
        {
            if (entity == null)
            {
                return entity;
            }
            else if (!string.IsNullOrEmpty(queryString.ThenBy))
            {
                return entity.OrderByWithThenBy(queryString);
            }
            else if (!string.IsNullOrEmpty(queryString.OrderBy))
            {
                try
                {
                    var property = typeof(T).GetProperty(queryString.OrderBy, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (property != null)
                    {
                        entity = queryString.DesOrder ?
                            entity.OrderByDescending(x => EFC.Property<object>(x, property.Name)) :
                            entity.OrderBy(x => EFC.Property<object>(x, property.Name));
                    }
                }
                catch
                {
                }
            }

            return entity;
        }

        public static IQueryable<T> IncludeOneOrMany<T>(this IQueryable<T> entity, params Expression<Func<T, object>>?[] includes)
            where T : class
        {
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    if (entity != null && entity.Any() && include != null)
                    {
                        entity = entity.Include(include);
                    }
                }
            }

            return entity;
        }

        public static IQueryable<T> Condition<T>(this IQueryable<T> source, params Expression<Func<T, bool>>[] conditions)
        {
            if (conditions is null || !conditions.Any())
            {
                return source;
            }

            foreach (var condition in conditions)
            {
                if (condition is null)
                {
                    continue;
                }

                source = source.Where(condition);
            }

            return source;
        }

        public static IQueryable<T> TrackChanges<T>(this IQueryable<T> source, bool trackChanges)
            where T : class
        {
            source = !trackChanges ? source.AsNoTracking() : source.AsTracking();
            return source;
        }

        private static IQueryable<T> OrderByWithThenBy<T>(this IQueryable<T>? entity, QueryStringParameters queryString)
        {
            if (entity != null && !string.IsNullOrEmpty(queryString.OrderBy))
            {
                try
                {
                    var propertyOrder = typeof(T).GetProperty(queryString.OrderBy, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    var propertyThen = typeof(T).GetProperty(queryString.ThenBy!, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (propertyOrder != null && propertyThen != null)
                    {
                        entity = !queryString.DesOrder ?
                            entity.OrderByDescending(x => EFC.Property<object>(x!, propertyOrder.Name))
                            .ThenBy(x => EFC.Property<object>(x!, propertyThen.Name)) :
                            entity.OrderBy(x => EFC.Property<object>(x!, propertyOrder.Name))
                        .ThenBy(x => EFC.Property<object>(x!, propertyThen.Name));
                    }
                }
                catch
                {
                }
            }

                return entity;
        }
    }
}
