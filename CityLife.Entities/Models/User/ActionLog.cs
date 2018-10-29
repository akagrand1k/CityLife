using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.User
{
    [Table("ActionLog")]
    public class ActionLog : BaseEntity
    {
        /// <summary>
        /// Описание действия.
        /// </summary>
        public string Data { get; set; }
        public string EntityName { get; set; }
        public int? EntryId { get; set; }
        public string InnerReferralUrl { get; set; }
        /// <summary>
        /// alias of action
        /// </summary>
        public string Action { get; set; }

        public int? OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public UserProfile Profile { get; set; }

    }
}
