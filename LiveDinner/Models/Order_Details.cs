namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Details
    {
        [Key]
        public int OD_Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OD_Sale_Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OD_Purchase_Price { get; set; }

        public int? OD_Quantity { get; set; }

        public int Product_Fid { get; set; }

        public int Order_Fid { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
