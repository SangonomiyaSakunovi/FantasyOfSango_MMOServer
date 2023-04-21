using SangoCommon.LocationCode;
using System;

namespace SangoCommon.Struct
{
    public class CommonStruct
    {
        public struct AOISceneGrid : IEquatable<AOISceneGrid>
        {
            public SceneCode SceneCode { get; private set; }
            public int GridX { get; private set; }
            public int GridZ { get; private set; }

            public AOISceneGrid(SceneCode sceneCode, int gridX, int gridZ) : this()
            {
                SceneCode = sceneCode;
                GridX = gridX;
                GridZ = gridZ;
            }

            public bool Equals(AOISceneGrid aoi) => SceneCode == aoi.SceneCode && GridX == aoi.GridX && GridZ == aoi.GridZ;
            public override bool Equals(object obj) => obj is AOISceneGrid aoi && this.Equals(aoi);
            public override int GetHashCode() => (GridX, GridZ).GetHashCode();
            public static bool operator ==(AOISceneGrid aoi1, AOISceneGrid aoi2) => aoi1.Equals(aoi2);
            public static bool operator !=(AOISceneGrid aoi1, AOISceneGrid aoi2) => !(aoi1 == aoi2);

        }
    }
}
