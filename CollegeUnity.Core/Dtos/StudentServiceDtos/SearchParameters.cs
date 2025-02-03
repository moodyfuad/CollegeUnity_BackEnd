using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.StudentServiceDtos
{
    public class SearchParameters
    {
        public string? CardId {get;set;}
        public string? FirstName {get;set;}
        public string? MiddleName {get;set;}
        public string? LastName {get;set;}
        public string? Email {get;set;}
        public string? Phone {get;set;}
        public Level? Level {get;set;}
        public Major? Major {get;set;}
        public AcceptanceType? AcceptanceType {get;set;}
        public Gender? Gender {get;set;}
        public bool? IsLevelEditable {get;set;}
        public AccountStatus? AccountStatus { get;set;}
    }

}
