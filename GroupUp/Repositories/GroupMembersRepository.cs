using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupUp.Models;

namespace GroupUp.Repositories
{
  public class GroupMembersRepository
  {
    private readonly IDbConnection _db;

    public GroupMembersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal GroupMember Create(GroupMember groupMemberData)
    {
      string sql = @"
      INSERT INTO groupmembers
      (accountId, groupId)
      VALUES
      (@AccountId, @GroupId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, groupMemberData);
      groupMemberData.Id = id;
      return groupMemberData;
    }

    internal GroupMember Get(int id)
    {
      string sql = "SELECT * FROM groupmembers WHERE id = @id";
      return _db.QueryFirstOrDefault<GroupMember>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM groupmembers WHERE id = @Id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal List<GroupMemberProfileViewModel> GetByGroupId(int id)
    {
      string sql = @"
      SELECT
        a.*,
        gm.id AS groupMemberId
      FROM groupmembers gm
      JOIN accounts a ON a.id = gm.accountId
      WHERE gm.groupId = @id;
      ";
      return _db.Query<GroupMemberProfileViewModel>(sql, new { id }).ToList();
    }

    internal List<GroupMemberGroupViewModel> GetByAccountId(string id)
    {
      // DOUBLE JOIN!!
      string sql = @"
      SELECT
        a.*,
        g.*,
        gm.id AS groupMemberId
      FROM groupmembers gm
      JOIN _groups g ON g.id = gm.groupId
      JOIN accounts a ON a.id = g.creatorId
      WHERE gm.accountId = @id;
      ";
      return _db.Query<Profile, GroupMemberGroupViewModel, GroupMemberGroupViewModel>(sql, (prof, gmgvm) =>
      {
        gmgvm.Creator = prof;
        return gmgvm;
      }, new { id }).ToList();
    }
  }
}