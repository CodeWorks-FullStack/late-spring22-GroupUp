using GroupUp.Models;

namespace GroupUp.Interfaces
{
  public interface ICreated
  {
    string CreatorId { get; set; }
    Profile Creator { get; set; }
  }
}