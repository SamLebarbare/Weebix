using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Weebix.Models
{
    public class WeebixDoContext : DbContext
    {
        public DbSet<Card> deck { get; set; }
        public DbSet<Party> games { get; set; }

    }
}