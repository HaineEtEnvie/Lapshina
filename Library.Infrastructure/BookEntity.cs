namespace Library.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class BookEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookEntity()
        {
            Extradition = new HashSet<ExtraditionEntity>();
        }

        public long id { get; set; }

        [StringLength(2147483647)]
        public string name { get; set; }

        [StringLength(2147483647)]
        public string publishinghouse { get; set; }

        [StringLength(2147483647)]
        public string genre { get; set; }

        [StringLength(2147483647)]
        public string writerfirstname { get; set; }

        [StringLength(2147483647)]
        public string writesecondrname { get; set; }

        [StringLength(2147483647)]
        public string writerlastname { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtraditionEntity> Extradition { get; set; }
    }
}
