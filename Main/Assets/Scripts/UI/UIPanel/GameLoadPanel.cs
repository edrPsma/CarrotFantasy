using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using EG.Scene;

public class GameLoadPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    protected override void OnInit()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected override void OnEnter()
    {
        StartCoroutine(ChangeAlpha());
    }

    IEnumerator ChangeAlpha()
    {
        canvasGroup.blocksRaycasts = true;
        while (GetUtility<SceneManager>().IsLoading)
        {
            canvasGroup.alpha = 1 - (float)GetUtility<SceneManager>().LoadingProgress / 100;
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.blocksRaycasts = false;
        GetUtility<SceneManager>().ShowScene();
        canvasGroup.alpha = 0;
    }
}
