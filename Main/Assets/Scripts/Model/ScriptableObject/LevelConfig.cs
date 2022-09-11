using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "创建关卡配置文件")]
public class LevelConfig : ScriptableObject
{
    public List<LevelInfo> DataList = new List<LevelInfo>();
}
