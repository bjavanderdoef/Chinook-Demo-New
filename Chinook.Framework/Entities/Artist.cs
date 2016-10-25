    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace Chinook.Framework.Entities
{
    [Table("Artist")]
    public partial class Artist
    {
        public int ArtistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
