using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SigChat.Models
{
    public class ChatUser: IdentityUser
    {
        public virtual ICollection<Message> Messages { get; set; }
    }
}