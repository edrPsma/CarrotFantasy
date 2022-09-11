using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG.Scene;
using EG.UI;

public class NormalGameOptionSceneState : BaseSceneState
{
    protected override string BindScene()
    {
        return "Assets/Scenes/DynamicScene/NormalOptionScene.unity";
    }

    protected override void OnLoadStart()
    {
        RegisterModel<LevelModel>();
        GetUtility<UIManager>().PreLoadPanel("Assets/Prefabs/UIPanel/HelpPanel.prefab");
        GetUtility<UIManager>().PreLoadPanel("Assets/Prefabs/UIPanel/GameNormalOptionPanel.prefab");
        GetUtility<UIManager>().PreLoadPanel("Assets/Prefabs/UIPanel/GameNormalBigLevelPanel.prefab");
        GetUtility<UIManager>().PreLoadPanel("Assets/Prefabs/UIPanel/GameNormalLevelPanel.prefab");
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/GameLoadPanel.prefab").transform.SetAsLastSibling();
    }

    protected override void OnEnterScene()
    {
        GetUtility<UIManager>().PopPanel();
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/GameNormalOptionPanel.prefab");
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/GameNormalBigLevelPanel.prefab");
    }
}
