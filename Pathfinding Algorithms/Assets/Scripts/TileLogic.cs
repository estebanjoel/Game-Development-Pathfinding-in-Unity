using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    [SerializeField] private Vector3Int _position;
    public Vector3Int position{get{return _position;} set{_position = value;}}
    [SerializeField] private TileLogic _previous;
    public TileLogic previous{get{return _previous;} set{_previous = value;}}
    [SerializeField] int _moveCost;
    public int moveCost {get{return _moveCost;} set{_moveCost = value;}}
    //Costo entre el origen y este tile
    [SerializeField] float _costFromOrigin;
    public float costFromOrigin{get{return _costFromOrigin;} set{_costFromOrigin = value;}}
    //Costo entre este tile y el objetivo
    [SerializeField] float _costToObjective;
    public float costToObjective {get{return _costToObjective;} set{_costToObjective = value;}}

    // gCost + hCost = fCost
    [SerializeField] float _score;
    public float score {get{return _score;} set{_score = value;}}
    
}
