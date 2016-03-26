namespace SampleApp.Data.Services
{
    using Identity.Models;
    using Microsoft.AspNet.Identity;

    public class RoleService : RoleManager<Role>
    {
        public RoleService(IRoleStore<Role, string> store)
            : base(store)
        {}
    }
}