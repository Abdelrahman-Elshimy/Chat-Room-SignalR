using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SigChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigChat.Data
{
    public class ApplicationDbContext : IdentityDbContext<ChatUser>
    {
        public DbSet<Message> Messages { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
