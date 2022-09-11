using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int TotalRound { get; set; }
    public Round[] RoundList { get; set; }
    public int CurrentRound { get; set; }

    public Level(int roundNum, List<Round.RoundInfo> roundInfoList)
    {
        TotalRound = roundNum;
        RoundList = new Round[TotalRound];
        for (int i = 0; i < TotalRound; i++)
        {
            RoundList[i] = new Round(roundInfoList[i].MonsterIDList, i, this);
        }

        for (int i = 0; i < TotalRound; i++)
        {
            if (i == TotalRound - 1)
            {
                break;
            }
            RoundList[i].SetNextRound(RoundList[i + 1]);
        }
    }

    public void HandleRound()
    {
        if (CurrentRound >= TotalRound)
        {
            //游戏胜利
        }
        else if (TotalRound - 1 == CurrentRound)
        {
            //最后一波怪的UI显示和音乐播放
        }
        else
        {
            RoundList[CurrentRound].Handle(CurrentRound);
        }
    }

    //调用最后一回合的handle方法
    public void HandleLastRound()
    {
        RoundList[CurrentRound].Handle(CurrentRound);
    }

    public void AddRoundNum()
    {
        CurrentRound++;
    }
}
