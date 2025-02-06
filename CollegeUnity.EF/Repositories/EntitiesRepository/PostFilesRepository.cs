using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class PostFilesRepository : BaseRepository<PostFile>, IPostFilesRepository
    {
        private readonly CollegeUnityDbContext _context;
        public PostFilesRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<PostFile> postFiles)
        {
            await _context.PostFiles.AddRangeAsync(postFiles);
        }
    }
}
