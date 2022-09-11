using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;

[System]
public class StorageSystem : AbstractSystem
{
    //设置和数据
    public BindableProperty<bool> BackgroundMusic { get; set; } = new BindableProperty<bool>();
    public BindableProperty<bool> SoundEffect { get; set; } = new BindableProperty<bool>();
    public BindableProperty<int> AdventureModelNum { get; set; } = new BindableProperty<int>(1);
    public BindableProperty<int> BurriedLevelNum { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> BossModelNum { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> Coin { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> KillMonsterNum { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> KillBossNum { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> ClearItemNum { get; set; } = new BindableProperty<int>();

    //怪物窝
    public BindableProperty<int> Cookies { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> Milk { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> Nest { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> Diamands { get; set; } = new BindableProperty<int>();

    //关卡数据
    public List<bool> UnLockNormalModelBigLevelList { get; set; } = new List<bool>();
    public List<LevelStage> UnLockNormalModelLevelList { get; set; } = new List<LevelStage>();
    public List<int> UnLockedNormalModelLevelNum = new List<int>();

}
