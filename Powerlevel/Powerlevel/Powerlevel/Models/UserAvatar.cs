namespace Powerlevel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAvatar")]
    public partial class UserAvatar
    {
        [Key]
        public int UAId { get; set; }

        public int UserId { get; set; }

        [StringLength(64)]
        public string Body { get; set; }

        [StringLength(64)]
        public string Armor { get; set; }

        [StringLength(64)]
        public string Weapon { get; set; }

        [StringLength(64)]
        public string Race { get; set; }

        public virtual User User { get; set; }
    }
}
