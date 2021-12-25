using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SigChat.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string MessageBody { get; set; }
        public DateTime MessageDate { get; set; }

        public virtual  ChatUser FromUser { get; set; }
    }
}
