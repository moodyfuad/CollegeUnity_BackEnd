using CollegeUnity.Contract.AdminFeatures.Posts;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.Post
{
    public class ManagePostFeatures : IManagePostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public ManagePostFeatures(IRepositoryManager repositoryManager) 
        {
            _repositoryManager = repositoryManager;
        }

        //public Task<PagedList<GPostDto>> GetPostsDetails(DateTime dateTime, GetPostsDetailsParameters parameters)
        //{
        //    var posts = _repositoryManager.
        //}
    }
}
