using System;

namespace Orient.Base.Net.Core.Api.Core.Common.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public static class RoleConstants
    {
        /// <summary>
        /// Super Admin
        /// </summary>
        public const string SA = "075c1072-92a2-4f99-ac11-bf985b23da6e";
        public static Guid SuperAdminId = new Guid(SA);

        /// <summary>
        /// Admin
        /// </summary>
        public const string Admin = "506F8FC8-816A-4505-82ED-E271447938FE";
        public static Guid AdminId = new Guid(Admin);

        /// <summary>
        /// Shipper
        /// </summary>
        public const string Shipper = "5D8C397B-5F6B-42D6-87F6-1D73870F8798";
        public static Guid ShipperId = new Guid(Shipper);

        /// <summary>
        /// User
        /// </summary>
        public const string User = "F67AFD57-D2B5-41FF-8FDB-A7F08B57EA58";
        public static Guid UserId = new Guid(User);
    }
}
