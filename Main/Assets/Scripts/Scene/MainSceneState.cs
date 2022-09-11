using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG.Scene;
using EG.UI;

public class MainSceneState : BaseSceneState
{
    protected override string BindScene()
    {
        return "Assets/Scenes/DynamicScene/MainScene.unity";
    }

    protected override void OnLoadStart()
    {
        GetUtility<UIManager>().PreLoadPanel("Assets/Prefabs/UIPanel/SetPanel.prefab");
        GetUtility<UIManager>().PreLoadPanel("Assets/Prefabs/UIPanel/HelpPanel.prefab");
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/StartLoadPanel.prefab");
    }

    protected override void OnEnterScene()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/MainPanel.prefab");
    }
}
