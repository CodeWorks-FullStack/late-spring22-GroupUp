using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GroupUp.Models;
using GroupUp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GroupMembersController : ControllerBase
  {
    private readonly GroupMembersService _gms;

    public GroupMembersController(GroupMembersService gms)
    {
      _gms = gms;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<GroupMember>> CreateAsync([FromBody] GroupMember groupMemberData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupMemberData.AccountId = userInfo.Id;
        GroupMember gm = _gms.Create(groupMemberData);
        return Ok(gm);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<GroupMember>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _gms.Delete(id, userInfo.Id);
        return Ok(new { message = "Delete" });
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }
  }
}