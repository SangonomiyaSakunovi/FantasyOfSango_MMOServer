using SangoCommon.Classs;
using System;
using System.Collections.Generic;
using SangoCommon.Structs;

namespace FantasyOfSango.Systems
{
    public class AStarSystem
    {
        //Attention! In this method, we define that NPC can`t go to the edge of map, that should avoid before it runs in Server.

        public static AStarSystem Instance = null;

        //These transforms need set in games, so must get float, and also need change to int = 10*float.
        public Vector3Position startTrans { get; set; }
        public Vector3Position targetTrans { get; set; }

        //Attention! Int gridLenth = 1, means the real map grid is 0.1f.
        public int GridLength = 1;

        public List<AStarGridPoint> aStarGridPointList;
        public Dictionary<Vector2Grid, AStarGridPoint> mapDict;
        public List<AStarGridPoint> aStarList = null;
        private Dictionary<Vector2Grid, AStarGridPoint> openDict = null;
        private Dictionary<Vector2Grid, AStarGridPoint> closeDict = null;

        private Vector2Grid startPos;
        private Vector2Grid targetPos;

        private AStarGridPoint startPoint = null;
        private AStarGridPoint targetPoint = null;

        private void Start()
        {
            Instance = this;
            int startXInt = (int)((startTrans.X + float.Epsilon) * 10);
            int startZInt = (int)((startTrans.Z + float.Epsilon) * 10);
            int targetXInt = (int)((targetTrans.X + float.Epsilon) * 10);
            int targetZInt = (int)((targetTrans.Z + float.Epsilon) * 10);

            startPos = new Vector2Grid(startXInt, startZInt);
            targetPos = new Vector2Grid(targetXInt, targetZInt);

            openDict = new Dictionary<Vector2Grid, AStarGridPoint>();
            closeDict = new Dictionary<Vector2Grid, AStarGridPoint>();
            aStarList = new List<AStarGridPoint>();
            mapDict = new Dictionary<Vector2Grid, AStarGridPoint>();
        }

        public void AStarGraphListToDict()
        {
            for (int i = 0; i < aStarGridPointList.Count; i++)
            {
                Vector2Grid tempPoint = new Vector2Grid(aStarGridPointList[i].X, aStarGridPointList[i].Z);
                mapDict.Add(tempPoint, aStarGridPointList[i]);
            }
        }

        public void Test()
        {
            startPoint = mapDict[startPos];
            targetPoint = mapDict[targetPos];

            PathFinder(startPoint, targetPoint);
            PathSaver(startPoint, targetPoint);

            //If the right path has found
            for (int i = aStarList.Count - 1; i > 0; i--)
            {

            }
        }

        private void PathFinder(AStarGridPoint startPoint, AStarGridPoint targetPoint)
        {
            bool isFind = false;
            openDict.Add(startPos, startPoint);
            while (openDict.Count > 0)
            {
                AStarGridPoint minFValuePoint = FindMinFValuePoint(openDict);
                Vector2Grid minFValuePointPos = new Vector2Grid(minFValuePoint.X, minFValuePoint.Z);
                openDict.Remove(minFValuePointPos);
                closeDict.Add(minFValuePointPos, minFValuePoint);
                List<AStarGridPoint> surroundPointList = GetSurroundPoints(minFValuePointPos);
                for (int i = 0; i < surroundPointList.Count; i++)
                {
                    AStarGridPoint tempPoint = surroundPointList[i];
                    Vector2Grid tempPointPos = new Vector2Grid(tempPoint.X, tempPoint.Z);
                    if (openDict.ContainsKey(tempPointPos))
                    {
                        float tempGValue = CalculateGValue(minFValuePoint, tempPoint);
                        if (tempGValue < tempPoint.G)
                        {
                            tempPoint.Parent = minFValuePoint;
                            tempPoint.G = tempGValue;
                            tempPoint.F = tempGValue + tempPoint.H;
                            openDict[tempPointPos] = tempPoint;
                        }
                    }
                    else
                    {
                        tempPoint.Parent = minFValuePoint;
                        CalculateFValue(tempPoint, targetPoint);
                        openDict.Add(tempPointPos, tempPoint);
                    }
                }
                if (openDict.ContainsKey(targetPos))
                {
                    isFind = true;
                    break;
                }
            }
            if (!isFind)
            {

            }
            else
            {

            }
        }

        private void PathSaver(AStarGridPoint startPoint, AStarGridPoint targetPoint)
        {
            AStarGridPoint tempPoint = targetPoint;
            while (tempPoint.Parent != null)
            {
                aStarList.Add(tempPoint);
                tempPoint = tempPoint.Parent;
            }
        }

