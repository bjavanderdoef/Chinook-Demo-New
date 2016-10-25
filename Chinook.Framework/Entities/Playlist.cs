    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace Chinook.Framework.Entities
{
    [Table("Playlist")]
    public partial class Playlist
    {
        public int PlaylistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
