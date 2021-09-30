using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Models
{
    public class ChatdbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public ChatdbContext():base("server=localhost;Database=chatdb;Trusted_Connection=True;")
        {

        }
       
    }
}