        private List<AStarGridPoint> GetSurroundPoints(Vector2Grid currentPointPos)
        {
            List<AStarGridPoint> surroundPointList = new List<AStarGridPoint>();
            int xInt = currentPointPos.GridX;
            int zInt = currentPointPos.GridZ;
            Vector2Grid upPoint = new Vector2Grid(xInt, zInt + GridLength);
            Vector2Grid downPoint = new Vector2Grid(xInt, zInt - GridLength);
            Vector2Grid leftPoint = new Vector2Grid(xInt - GridLength, zInt);
            Vector2Grid rightPoint = new Vector2Grid(xInt + GridLength, zInt);
            Vector2Grid leftUpPoint = new Vector2Grid(xInt - GridLength, zInt + GridLength);
            Vector2Grid leftDownPoint = new Vector2Grid(xInt - GridLength, zInt - GridLength);
            Vector2Grid rightUpPoint = new Vector2Grid(xInt + GridLength, zInt + GridLength);
            Vector2Grid rightDownPoint = new Vector2Grid(xInt + GridLength, zInt - GridLength);

            if (mapDict.ContainsKey(upPoint) && !mapDict[upPoint].IsObstacle && !closeDict.ContainsKey(upPoint))
            {
                surroundPointList.Add(mapDict[upPoint]);
            }
            if (mapDict.ContainsKey(downPoint) && !mapDict[downPoint].IsObstacle && !closeDict.ContainsKey(downPoint))
            {
                surroundPointList.Add(mapDict[downPoint]);
            }
            if (mapDict.ContainsKey(leftPoint) && !mapDict[leftPoint].IsObstacle && !closeDict.ContainsKey(leftPoint))
            {
                surroundPointList.Add(mapDict[leftPoint]);
            }
            if (mapDict.ContainsKey(rightPoint) && !mapDict[rightPoint].IsObstacle && !closeDict.ContainsKey(rightPoint))
            {
                surroundPointList.Add(mapDict[rightPoint]);
            }
            if (mapDict.ContainsKey(leftUpPoint) && !mapDict[leftUpPoint].IsObstacle && !closeDict.ContainsKey(leftUpPoint))
            {
                surroundPointList.Add(mapDict[leftUpPoint]);
            }
            if (mapDict.ContainsKey(leftDownPoint) && !mapDict[leftDownPoint].IsObstacle && !closeDict.ContainsKey(leftDownPoint))
            {
                surroundPointList.Add(mapDict[leftDownPoint]);
            }
            if (mapDict.ContainsKey(rightUpPoint) && !mapDict[rightUpPoint].IsObstacle && !closeDict.ContainsKey(rightUpPoint))
            {
                surroundPointList.Add(mapDict[rightUpPoint]);
            }
            if (mapDict.ContainsKey(rightDownPoint) && !mapDict[rightDownPoint].IsObstacle && !closeDict.ContainsKey(rightDownPoint))
            {
                surroundPointList.Add(mapDict[rightDownPoint]);
            }
            return surroundPointList;
        }

        private AStarGridPoint FindMinFValuePoint(Dictionary<Vector2Grid, AStarGridPoint> openDict)
        {
            float minFValue = float.MaxValue;
            AStarGridPoint minFValuePoint = null;
            foreach (AStarGridPoint point in openDict.Values)
            {
                if (point.F < minFValue)
                {
                    minFValue = point.F;
                    minFValuePoint = point;
                }
            }
            return minFValuePoint;
        }

        private float CalculateGValue(AStarGridPoint currentPoint, AStarGridPoint nextPoint)
        {
            float gValue = CalculateVector2Dis(currentPoint.X, currentPoint.Z, nextPoint.X, nextPoint.Z) + currentPoint.G;
            return gValue;
        }

        private void CalculateFValue(AStarGridPoint currentPoint, AStarGridPoint targetPoint)
        {
            //F = G + H
            float hValue = Math.Abs(currentPoint.X - targetPoint.X) + Math.Abs(currentPoint.Z - targetPoint.Z);
            AStarGridPoint parentPoint = currentPoint.Parent;
            float gValue = CalculateVector2Dis(currentPoint.X, currentPoint.Z, parentPoint.X, parentPoint.Z) + parentPoint.G;
            currentPoint.F = gValue + hValue;
            currentPoint.G = gValue;
            currentPoint.H = hValue;
        }

        private float CalculateVector2Dis(int x1, int z1, int x2, int z2)
        {
            int xlen = x1 - x2;
            int zlen = z1 - z2;
            float dis = (float)Math.Sqrt(xlen * xlen + zlen * zlen);
            return dis;
        }
    }
}
