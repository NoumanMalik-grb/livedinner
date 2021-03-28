namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            FeedBacks = new HashSet<FeedBack>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Account_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Phone_Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedBack> FeedBacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
