using System.Collections.Generic;

namespace ICR.Domain.Model.UserRoleAgreggate
{
    public interface IUserRoleAggregateRepository
    {
        // ===== USER =====
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(long userId);
        User? GetUserById(long userId);
        User? GetUserByUsername(string username);
        List<User> GetUsers();

        // ===== ROLE =====
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(long roleId);
        Role? GetRoleById(long roleId);
        Role? GetRoleByName(string name);
        List<Role> GetRoles();

        // ===== USER_ROLE =====
        void AddUserRole(long userId, long roleId);
        void RemoveUserRole(long userId, long roleId);

        List<Role> GetRolesByUserId(long userId);
        List<User> GetUsersByRoleId(long roleId);
    }
}
