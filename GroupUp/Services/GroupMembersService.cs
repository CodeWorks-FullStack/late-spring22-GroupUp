using System;
using System.Collections.Generic;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class GroupMembersService
  {
    private readonly GroupMembersRepository _repo;

    public GroupMembersService(GroupMembersRepository repo)
    {
      _repo = repo;
    }

    internal GroupMember Create(GroupMember groupMemberData)
    {
      return _repo.Create(groupMemberData);
    }

    internal void Delete(int id, string userId)
    {
      GroupMember found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      if (found.AccountId != userId)
      {
        throw new Exception("Invalid Access");
      }
      _repo.Delete(id);
    }

    internal List<GroupMemberGroupViewModel> GetByAccountId(string id)
    {
      return _repo.GetByAccountId(id);
    }

    internal List<GroupMemberProfileViewModel> GetByGroupId(int id)
    {
      return _repo.GetByGroupId(id);
    }
  }
}