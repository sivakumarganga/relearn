using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.EFCore.Entities
{
    public class User : BaseEntity, IEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
