using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Springfield.DBModels
{
    [Table("ItemImages")]
    public class ItemImage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Path { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}