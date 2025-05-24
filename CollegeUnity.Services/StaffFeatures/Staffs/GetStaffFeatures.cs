using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Staffs;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StaffFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions;
using CollegeUnity.Core.MappingExtensions.StaffExtensions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Staffs
{
    public class GetStaffFeatures : IGetStaffFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public GetStaffFeatures(IRepositoryManager repositoryManager)
        {
           _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GetStaffDto>> GetStaffs(int staffId, GetStaffParameters parameters)
        {
            Expression<Func<Staff, bool>> conditions = 
                s => s.AccountStatus == Core.Enums.AccountStatus.Active && 
                s.Id != staffId && !s.Roles.Contains(Roles.Admin);

            if (parameters.Role.HasValue)
            {
                conditions = conditions.And(r => r.Roles.Contains(parameters.Role.Value));
            }

            if (!string.IsNullOrEmpty(parameters.FullName))
            {
                conditions = conditions.And(s =>
                    (s.FirstName + " " + s.MiddleName + " " + s.LastName).StartsWith(parameters.FullName));
            }

            var staffs = await _repositoryManager.StaffRepository.GetRangeByConditionsAsync(conditions, parameters);
            return staffs.MapPagedList(StaffExtension.GetStaff);
        }
    }
}
