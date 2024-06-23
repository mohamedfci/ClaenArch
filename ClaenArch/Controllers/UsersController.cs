
using Application.MediatR;
using ClaenArch.Services;
using Domains.Data;
using Domains.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClaenArch.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : BaseApiController
{
     
    private readonly TokenService _tokenService;

    public UsersController( 
        TokenService tokenService, ILogger<UsersController> logger)
    {
       
        _tokenService = tokenService;
    }


    //[HttpPost]
    //[Route("register")]
    //public async Task<IActionResult> Register(RegistrationRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    var result = await _userManager.CreateAsync(
    //        new ApplicationUser { UserName = request.Username, Email = request.Email, Role = request.Role },
    //        request.Password!
    //    );

    //    if (result.Succeeded)
    //    {
    //        request.Password = "";
    //        return CreatedAtAction(nameof(Register), new { email = request.Email, role = Role.User }, request);
    //    }

    //    foreach (var error in result.Errors)
    //    {
    //        ModelState.AddModelError(error.Code, error.Description);
    //    }

    //    return BadRequest(ModelState);
    //}


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserDTO>> Authenticate([FromBody] TblUsers request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await Mediator.Send(new GetAllQuery<TblUsers>());
        TblUsers userInDb = managedUser.Where(x => x.UserName == request.UserName && x.Pass == request.Pass).FirstOrDefault();
        if (userInDb == null)
        {
            return Unauthorized();
            //   return BadRequest("Bad credentials");
        }

        //var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password!);

        //if (!isPasswordValid)
        //{
        //    return BadRequest("Bad credentials");
        //}

        //var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        //if (userInDb is null)
        //{
        //    return Unauthorized();
        //}

        var accessToken = _tokenService.GenerateJwtToken(userInDb);
         

        return Ok(new UserDTO
        {
            UserName = userInDb.UserName,
            Token = accessToken,
        });
    }
}