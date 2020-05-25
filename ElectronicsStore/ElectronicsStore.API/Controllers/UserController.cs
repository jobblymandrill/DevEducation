using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Auth;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async ValueTask<ActionResult> UserAdd(UserInputModel inputModel)
        {
            if (inputModel == null) { return BadRequest("No data to insert"); }
            var result = await _userRepository.AddUser(_mapper.Map<User>(inputModel));
            if (result.IsOkay) { return Ok(); }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpPost("token")]
        public async ValueTask<IActionResult> Token(AuthInputModel inputModel)
        {
            var identity = await GetIdentity(inputModel);
            if (identity == null) { return BadRequest("Email or password is invalid"); }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = inputModel.Email
            };

            return Json(response);
        }

        private async ValueTask<ClaimsIdentity> GetIdentity(AuthInputModel inputModel)
        {
            UserInputModel userInputModel = new UserInputModel() { Email = inputModel.Email, Password = inputModel.Password };
            User user = await _userRepository.GetUserByEmailAndPassword(_mapper.Map<User>(userInputModel));
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token");
                return claimsIdentity;
            }
            return null;
        }
    }
}
