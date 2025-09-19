using neuromasters.borders.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Repositories
{
    public interface IRolesRepository
    {
        // Gerenciamento de Roles
        Task<bool> RoleExistsAsync(string roleName);
        Task<string?> GetUserRoleAsync(string userId);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto?> GetRoleByNameAsync(string roleName);

        // Atribuição de Roles a Usuários
        Task<bool> UserHasRoleAsync(string userId, string roleName);
        Task<bool> AssignRoleToUserAsync(string userId, string roleName);
        Task<bool> RemoveRoleFromUserAsync(string userId, string roleName);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task<IEnumerable<UserRoleDto>> GetUsersInRoleAsync(string roleName);

        // RoleClaims (opcional, mas poderoso)
        Task<bool> AddClaimToRoleAsync(string roleName, string claimType, string claimValue);
        Task<bool> RemoveClaimFromRoleAsync(string roleName, string claimType, string claimValue);
        Task<IEnumerable<RoleClaimDto>> GetRoleClaimsAsync(string roleName);
    }
}
