using _1Global.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1Global.Data.DTO
{
    [Table("Device")]
    public class Device
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity),Column("Id")]
        public int Id { get; set; }
        [Column("Name"),Required]
        public string Name { get; set; }
        [Column("Brand"),Required]
        public string Brand { get; set; }
        [Column("DeviceState"),Required]
        public DeviceState State { get; set; }
        [Column("CreationTime"),Required,DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreationTime { get; set; }

    }
}
