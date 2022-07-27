using System;
using System.Collections.Generic;
using GroupUp.Models;
using GroupUp.Repositories;

namespace GroupUp.Services
{
  public class GroupsService
  {
    private readonly GroupsRepository _repo;

    public GroupsService(GroupsRepository repo)
    {
      _repo = repo;
    }

    private Group ValidateOwnership(int id, string userId)
    {
      Group original = Get(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Forboden Treasure");
      }

      return original;
    }

    internal List<Group> Get(string userId) // user id MIGHT be null
    {
      List<Group> groups = _repo.Get();
      //   but what if I am logged in?
      return groups.FindAll(g => g.IsPrivate == false || g.CreatorId == userId);
    }

    internal Group Get(int id)
    {
      Group found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }

    internal Group Create(Group groupData)
    {
      return _repo.Create(groupData);
    }

    internal Group Edit(Group groupData)
    {
      Group original = ValidateOwnership(groupData.Id, groupData.CreatorId);
      original.Name = groupData.Name ?? original.Name;
      original.IsPrivate = groupData.IsPrivate ?? original.IsPrivate;

      _repo.Edit(original);
      original.UpdatedAt = new DateTime();
      return original;
    }

    internal void Delete(int id, string userId)
    {
      Group found = ValidateOwnership(id, userId);
      _repo.Delete(id);
    }
  }
}