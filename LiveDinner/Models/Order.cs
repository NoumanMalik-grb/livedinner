namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        public int Order_Id { get; set; }

        [StringLength(50)]
        public string Order_Name { get; set; }

        [StringLength(50)]
        public string Order_Address { get; set; }
        
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Order_Email { get; set; }

        [StringLength(50)]
        public string Order_Phone_Number { get; set; }

        [StringLength(50)]
        public string Order_Type { get; set; }

        [StringLength(50)]
        public string Order_Delivery_Status { get; set; }

        [StringLength(50)]
        public string Order_Status { get; set; }

        public DateTime? Order_Date_Time { get; set; }

        public int? Account_Fid { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
