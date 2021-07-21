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
    [Route("api/v{version:apiVersion}/account")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountRepo repo;
        IFriendRepository friendRepo;
        private readonly IMapper mapper;
        public AccountController(IAccountRepo _repo, IMapper _mapper, IFriendRepository friendRepo)
        {
            repo = _repo;
            mapper = _mapper;
            this.friendRepo = friendRepo;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAccounts()
        {
            var objList = repo.GetAllAccounts();
            var dtoList = new List<Account>();
            foreach (var obj in objList)
            {
                dtoList.Add((Account)obj);
            }
            return Ok(dtoList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccountByID(int id)
        {
            Account account = repo.FindAccountById(id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccountByEmail(string email)
        {
            Account account = repo.FindAccountByEmail(email);
            if (account == null)
                return NotFound();
            return Ok(account);
        }
        [HttpGet("profile/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProfile(int id)
        {
            Account account = repo.getProfile(id);
            if (account == null)
                return NotFound();
            ProfileDto profile = mapper.Map<ProfileDto>(account);
            profile.Friends = friendRepo.GetAllFriendsAccountForAccount(id);
            return Ok(profile);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAccount(int id)
        {
            Account account = repo.FindAccountById(id);
            if (account == null)
                return NotFound("Check Id");
            if(!repo.DeleteAccount(account))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record{account.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public IActionResult UpdateAccount(int id, [FromBody] AccountRegisterInDto account)
        {
            if (account == null)
            {
                return BadRequest(ModelState);
            }
        /*    Account emailAccount = repo.FindAccountByEmail(account.Email);
          if (emailAccount != null && emailAccount.Id != id)
            {
                ModelState.AddModelError("", $"This email already exists");
                return StatusCode(400, ModelState);
            }*/
            Account NewAccount = mapper.Map<Account>(account);
            NewAccount.Id = id;
            if (!repo.UpdateAccount(NewAccount))
            {
                ModelState.AddModelError("", $"something went wrong updating the record{account.UserName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccountByName(string name)
        {
            List<Account> accounts = repo.FindAccountByName(name);
            if (accounts == null || accounts.Count() == 0)
                return NotFound();
            return Ok(accounts);
        }
    } 
}