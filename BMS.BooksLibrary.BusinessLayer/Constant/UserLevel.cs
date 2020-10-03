namespace BMS.BusinessLayer.Constant
{
  
    public static class UserLevel
    {
        public struct RegistrationProcess
        {
            public const string WaitingToBeApproved = "WaitingToBeApproved";
            public const string NotExits = "NotExits";
            public const string Approved = "Approved";
            public const string WrongPassword = "WrongPassword";
        }

        public struct AccessArea
        {
            public const string LibraryUser = "LibraryUser";
            public const string LibraryAdmin = "LibraryAdmin";
        }
        
        
    }
}