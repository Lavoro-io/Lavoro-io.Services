using UserService.IServices;
using UserService.Utilities;
using UserService.DTO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using UserService.DAL;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using Saml;
using Microsoft.EntityFrameworkCore;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _dbContext;

        public UserService(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Private Methods
        private UserDAL GetUserDb(Guid uuid)
        {
            return _dbContext.Users.Include(x => x.Role).Where(x => x.UserId == uuid && x.Enabled).FirstOrDefault();
        }

        private UserDAL GetUserByEmail(string email)
        {
            return _dbContext.Users.Include(x => x.Role).Where(x => x.Email == email && x.Enabled).FirstOrDefault();
        }

        private Guid GetRoleByEnum(Roles role)
        {
            return _dbContext.Roles.Where(x => x.RoleEnum == role).FirstOrDefault().RoleId;
        }

        private AuthDTO generateJwtToken(UserDAL user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();

            //Temp key for test purpose. Move key on better place
            var key = Encoding.ASCII.GetBytes("25f02b821910431fae479d1898a7195f");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("uuid", user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthDTO()
            {
                uuid = user.UserId,
                Token = tokenHandler.WriteToken(token),
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = tokenDescriptor.Expires
            };
        }

        #endregion

        public UserDTO GetUser(Guid uuid)
        {
            var user = GetUserDb(uuid);

            if (user == null) return null;

            return new UserDTO()
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Role = user.Role.Name
            };
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            var userToUpdate = GetUserDb(user.UserId);

            userToUpdate.Username = user.Username;
            //userToUpdate.Email = user.Email;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Name = user.Name;

            _dbContext.Users.Update(userToUpdate);
            _dbContext.SaveChanges();

            return new UserDTO()
            {
                UserId = userToUpdate.UserId,
                Username = userToUpdate.Username,
                Email = userToUpdate.Email,
                Name = userToUpdate.Name,
                Surname = userToUpdate.Surname
            };
        }

        public void DeleteUser(Guid uuid)
        {
            var user = GetUserDb(uuid);

            user.Enabled = false;

            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }

        public bool RegisterUser(RegisterFormDTO registerForm)
        {
            var newUser = _dbContext.Users.Add(new DAL.UserDAL()
            {
                Email = registerForm.Email,
                Surname = registerForm.Surname,
                Name = registerForm.Name,
                Username = registerForm.Email.Split("@")[0],
                Password = BC.HashPassword(registerForm.Password),
                RoleId = GetRoleByEnum(Roles.User)
            });

            _dbContext.SaveChanges();

            return newUser != null;
        }

        public AuthDTO GetToken(LoginFormDTO loginForm)
        {
            var user = GetUserByEmail(loginForm.Email);
            var isValid = BC.Verify(loginForm.Password, user.Password);

            if (user == null && !isValid) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
        }

        public string SamlRequest(string baseHref, string redirectUri)
        {
            //TODO: specify the SAML provider url here, aka "Endpoint"
            var samlEndpoint = "<SAML-ENDPOINT>";

            var request = new AuthRequest(
                baseHref, //TODO: put your app's "entity ID" here
                redirectUri //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
            );

            return request.GetRedirectUrl(samlEndpoint);
        }

        public AuthDTO SamlResponse(HttpRequest samlResponseRequest)
        {
            // 1. TODO: specify the certificate that your SAML provider gave you
            string samlCertificate = @"-----BEGIN CERTIFICATE-----
                                    BLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAHBLAH
                                    -----END CERTIFICATE-----
                                    ";

            // 2. Let's read the data - SAML providers usually POST it into the "SAMLResponse" var
            Saml.Response samlResponse = new Response(samlCertificate, samlResponseRequest.Form["SAMLResponse"]);

            // 3. We're done!
            if (samlResponse.IsValid())
            {
                //WOOHOO!!! user is logged in
                var uuid = samlResponse.GetNameID();

                var token = generateJwtToken(new UserDAL() { UserId = new Guid(uuid) });

                return token;
            }

            return null;
        }
    }

}
