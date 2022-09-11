using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo
{
    public int bigLevelId;
    public int smallLevelId;
    public List<GridPoint.GridState> gridStateList;
    public List<GridPoint.GridIndex> monsterPath;
    public List<Round.RoundInfo> roundInfoList;
}
