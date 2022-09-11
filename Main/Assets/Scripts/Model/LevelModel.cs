using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.Resource;

public class LevelModel : AbstractModel
{
    private string ConfigPath = "Assets/Prefabs/Config/LevelConfig.asset";
    private LevelConfig mLevelConfig;
    public int BigLevelCount { get; private set; }

    protected override void OnInit()
    {
        mLevelConfig = GetUtility<ResourcesManager>().Load<LevelConfig>(ConfigPath);
        BigLevelCount = mLevelConfig.DataList.Count;
    }

    #region 通过大关卡ID获取小关卡数量
    public int GetLevelCountByBigLevelId(int bigLevelId)
    {
        if (bigLevelId >= 0 && bigLevelId < BigLevelCount)
            return mLevelConfig.DataList[bigLevelId].LevelDetails.Count;
        else
            return 0;
    }
    #endregion

    #region 获取大关卡信息
    public List<BigLevelInfo> GetBigLevelInfos()
    {
        List<BigLevelInfo> bigLevelInfoList = new List<BigLevelInfo>();
        foreach (var item in mLevelConfig.DataList)
        {
            BigLevelInfo info = new BigLevelInfo();
            info.BigLevelBG = item.BigLevelBG;
            info.BigLevelTitle = item.BigLevelTitle;
            bigLevelInfoList.Add(info);
        }
        return bigLevelInfoList;
    }
    #endregion

    #region 通过大关卡ID获取关卡装饰
    public LevelDecorate GetBigLevelDecorateById(int bigLevelId)
    {
        LevelDecorate decorate = new LevelDecorate();
        if (bigLevelId >= 0 && bigLevelId < BigLevelCount)
        {
            decorate.DecorateLeft = mLevelConfig.DataList[bigLevelId].DecorateLeft;
            decorate.DecorateRight = mLevelConfig.DataList[bigLevelId].DecorateRight;
        }

        return decorate;
    }
    #endregion

    #region 通过大关卡ID获取小关卡信息
    public List<SmallLevelInfo> GetSmallLevelInfosById(int bigLevelId)
    {
        List<SmallLevelInfo> infoList = new List<SmallLevelInfo>();
        if (bigLevelId >= 0 && bigLevelId < BigLevelCount)
        {
            foreach (var smallLevel in mLevelConfig.DataList[bigLevelId].LevelDetails)
            {
                SmallLevelInfo info = new SmallLevelInfo();
                info.Waves = smallLevel.Waves;
                info.LevelMap = smallLevel.LevelMap;
                info.TowerList = smallLevel.TowerList;
                infoList.Add(info);
            }
        }
        return infoList;
    }
    #endregion
}

#region 结构体定义
public struct BigLevelInfo
{
    public Sprite BigLevelBG;
    public Sprite BigLevelTitle;
}

public struct LevelDecorate
{
    public Sprite DecorateLeft;
    public Sprite DecorateRight;
}

public struct SmallLevelInfo
{
    public int Waves;
    public Sprite LevelMap;
    public List<Sprite> TowerList;
}
#endregion