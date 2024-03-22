using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.DataBase.Enums;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Seeding
{
    public class SeedRole
    {
        private readonly IRoleService _roleService;
        public SeedRole(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public void SeedRoles()
        {
            if (_roleService.GetAll().Any())
            {
                return;
            }

            List<Role> roles = new();

            roles.Add(new Role() { RoleName = Roles.RoleEnum.Employee });
            roles.Add(new Role() { RoleName = Roles.RoleEnum.Coordinator });
            roles.Add(new Role() { RoleName = Roles.RoleEnum.Administrator });

            foreach (var role in roles)
            {
                _roleService.Insert(role);
            }
        }
    }
}
