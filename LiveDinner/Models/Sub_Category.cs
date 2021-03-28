namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Category
    {
        [Key]
        public int Sub_Category_ID { get; set; }

        [StringLength(50)]
        public string Sub_Category_Name { get; set; }

        public int? Category_Fid { get; set; }

        public virtual Category Category { get; set; }
    }
}
