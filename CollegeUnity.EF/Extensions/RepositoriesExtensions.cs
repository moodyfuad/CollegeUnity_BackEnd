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
            if (entity is null)
            {
                return entity;
            }

            PropertyInfo[] properties = typeof(T).GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

            if (!string.IsNullOrEmpty(queryString.OrderBy))
            {
                var orderByProperty = properties.FirstOrDefault(
                    p => p.Name.Contains(queryString.OrderBy, StringComparison.OrdinalIgnoreCase));

                if (orderByProperty is not null)
                {
                    entity = queryString.DesOrder ?
                        entity.OrderByDescending(x => EFC.Property<object>(x, orderByProperty.Name))
                        :
                        entity.OrderBy(x => EFC.Property<object>(x, orderByProperty.Name));
                }
            }

            if (!string.IsNullOrEmpty(queryString.ThenBy) && entity is IOrderedQueryable<T> orderedEntity)
            {
                var thenByProperty = properties.FirstOrDefault(
                    p => p.Name.Contains(queryString.ThenBy, StringComparison.OrdinalIgnoreCase));

                entity = orderedEntity.ApplyThenBy(thenByProperty);
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

        private static IQueryable<T>? ApplyThenBy<T>(this IOrderedQueryable<T>? entity, PropertyInfo? thenByProperty)
        {
            if (entity is not null && thenByProperty is not null)
            {
                try
                {
                    entity = entity.ThenBy(x => EFC.Property<object>(x!, thenByProperty.Name));
                }
                catch
                {
                }
            }

            return entity;
        }
    }
}
