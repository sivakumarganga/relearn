using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.EFCore.Entities
{
    public class Role : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Role() { }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
