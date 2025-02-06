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
        private readonly CollegeUnityDbContext _dbContext;
        public PostRepository(CollegeUnityDbContext context) : base(context)
        {
        }
    }
}
