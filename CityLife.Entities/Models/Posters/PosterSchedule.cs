using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Posters
{
    public class PosterSchedule
    {
        [Key]
        public int Id { get; set; }
        public int PosterId { get; set; }

        /// <summary>
        /// Alias session from References
        /// </summary>
        public string Session { get; set; }

        [Index]
        public DateTime DateStart { get; set; }

        [Index]
        public DateTime DateEnd { get; set; }

        public DateTime TimeOfDay { get; set; }

        [ForeignKey("PosterId")]
        public Poster Poster { get; set; }

    }
}
