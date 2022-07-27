using GroupUp.Interfaces;

namespace GroupUp.Models
{
  public class Group : RepoItem<int>, ICreated
  {
    public string Name { get; set; }
    public bool? IsPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }

  public class GroupMemberGroupViewModel : Group
  {
    public int GroupMemberId { get; set; }
  }
}