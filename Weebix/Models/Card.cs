using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Weebix.Models
{

    public class Card
    {
        [Key]
        public int cardId { get; set; }
        public string path { get; set; }

    }
}