namespace SangoCommon.Class
{
    public class AStarGridPoint
    {
        public AStarGridPoint Parent { get; set; }
        public float F { get; set; }
        public float G { get; set; }
        public float H { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool IsObstacle { get; set; }
        public AStarGridPoint(int x, int y, int z, bool isObstacle)
        {
            X = x;
            Y = y;
            Z = z;
            IsObstacle = isObstacle;
            Parent = null;
            F = 0;
            G = 0;
            H = 0;
        }
    }
}
