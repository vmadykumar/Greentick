using System;
using System.Collections.Generic;

namespace ComplyRite.Web.MVC.Facade.SchedulingMgmt
{

    public class UserBasicInfo
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string Fullname { set; get; }
        public string Photo { set; get; }
        public DateTime DOB { set; get; }
        public string UserName { set; get; }
        public string Suffix { set; get; }
        public string Prefix { set; get; }
        public string MaritalStatus { set; get; }
        public string Gender { set; get; }
        public string UUID { set; get; }
        public string ProfilePicPath { set; get; }
        public string UserType { set; get; }
        public string UserAbbr { get; set; }
    }

    public class UserContact
    {
        public string ContactType { set; get; }
        public string ContactValue { set; get; }
        public bool IsPrimary { set; get; }
    }

    public class UserAccount
    {
        public string LobCode { set; get; }
        public string LobName { set; get; }
        public List<UserLocation> Locations { set; get; }
    }
    public class UserLocation
    {
        public string LocationCode { set; get; }
        public string LocationName { set; get; }
        public string LOBCode { get; set; }
    }

    public class UserPreference
    {
        public string ModuleCode { set; get; }
        public string LobCode { set; get; }
    }

    public class UserDetailsModel
    {
        public UserBasicInfo UserBasicInfo { set; get; }
        public List<UserContact> UserContactInfo { set; get; }
        public List<UserAccount> UserAccounts { set; get; }
        public List<UserPreference> UserPreferences { set; get; }
    }
    public class AuditorModel
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string AccountAbbreviation { set; get; }
        public string UserName { set; get; }
        public string UUID { set; get; }
    }
}