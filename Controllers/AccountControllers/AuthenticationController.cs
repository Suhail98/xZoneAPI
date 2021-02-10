using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountRepo;

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author your_name_here
/// </summary>
namespace xZoneAPI.Controllers.AccountControllers
{
    /// <summary>
    ///  A class that represents ...
    /// 
    ///  @see OtherClasses
    ///  @author your_name_here
    /// </summary>
    [Route("api/v{version:apiVersion}/authentication")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAccountRepo repo;
        private readonly IMapper mapper;
        public AuthenticationController(IAccountRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult register([FromBody] AccountRegisterInDto account)
        {
            if (account == null) 
                return BadRequest();
            Account accountObj = mapper.Map<Account>(account);
            if (repo.FindAccountByEmail(account.Email) != null)
                return NotFound("Email ALready Exist");
           Account _account = repo.register(accountObj);
            _account.Password = "";
            return Ok(_account);
        }
        
        [HttpPost("login")]
        public IActionResult login([FromBody] AccountLoginDto account)
        {
            Account accountObj = repo.AuthenticateUser(account.Email, account.Password);
            if (account == null)
                return BadRequest("Error");
            if(accountObj == null)
            {
                return BadRequest("email or password are wrong");
            }
            return Ok(accountObj);
        }
    }
}
