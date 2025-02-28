using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
