using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface IUsersRepository
    {

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<User> GetUserAsync(string email);

        Task<User> GetUserAsync(Guid userId);

        Task<IdentityResult> ChangePasswordAsync(User user, string currenPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();
    }
}