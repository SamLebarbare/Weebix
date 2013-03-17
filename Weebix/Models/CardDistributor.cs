using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weebix.Models
{
    public class CardDistributor
    {
        [Key]
        public int distributorId { get; set; }
        public int lastIndex { get; set; }
    }
}