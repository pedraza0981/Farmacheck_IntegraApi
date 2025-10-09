using System.Collections.Generic;

namespace Farmacheck.Models.Request
{
    public class SaveMenuRoleAssignmentRequest
    {
        public int RoleId { get; set; }

        public List<int> MenuIds { get; set; } = new();
    }
}
