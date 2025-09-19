using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Entities;
using neuromasters.borders.Repositories;
using System.Security.Claims;

namespace neuromasters.repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Gerenciamento de Roles

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string?> GetUserRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return false;

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
                return false;

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => new RoleDto(r.Id, r.Name ?? string.Empty));
        }

        public async Task<RoleDto?> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role is null ? null : new RoleDto(role.Id, role.Name ?? string.Empty);
        }

        #endregion

        #region Atribuição de Roles a Usuários

        public async Task<bool> UserHasRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            if (!await _roleManager.RoleExistsAsync(roleName))
                return false;

            if (await _userManager.IsInRoleAsync(user, roleName))
                return false; // Usuário já tem essa role

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            if (!await _userManager.IsInRoleAsync(user, roleName))
                return false; // Usuário não tem essa role

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<UserRoleDto>> GetUsersInRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                return new List<UserRoleDto>();

            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            return usersInRole.Select(u => new UserRoleDto(
                u.Id,
                u.Email ?? string.Empty,
                u.UserName ?? string.Empty,
                roleName
            ));
        }

        #endregion

        #region RoleClaims

        public async Task<bool> AddClaimToRoleAsync(string roleName, string claimType, string claimValue)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
                return false;

            var claim = new Claim(claimType, claimValue);
            var result = await _roleManager.AddClaimAsync(role, claim);
            return result.Succeeded;
        }

        public async Task<bool> RemoveClaimFromRoleAsync(string roleName, string claimType, string claimValue)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
                return false;

            var claim = new Claim(claimType, claimValue);
            var result = await _roleManager.RemoveClaimAsync(role, claim);
            return result.Succeeded;
        }

        public async Task<IEnumerable<RoleClaimDto>> GetRoleClaimsAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
                return new List<RoleClaimDto>();

            var claims = await _roleManager.GetClaimsAsync(role);

            return claims.Select(c => new RoleClaimDto(
                roleName,
                c.Type,
                c.Value
            ));
        }

        #endregion
    }
}
