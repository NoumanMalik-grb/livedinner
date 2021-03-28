namespace LiveDinner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            FeedBacks = new HashSet<FeedBack>();
            Order_Details = new HashSet<Order_Details>();
            Products1 = new HashSet<Product>();
        }

        [Key]
        public int Product_Id { get; set; }

        [StringLength(50)]
        public string Product_Name { get; set; }

        [StringLength(50)]
        public string Product_Picture { get; set; }
        [NotMapped]
        public HttpPostedFileBase pro_img { get; set; }
        [NotMapped]
        public int Product_Quantity { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Product_Purchase_price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Product_Sale_Price { get; set; }

        [StringLength(100)]
        public string Product_Description { get; set; }

        [StringLength(50)]
        public string Product_Status { get; set; }

        public int? Sub_Category_Fid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedBack> FeedBacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products1 { get; set; }

        public virtual Product Product1 { get; set; }
    }
}
