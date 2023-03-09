using Common.ElementCode;
using System.ComponentModel.DataAnnotations;

namespace PlayerData
{
    public class AttributeInfo
    {
        //Value
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public int HP { get; set; }
        public int HPFull { get; set; }
        public int MP { get; set; }
        public int MPFull { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public ElementTypeCode ElementType { get; set; }
        public int ElementGauge { get; set; }
        //ReferenceKey
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
