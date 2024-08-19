using System.ComponentModel.DataAnnotations;

namespace backnc.Data.POCOEntities
{
    public class UserRole
    {        
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
