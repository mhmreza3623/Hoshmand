using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hoshmand.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        public string RawResponse { get; set; }
        public DateTime ResponsDate { get; set; }
    }
}
