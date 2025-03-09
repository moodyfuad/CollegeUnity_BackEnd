using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Contract.SharedFeatures.Posts.Comments;
using CollegeUnity.Contract.SharedFeatures.Posts.Votes;
using CollegeUnity.Contract.StudentFeatures.Account;
using CollegeUnity.Services.AdminFeatures.Communites;
using CollegeUnity.Services.AdminFeatures.Staffs;
using CollegeUnity.Services.SharedFeatures.Authentication;
using CollegeUnity.Services.SharedFeatures.Posts.Comments;
using CollegeUnity.Services.SharedFeatures.Posts.Votes;
using CollegeUnity.Services.StudentFeatures.Account;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services
{
    public static class FeaturesDIExtensions
    {
        public static IServiceCollection AddFeaturesDI(this IServiceCollection services)
        {
            //Admin Features
            services.AddScoped<IManageCommunityFeatures, ManageCommunityFeatures>();

            // Shared Features
            services.AddScoped<ILoginFeatures, LoginFeatures>();
            services.AddScoped<IForgetPasswordFeatures, ForgetPasswordFeatures>();
            services.AddScoped<ICommentFeatures, CommentFeatures>();
            services.AddScoped<IVoteFeatures, VoteFeatures>();
            services.AddScoped<IManageStaffFeatures, ManageStaffFeatures>();

            // Student Features
            services.AddScoped<ISignUpFeatures, SignUpFeature>();

            // Staff Features
            return services;
        }
    }
}
