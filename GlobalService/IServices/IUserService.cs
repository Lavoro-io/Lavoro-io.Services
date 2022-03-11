using Saml;
using System;
using GlobalService.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GlobalService.IServices
{
    public interface IUserService
    {
        UserDTO GetUser(Guid uuid);
        List<UserDTO> GetUsers();
        UserDTO UpdateUser(UserDTO user);
        void DeleteUser(Guid uuid);

        //Auth Methods
        bool RegisterUser(RegisterFormDTO registerForm);
        AuthDTO GetToken(LoginFormDTO loginForm);

        //Saml Login
        string SamlRequest(string baseHref, string redirectUri);
        AuthDTO SamlResponse(HttpRequest samlResponse);
    }
}

