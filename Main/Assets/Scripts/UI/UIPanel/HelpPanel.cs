using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class HelpPanel : BasePanel
{
    private Tweener mSetPanelLeftTween;
    public TMP_Text HelpPagePageNum;
    public TMP_Text TowerPagePageNum;
    public SliderCanCoverScrollView HelpPageScro;
    public SliderCanCoverScrollView TowerPageScro;

    public GameObject HelpPage;
    public GameObject MonsterPage;
    public GameObject TowerPage;

    #region 生命周期
    protected override void OnInit()
    {
        transform.position = new Vector3(Screen.width, transform.position.y);
        transform.DOLocalMove(new Vector3(transform.position.x, 0, 0), 0);
        mSetPanelLeftTween = transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f).SetAutoKill(false);
        mSetPanelLeftTween.Pause();

        BindProperty();
    }
    protected override void OnEnter()
    {
        Synchronization();
        mSetPanelLeftTween.PlayForward();
    }

    protected override void OnExit()
    {
        mSetPanelLeftTween.PlayBackwards();
    }
    #endregion

    public void BackToMainPanel()
    {
        GetUtility<UIManager>().PopPanel();
    }

    #region 属性绑定
    public void BindProperty()
    {
        HelpPageScro.CurrentIndex.OnValueChanged = value => { HelpPagePageNum.text = value.ToString() + "/" + HelpPageScro.totalItemNum; };
        TowerPageScro.CurrentIndex.OnValueChanged = value => { TowerPagePageNum.text = value.ToString() + "/" + TowerPageScro.totalItemNum; };
    }
    #endregion

    #region  同步状态
    public void Synchronization()
    {
        HelpPage.SetActive(true);
        MonsterPage.SetActive(false);
        TowerPage.SetActive(false);

        HelpPageScro.Reset();
        TowerPageScro.Reset();
    }
    #endregion
}
