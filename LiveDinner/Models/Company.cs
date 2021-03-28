namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Company")]
    public partial class Company
    {
        [Key]
        public int Company_Id { get; set; }

        [StringLength(50)]
        public string Company_Name { get; set; }

        [StringLength(50)]
        public string Company_Email { get; set; }

        [StringLength(50)]
        public string Company_Contact { get; set; }

        [StringLength(200)]
        public string Company_Address { get; set; }

        [StringLength(200)]
        public string Company_Logo { get; set; }

        [NotMapped]

        public HttpPostedFileBase logo_img { get; set; }

    }
}
