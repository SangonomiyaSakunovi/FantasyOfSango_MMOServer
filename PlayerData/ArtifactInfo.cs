using Common.ArtifactCode;

namespace PlayerData
{
    public class ArtifactInfo
    {
        public int Id { get; set; }
        public ArtifactNameCode ArtifactName { get; set; }
        public int Attack { get; set; }
        //ReferenceKey
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
