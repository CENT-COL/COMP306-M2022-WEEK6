using System;
using System.Collections.Generic;

namespace WebApp4Sec001.Models
{
    public partial class Login
    {
        public string LoginName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Student LoginNameNavigation { get; set; } = null!;
    }
}
