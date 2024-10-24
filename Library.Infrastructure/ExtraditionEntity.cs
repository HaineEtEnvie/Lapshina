namespace Library.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Extradition")]
    public partial class ExtraditionEntity
    {
        public long id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string data { get; set; }

        public long idbook { get; set; }

        public long idreaders { get; set; }

        public virtual BookEntity Book { get; set; }

        public virtual ReadersEntity Readers { get; set; }
    }
}
