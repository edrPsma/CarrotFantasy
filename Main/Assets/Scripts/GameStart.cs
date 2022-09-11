using UnityEngine;
using EG;
using EG.Scene;
using EG.UI;

public class GameStart : ViewController
{
    bool isEditor = false;
    private void Awake()
    {
#if UNITY_EDITOR
        isEditor = true;
#endif
        if (isEditor)
        {
            EGrameWork.Start(StartModel.Editor);
        }
        else
        {
            EGrameWork.Start(StartModel.Application);
        }
    }

    private void Start()
    {
        GetUtility<SceneManager>().LoadScene<MainSceneState>(false);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnStart()
    {
        EGrameWork.Start(StartModel.Editor);
    }
}
