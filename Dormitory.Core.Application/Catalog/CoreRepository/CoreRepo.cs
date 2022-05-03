using Dormitory.Core.Application.Catalog.CoreRepository.Dtos;
using Dormitory.Domain.AppEntities;
using Dormitory.Domain.Shared.Constant;
using Dormitory.Domain.Shared.Extension;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Application.Catalog.CoreRepository
{
    public class CoreRepo : ICoreRepo
    {
        private readonly AdminSolutionDbContext _adminDbContext;
        public CoreRepo(
            AdminSolutionDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }
        public async Task<LoginStatusDto> Authenticate(string userName, string password, int tenantId)
        {
            var user = await _adminDbContext.UserAccountEntities
                .Where(x => x.UserName == userName && x.Password == CoreExtensions.Encrypt(password) && x.Tenant == tenantId)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                }).FirstOrDefaultAsync();

            if(user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("TenantId", tenantId.ToString()),
                };

                var secretBytes = Encoding.UTF8.GetBytes(CoreConstant.Secret);
                var key = new SymmetricSecurityKey(secretBytes);
                var algorithm = SecurityAlgorithms.HmacSha256;

                var signingCredentials = new SigningCredentials(key, algorithm);

                var token = new JwtSecurityToken(
                    CoreConstant.Issuer,
                    CoreConstant.Audiance,
                    claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials);

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                return new LoginStatusDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Access_token = tokenJson,
                    IsLoginSuccess = true
                };
            }else
            {
                return new LoginStatusDto
                {
                    UserId = 0,
                    UserName = null,
                    Access_token = null,
                    IsLoginSuccess = false,
                };
            }
        }

        public async Task<int> Register(RegisterRequest request)
        {
            var user = new UserAccountEntity();
            user.UserName = request.UserName;
            user.Password = CoreExtensions.Encrypt(request.Password);
            user.Email = request.Email;
            user.IsDeleted = false;
            user.UserInfoId = request.UserInfoId;
            user.CreatedTime = DateTime.Now;
            user.Tenant = request.Tenant;

            await _adminDbContext.AddAsync(user);
            return await _adminDbContext.SaveChangesAsync();
        }
    }
}
