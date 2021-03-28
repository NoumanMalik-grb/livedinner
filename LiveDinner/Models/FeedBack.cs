namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        [Key]
        public int FeedBack_Fid { get; set; }

        public DateTime? Date_Time { get; set; }

        public int? FeedBack_Rating { get; set; }

        public int? Account_Fid { get; set; }

        public int? Product_Fid { get; set; }

        public virtual Account Account { get; set; }

        public virtual Product Product { get; set; }
    }
}
