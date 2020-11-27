using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System.Collections.Generic;

namespace IdentityServer.MVC.Models
{
    public class ApplicationUser : IdentityUser<ObjectId>
    {
        public ApplicationUser()
        {
            Role = new List<string>();
        }
        public IList<string> Role { get; set; }
    }
}
