using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStage
{
    public int[] TowerIDList;//可以建造的塔
    public int TowerIdListLength;//列表长度
    public bool AllClear;//是否清空此关卡道具
    public int CarrotState;//萝卜状态
    public int SmallLevelID;//小关卡ID
    public int BigLevelID;//大关卡ID
    public bool UnLocked;//此关卡是否解锁
    public bool IsRewardLevel;//是否为奖励关卡
    public int TotalRound;//波数

    public LevelStage(int[] towerIDList, int towerIdListLength, bool allClear, int carrotState, int smallLevelID, int bigLevelID, bool unLocked, bool isRewardLevel, int totalRound)
    {
        TowerIDList = towerIDList;
        TowerIdListLength = towerIdListLength;
        AllClear = allClear;
        CarrotState = carrotState;
        SmallLevelID = smallLevelID;
        BigLevelID = bigLevelID;
        UnLocked = unLocked;
        IsRewardLevel = isRewardLevel;
        TotalRound = totalRound;
    }
}
