using Dormitory.Core.Application.Catalog.CoreRepository.Dtos;
using Dormitory.Domain.Shared.Constant;
using Dormitory.Domain.Shared.Extension;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Dormitory.EntityFrameworkCore.StudentEntityFrameworkCore;
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
        private readonly StudentSolutionDbContext _studentDbContext;
        public CoreRepo(
            AdminSolutionDbContext adminDbContext,
            StudentSolutionDbContext studentDbContext)
        {
            _adminDbContext = adminDbContext;
            _studentDbContext = studentDbContext;
        }
        public async Task<LoginStatusDto> Authenticate(string userName, string password, int tenantId)
        {
            var user = new UserDto();
            //neu nguoi dang nhap la sinh vien
            if(tenantId == DataConfigConstant.studentTenantId)
            {
                user = await _studentDbContext.UserAccountEntities
                    .Where(x => x.UserName == userName && x.Password == CoreExtensions.Encrypt(password))
                    .Select(x => new UserDto
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Password = x.Password,
                        Email = x.Email,
                    }).FirstOrDefaultAsync();
            }
            //neu nguoi dang nhap la quan ly ktx
            if(tenantId == DataConfigConstant.adminTenantId)
            {
                user = await _adminDbContext.UserAccountEntities
                    .Where(x => x.UserName == userName && x.Password == CoreExtensions.Encrypt(password))
                    .Select(x => new UserDto
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Password = x.Password,
                        Email = x.Email,
                    }).FirstOrDefaultAsync();
            }

            if(user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
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
                    UserName = user.UserName,
                    Access_token = tokenJson,
                    IsLoginSuccess = true
                };
            }else
            {
                return new LoginStatusDto
                {
                    UserName = null,
                    Access_token = null,
                    IsLoginSuccess = false,
                };
            }
        }
    }
}
