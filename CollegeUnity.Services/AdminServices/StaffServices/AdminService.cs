using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.StaffExtensions;
using System.Linq.Expressions;

namespace CollegeUnity.Services.AdminServices
{
    public partial class AdminService : IAdminServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public AdminService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<ApiResponse<CreateStaffDto>> CreateStaffAccount(CreateStaffDto Dto)
        {
            Expression<Func<Staff, bool>> expression = 
                (staff =>
            staff.Email == Dto.Email ||
            staff.Phone == Dto.Phone);

            var searchResult = await SearchStaffBy(expression);
            if (searchResult.IsSuccess)
            {
                string msg = GetExistedProperty(searchResult.Data, Dto)?? "Staff member already exist";
                    
                return ApiResponse<CreateStaffDto>.BadRequest($"[{msg}] already exist");
            }

            Staff staff = Dto.MapTo<Staff>();

            try {
                staff = await _repositoryManager.StaffRepository.CreateAsync(staff);
                await _repositoryManager.SaveChangesAsync();
            } 
            catch (Exception ex) 
            {
                return ApiResponse<CreateStaffDto>.
                    InternalServerError(errors: ex.Data.ToString().Split().ToList());
            }
            return ApiResponse<CreateStaffDto>.Created(staff.MapTo<CreateStaffDto>());
        }

        public async Task<ApiResponse<IEnumerable<CreateStaffDto>>> GetAllStaff(StaffParameters staffParameters)
        {
            return await _GetAllStaff(staffParameters);
        }

        public async Task<ApiResponse<IEnumerable<Staff>>> SearchStaffBy(Expression<Func<Staff, bool>> expression, StaffParameters staffParameters = default)
        {
            staffParameters ??= new StaffParameters();
            var staff = await _repositoryManager.StaffRepository.GetRangeByConditionsAsync(expression, staffParameters);
            if (staff.Any())
            {
                return ApiResponse<IEnumerable<Staff>>.Success(staff);
            }
            else
            {
                return ApiResponse<IEnumerable<Staff>>.NotFound();
            }


        }

        public async Task<ApiResponse<IEnumerable<Student>>> SearchStudentsBy(string name, StudentParameters studentParameters)
        {
            var students = await _SearchStudentBy(name, studentParameters);
            return students.Any() ? ApiResponse<IEnumerable<Student>>.Success(data: students) : ApiResponse<IEnumerable<Student>>.NotFound();
                ;
        }

        async Task<ApiResponse<IEnumerable<Staff>>> IAdminServices.SearchStaffBy(string name, StaffParameters staffParameters)
        {
            IEnumerable<Staff> staffs = await _repositoryManager.StaffRepository.
                GetRangeByConditionsAsync( s => s.FirstName.Contains(name), staffParameters);

            if (staffs.Any()) return ApiResponse<IEnumerable<Staff>>.Success(staffs);

            return ApiResponse<IEnumerable<Staff>>.NotFound();
        }
    }
}
