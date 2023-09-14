using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    public Vector2Int Position;
    public Vector3 WorldPosition;
    public float CostFromOrigin;
    public float CostToObjective;
    public float Score;
    public int MoveCost;
    public TileLogic Previous;
}
