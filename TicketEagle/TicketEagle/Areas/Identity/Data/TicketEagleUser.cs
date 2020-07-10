using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketEagle.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the TicketEagleUser class
    public class TicketEagleUser : IdentityUser
    {
        [PersonalData]
        public string Nome { get; set; }
    }
}
