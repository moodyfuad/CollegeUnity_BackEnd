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
using CollegeUnity.Contract.AdminFeatures.Courses;
using CollegeUnity.Services.AdminFeatures.Courses;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Contract.AdminFeatures.Student;
using CollegeUnity.Services.AdminFeatures.Students;
using CollegeUnity.Contract.AdminFeatures.ScheduleFiles;
using CollegeUnity.Services.AdminFeatures.ScheduleFiles;
using CollegeUnity.Contract.SharedFeatures.ScheduleFiles;
using CollegeUnity.Services.SharedFeatures.ScheduleFiles;
using CollegeUnity.Services.StudentFeatures.Communites;
using CollegeUnity.Contract.SharedFeatures;
using Microsoft.Extensions.Options;
using CollegeUnity.Services.SharedFeatures;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Services.SharedFeatures.Chats;
using CollegeUnity.Contract.StaffFeatures.Chats;
using CollegeUnity.Services.StaffFeatures.Chat;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Services.PostFilesFeatures;
using CollegeUnity.Contract.AdminFeatures.FeedBacks;
using CollegeUnity.Services.AdminFeatures.FeedBacks;
using Microsoft.AspNetCore.SignalR;
using CollegeUnity.Services.Hubs;
using CollegeUnity.Services.Hubs.Connection;
using CollegeUnity.Services.Hubs.HubFeatures;
using CollegeUnity.Contract;
using CollegeUnity.Contract.StaffFeatures.Request;
using CollegeUnity.Services.StaffFeatures.Requests;
using CollegeUnity.Contract.SharedFeatures.Messages;
using CollegeUnity.Services.SharedFeatures.Messages;
using CollegeUnity.Contract.StaffFeatures.Students;
using CollegeUnity.Services.StaffFeatures;
using CollegeUnity.Contract.AdminFeatures.Subjects;
using CollegeUnity.Services.StaffFeatures.Subjects;
using CollegeUnity.Contract.SharedFeatures.Helpers;
using CollegeUnity.Services.SharedFeatures.Helpers;
using CollegeUnity.Contract.StaffFeatures.Account;
using CollegeUnity.Services.StaffFeatures.Account;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Services.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Services.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Contract.StaffFeatures.Staffs;
using CollegeUnity.Services.StaffFeatures.Staffs;

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

            // todo: delete this testing DI
            services.AddScoped<ITesting, Testing>();

            return services;
        }

        private static IServiceCollection AddSharedFeaturesDI(this IServiceCollection services)
        {
            // Shared Features
            services.AddSignalR();
            services.AddSingleton<IConnectionManager, ConnectionManager>();
            services.AddScoped<IMessageFeatures, MessageFeatures>();
            services.AddScoped<IChatListNotificationFeatures, ChatListNotificationFeatures>();
            services.AddScoped<IChatHubFeatures, ChatHubFeatures>();
            services.AddScoped<IActionFilterHelpers, ActionFilterHelpers>();
            services.AddScoped<IGetChatList, GetChatList>();
            services.AddScoped<ILoginFeatures, LoginFeatures>();
            services.AddScoped<IForgetPasswordFeatures, ForgetPasswordFeatures>();
            services.AddScoped<ICommentFeatures, CommentFeatures>();
            services.AddScoped<IVoteFeatures, VoteFeatures>();
            services.AddScoped<IGetScheduleFilesFeatures, GetScheduleFilesFeatures>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IFilesFeatures, FilesFeatures>();
            services.AddScoped<ISearchUsersFeature, SearchUsersFeature>();

            return services;
        }

        private static IServiceCollection AddAdminFeaturesDI(this IServiceCollection services)
        {
            // Admin Features
            services.AddScoped<IManageCommunityFeatures, ManageCommunityFeatures>();
            services.AddScoped<IManageStaffFeatures, ManageStaffFeatures>();
            services.AddScoped<IManageSubjectFeatures, ManageSubjectFeatures>();
            services.AddScoped<IManageCoursesFeatures, ManageCoursesFeatures>();
            services.AddScoped<IManageScheduleFilesFeatures, ManageScheduleFilesFeatures>();
            services.AddScoped<IManageFeedBackFeatures, ManageFeedBackFeatures>();


            return services;
        }

        private static IServiceCollection AddStudentFeaturesDI(this IServiceCollection services)
        {
            // Student Features
            services.AddScoped<ISignUpFeatures, SignUpFeature>();
            services.AddScoped<IStudentRequestsFeatures, StudentRequestsFeatures>();
            services.AddScoped<IStudentSubjectFeatures, StudentSubjectFeatures>();
            services.AddScoped<IStudentCoursesFeatures, StudentCoursesFeatures>();
            services.AddScoped<IManageStudentFeatures, ManageStudentFeatures>();
            services.AddScoped<IStudentCommunityFeatures, StudentCommunityFeatures>();
            services.AddScoped<IStudentProfileFeatures, StudentProfileFeatures>();

            return services;
        }

        private static IServiceCollection AddStaffFeaturesDI(this IServiceCollection services)
        {
            // Staff Features
            services.AddScoped<IGetStaffFeatures, GetStaffFeatures>();
            services.AddScoped<IChatManagementFeatures, ChatManagementFeatures>();
            services.AddScoped<IGetMyStudents, GetMyStudents>();
            services.AddScoped<ICreatePostFeatures, CreatePostFeatures>();
            services.AddScoped<IPostVoteFeatures, PostVoteFeatures>();
            services.AddScoped<IGetMySubjects, GetMySubjects>();
            services.AddScoped<IStaffRequestsFeatures, StaffRequestsFeatures>();
            services.AddScoped<IStaffProfileFeatures, StaffProfileFeatures>();
            return services;
        }
    }
}
