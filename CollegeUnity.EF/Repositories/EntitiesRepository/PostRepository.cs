using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly CollegeUnityDbContext _context;
        public PostRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> isExist(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            return post != null;
        }

        public async Task<bool> isMyPost(int staffId, int postId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == postId && p.StaffId == staffId);

            return post != null;
        }

        public async Task<PagedList<Post>> GetVotesWithConditionsAsync(
            Expression<Func<Post, bool>> condition,
            QueryStringParameters queryParams,
            bool trackChanges = false,
            params Expression<Func<Post, object>>[] includes)
        {
            var query = _context.Posts.IgnoreAutoIncludes().AsQueryable();

            query = query.IncludeOneOrMany(includes);

            query = query.Include(p => p.Votes)
             .ThenInclude(vote => vote.SelectedBy);

            query = query.Where(condition);

            query = query.OrderBy(queryParams);

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.AsPagedListAsync(queryParams);
        }
    }
}
