namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact_Us
    {
        [Key]
        public int Customer_Id { get; set; }

        [StringLength(50)]
        public string Customer_Name { get; set; }

        [StringLength(50)]
        public string Customer_Message { get; set; }

        [StringLength(50)]
        public string Customer_Email { get; set; }

        public DateTime? Date_time { get; set; }

     
    }
}
