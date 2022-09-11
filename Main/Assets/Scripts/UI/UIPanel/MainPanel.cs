using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using DG.Tweening;
using UnityEngine.UI;
using EG.Scene;

public class MainPanel : BasePanel
{
    #region 组件
    private CanvasGroup canvasGroup;
    private Animator mCarrotAnimator;
    private RectTransform mMonsterTrans;
    private RectTransform mCloudTrans;
    #endregion
    private Tweener mMainPanelLeftTween;
    private Tweener mMainPanelRightTween;
    private Tweener mExitTween;
    private Tweener mMonsterTweene;
    private Tweener mCloundTweene;
    private bool mToRight;

    protected override void OnInit()
    {
        #region 组件获取
        canvasGroup = GetComponent<CanvasGroup>();
        mCarrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
        mMonsterTrans = transform.Find("Img_Monster").GetComponent<RectTransform>();
        mCloudTrans = transform.Find("Img_Cloud").GetComponent<RectTransform>();
        #endregion

        mMainPanelLeftTween = transform.DOLocalMoveX(-Screen.width, 0.5f);
        mMainPanelLeftTween.SetAutoKill(false);
        mMainPanelLeftTween.Pause();
        mMainPanelRightTween = transform.DOLocalMoveX(Screen.width, 0.5f);
        mMainPanelRightTween.SetAutoKill(false);
        mMainPanelRightTween.Pause();
    }
    protected override void OnEnter()
    {
        mCarrotAnimator.Play("CarrotGrow");
        PlayUITween();
    }

    protected override void OnPause()
    {
        if (mToRight)
        {
            mMainPanelRightTween.PlayForward();
        }
        else
        {
            mMainPanelLeftTween.PlayForward();
        }
        mMainPanelLeftTween.onComplete = () =>
        {
            canvasGroup.alpha = 0;
        };
        mMainPanelRightTween.onComplete = () =>
        {
            canvasGroup.alpha = 0;
        };
    }

    protected override void OnResume()
    {
        canvasGroup.alpha = 1;
        if (mToRight)
        {
            mMainPanelRightTween.PlayBackwards();
        }
        else
        {
            mMainPanelLeftTween.PlayBackwards();
        }
    }

    private void OnDestroy()
    {
        mMainPanelLeftTween.Kill();
        mMainPanelRightTween.Kill();
        mExitTween.Kill();
        mMonsterTweene.Kill();
        mCloundTweene.Kill();
    }

    private void PlayUITween()
    {
        mMonsterTweene = mMonsterTrans.DOLocalMoveY(mMonsterTrans.localPosition.y + 150, 2)
        .SetLoops(-1, LoopType.Yoyo);

        mCloudTrans.DOLocalMoveX(-Screen.width / 2 - mCloudTrans.rect.width / 2, 0);
        mCloundTweene = mCloudTrans.DOLocalMoveX(Screen.width / 2 + mCloudTrans.rect.width / 2, 8)
        .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    public void ToSetting()
    {
        mToRight = true;
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/SetPanel.prefab");
    }

    public void ToHelp()
    {
        mToRight = false;
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/HelpPanel.prefab");
    }

    public void ToGameOption()
    {
        GetUtility<SceneManager>().LoadScene<NormalGameOptionSceneState>(false);
    }

    public void ToMonsterNest()
    {
        GetUtility<SceneManager>().LoadScene<MonsterNestSceneState>(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
