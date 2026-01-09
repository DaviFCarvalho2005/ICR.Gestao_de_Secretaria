using ICR.Infra;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ICR.Domain.Model.UserRoleAgreggate
{
    public class UserRoleAggregateRepository : IUserRoleAggregateRepository
    {
        private readonly ConnectionContext _context;

        public UserRoleAggregateRepository(ConnectionContext context)
        {
            _context = context;
        }

        // ================= USER =================

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(long userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User? GetUserById(long userId)
        {
            return _context.Users
                .FirstOrDefault(u => u.Id == userId);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users
                .FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        // ================= ROLE =================

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(long roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
            if (role == null) return;

            _context.Roles.Remove(role);
            _context.SaveChanges();
        }

        public Role? GetRoleById(long roleId)
        {
            return _context.Roles
                .FirstOrDefault(r => r.Id == roleId);
        }

        public Role? GetRoleByName(string name)
        {
            return _context.Roles
                .FirstOrDefault(r => r.Name == name);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        // ================= USER_ROLE =================

        public void AddUserRole(long userId, long roleId)
        {
            var exists = _context.UserRoles
                .Any(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (exists) return;

            var userRole = new UserRole(userId, userId, roleId);
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public void RemoveUserRole(long userId, long roleId)
        {
            var userRole = _context.UserRoles
                .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null) return;

            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        public List<Role> GetRolesByUserId(long userId)
        {
            return _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Join(
                    _context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => r
                )
                .ToList();
        }

        public List<User> GetUsersByRoleId(long roleId)
        {
            return _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Join(
                    _context.Users,
                    ur => ur.UserId,
                    u => u.Id,
                    (ur, u) => u
                )
                .ToList();
        }
    }
}
