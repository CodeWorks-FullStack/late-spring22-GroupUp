namespace GroupUp.Models
{
  public class GroupMember : RepoItem<int>
  {
    public int GroupId { get; set; }
    public string AccountId { get; set; }
  }
}