    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace Chinook.Framework.Entities
{
    [Table("Album")]
    public partial class Album
    {
        public int AlbumId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
