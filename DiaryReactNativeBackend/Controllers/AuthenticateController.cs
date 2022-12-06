using Amazon.Runtime;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Logics.Helpers;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels;
using DiaryReactNativeBackend.Services.Abstractions;
using DiaryReactNativeBackend.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;

namespace DiaryReactNativeBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserLogic _userLogic;
    private readonly IStorageService _storeService;

    public AuthenticateController(IConfiguration configuration, IUserLogic userLogic, IStorageService storeService)
    {
        _configuration = configuration;
        _userLogic = userLogic;
        _storeService = storeService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
    {
        var decodedPassword = EncodingHelper.EncodePasswordToBase64(requestModel.Password);
        var allUsers = await _userLogic.GetAllUsers();

        var existingUser = allUsers.FirstOrDefault(u =>
            u.Username == requestModel.Username &&
            u.Password == decodedPassword);

        if (existingUser == null)
        {
            return Unauthorized("Sai tên đăng nhập hoặc mật khẩu ");
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, requestModel.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("UserId", existingUser.UserId)
        };

        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            expires: DateTime.Now.AddDays(30),
            audience: _configuration["JWT:ValidAudience"],
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)); ;

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel) 
    {
        var allUsers = await _userLogic.GetAllUsers();

        var existingUser = allUsers.FirstOrDefault(u =>
            u.Username == requestModel.Username);

        if (existingUser != null)
        {
            return BadRequest("Tên đăng nhập này đã tồn tại!");
        }

        var registered = await _userLogic.SaveUser(requestModel);
        return Ok(registered);
    }

    [HttpPost]
    [Route("upload-image")]
    public async Task<IActionResult> UploadImage([FromBody] UploadImageModel requestModel)
    {
        try
        {
            var objUpload = new S3ObjectUpload(requestModel.ImageName, requestModel.Base64String, "osd-hr-management", "public/image");
            var path = await _storeService.UploadFileAsync(objUpload);

            return Ok(path);
        }
        catch
        {
            return BadRequest("Không thể tải hình ảnh lên");
        }
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetUsersPortal()
    {
        var result = await _userLogic.GetUsersPortal();
        Console.WriteLine(result);

        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    [Route("decode-token")]
    public async Task<IActionResult> DecodeToken([FromHeader] string jwtToken)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);

        var userId = jwt.Claims.First(c => c.Type == "UserId").Value;

        var user = await _userLogic.GetUserById(userId);

        return Ok(user);
    }
}
