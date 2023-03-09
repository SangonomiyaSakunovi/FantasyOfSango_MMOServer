using System.ComponentModel.DataAnnotations;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerData
{
    public class UserInfo
    {
        //Value
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string Account { get; set; }
        [Required, MaxLength(64)]
        public string Password { get; set; }
        //ReferenceKey
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
