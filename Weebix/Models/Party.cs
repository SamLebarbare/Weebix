using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weebix.Models
{
    public class Party
    {
        [Key]
        public int partyId { get; set; }
        public string name { get; set; }
        public int status { get; set; }
		public int playersInGame { get; set; }
        public DateTime createdAt { get; set; }


        public int ditributorId { get; set; }
    }
}