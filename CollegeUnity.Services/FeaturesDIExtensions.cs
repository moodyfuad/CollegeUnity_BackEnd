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
using EmailService.EmailService;
using EmailService;
using Microsoft.Extensions.DependencyInjection;
using CollegeUnity.Contract.StudentFeatures.Request;
using CollegeUnity.Services.StudentFeatures.Requests;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Services.StudentFeatures.Subjects;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Services.AdminFeatures.Subjects;
using CollegeUnity.Contract.StudentFeatures.Courses;
using CollegeUnity.Services.StudentFeatures.Courses;

namespace CollegeUnity.Services
{
    public static class FeaturesDIExtensions
    {
        public static IServiceCollection AddFeaturesLayerDI(this IServiceCollection services)
        {
            services.AddSharedFeaturesDI();

            services.AddAdminFeaturesDI();

            services.AddStudentFeaturesDI();

            services.AddStaffFeaturesDI();

            return services;
        }

        private static IServiceCollection AddSharedFeaturesDI(this IServiceCollection services)
        {
            // Shared Features
            services.AddScoped<ILoginFeatures, LoginFeatures>();
            services.AddScoped<IForgetPasswordFeatures, ForgetPasswordFeatures>();
            services.AddScoped<ICommentFeatures, CommentFeatures>();
            services.AddScoped<IVoteFeatures, VoteFeatures>();
            services.AddScoped<IEmailServices, EmailServices>();

            return services;
        }

        private static IServiceCollection AddAdminFeaturesDI(this IServiceCollection services)
        {
            // Admin Features
            services.AddScoped<IManageCommunityFeatures, ManageCommunityFeatures>();
            services.AddScoped<IManageStaffFeatures, ManageStaffFeatures>();
            services.AddScoped<IManageSubjectFeatures, ManageSubjectFeatures>();

            return services;
        }

        private static IServiceCollection AddStudentFeaturesDI(this IServiceCollection services)
        {
            // Student Features
            services.AddScoped<ISignUpFeatures, SignUpFeature>();
            services.AddScoped<IRequestsFeature, RequestsFeature>();
            services.AddScoped<IStudentSubjectFeatures, StudentSubjectFeatures>();
            services.AddScoped<IStudentCoursesFeatures, StudentCoursesFeatures>();

            return services;
        }

        private static IServiceCollection AddStaffFeaturesDI(this IServiceCollection services)
        {
            // Staff Features
            return services;
        }
    }
}
