using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupUp.Models;

namespace GroupUp.Repositories
{
  public class GroupsRepository
  {
    private readonly IDbConnection _db;

    public GroupsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Group> Get()
    {
      // POPULATE
      string sql = @"
      SELECT
        a.*,
        g.*
      FROM _groups g
      JOIN accounts a ON a.id = g.creatorId;";
      return _db.Query<Profile, Group, Group>(sql, (prof, group) =>
      {
        group.Creator = prof;
        return group;
      }).ToList();
    }

    internal Group Get(int id)
    {
      // POPULATE
      string sql = @"
      SELECT
        a.*,
        g.*
      FROM _groups g
      JOIN accounts a ON a.id = g.creatorId
      WHERE g.id = @id;";
      return _db.Query<Profile, Group, Group>(sql, (prof, group) =>
      {
        group.Creator = prof;
        return group;
      }, new { id }).FirstOrDefault();
    }

    internal Group Create(Group groupData)
    {
      string sql = @"
        INSERT INTO _groups
        (name, creatorId, isPrivate)
        VALUES
        (@Name, @CreatorId, @IsPrivate);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, groupData);
      groupData.Id = id;
      groupData.CreatedAt = new DateTime();
      groupData.UpdatedAt = new DateTime();
      return groupData;

    }

    internal void Edit(Group update)
    {
      string sql = @"
        UPDATE _groups
        SET
            name = @Name,
            isPrivate = @IsPrivate
        WHERE id = @Id;";
      _db.Execute(sql, update);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM _groups WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}