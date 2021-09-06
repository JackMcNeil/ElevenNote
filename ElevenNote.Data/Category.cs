using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descirption { get; set; }

        //public List<Note> Notes { get; set; }
    }
}
