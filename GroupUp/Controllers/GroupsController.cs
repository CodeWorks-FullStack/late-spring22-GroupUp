using System;
using System.Collections.Generic;
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
  public class GroupsController : ControllerBase
  {
    private readonly GroupsService _gs;
    // OH KNOW
    private readonly GroupMembersService _gms;

    public GroupsController(GroupsService gs, GroupMembersService gms)
    {
      _gs = gs;
      _gms = gms;
    }

    [HttpGet]
    public async Task<ActionResult<List<Group>>> Get()
    {
      try
      {
        // NOTE IF THE USER IS NOT LOGGED IN userInfo WILL BE NULL!!!!!
        // YOU CANNOT DRILL INTO PROPERTIES THAT ARE NULL WITHOUT ELVIS (?) 
        // **if not used error is typically Object not set to an instance of an object
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Group> groups = _gs.Get(userInfo?.Id);
        return Ok(groups);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Group> Get(int id)
    {
      try
      {
        Group group = _gs.Get(id);
        return Ok(group);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{id}/members")]
    public ActionResult<List<GroupMemberProfileViewModel>> GetMembers(int id)
    {
      try
      {
        List<GroupMemberProfileViewModel> members = _gms.GetByGroupId(id);
        return Ok(members);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Group>> Create([FromBody] Group groupData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupData.CreatorId = userInfo.Id;
        groupData.Creator = userInfo;
        Group group = _gs.Create(groupData);
        return Ok(group);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Group>> Edit([FromBody] Group groupData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupData.CreatorId = userInfo.Id;
        groupData.Id = id;
        Group group = _gs.Edit(groupData);
        return Ok(group);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Group>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _gs.Delete(id, userInfo.Id);
        return Ok(new { Message = "Deleted Group" });
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }



  }
}