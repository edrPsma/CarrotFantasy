using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo
{
    public Sprite BigLevelBG;
    public Sprite BigLevelTitle;
    public Sprite DecorateLeft;
    public Sprite DecorateRight;
    public List<LevelDetailInfo> LevelDetails = new List<LevelDetailInfo>();
}

[System.Serializable]
public class LevelDetailInfo
{
    public int Waves;
    public Sprite LevelMap;
    public List<Sprite> TowerList = new List<Sprite>();
}
