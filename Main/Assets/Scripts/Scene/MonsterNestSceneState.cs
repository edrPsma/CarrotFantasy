using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG.Scene;
using EG.UI;

public class MonsterNestSceneState : BaseSceneState
{
    protected override string BindScene()
    {
        return "Assets/Scenes/DynamicScene/MonsterNestScene.unity";
    }

    protected override void OnLoadStart()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/GameLoadPanel.prefab");
    }

    protected override void OnEnterScene()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/MonsterNestPanel.prefab");
    }
}
