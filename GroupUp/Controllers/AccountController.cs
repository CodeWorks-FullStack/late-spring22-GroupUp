using System;
using System.Threading.Tasks;
using GroupUp.Models;
using GroupUp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroupUp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly GroupMembersService _gms;

    public AccountController(AccountService accountService, GroupMembersService gms)
    {
      _accountService = accountService;
      _gms = gms;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("memberships")]
    [Authorize]
    public async Task<ActionResult<List<GroupMemberGroupViewModel>>> GetGroups()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<GroupMemberGroupViewModel> groups = _gms.GetByAccountId(userInfo.Id);
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}