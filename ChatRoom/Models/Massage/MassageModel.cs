using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Models.Massage
{
    public class MassageModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string Massage { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}
