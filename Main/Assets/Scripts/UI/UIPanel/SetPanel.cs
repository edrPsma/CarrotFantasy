using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SetPanel : BasePanel
{
    private Tweener mSetPanelRightTween;
    public TMP_Text[] Records;
    public Button BackgroundMusicBtn;
    public Sprite[] BackgroundMusicBtnSprite;
    public Button SoundEffectBtn;
    public Sprite[] SoundEffectBtnSprite;

    public GameObject OptionPage;
    public GameObject StatisicsPage;
    public GameObject ProducerPage;

    #region 生命周期
    protected override void OnInit()
    {
        transform.position = new Vector3(-Screen.width, transform.position.y);
        transform.DOLocalMove(new Vector3(transform.position.x, 0, 0), 0);
        mSetPanelRightTween = transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f).SetAutoKill(false);
        mSetPanelRightTween.Pause();

        BindProperty();
    }
    protected override void OnEnter()
    {
        Synchronization();
        mSetPanelRightTween.PlayForward();
    }

    protected override void OnExit()
    {
        mSetPanelRightTween.PlayBackwards();
    }
    #endregion

    #region 返回主面板
    public void BackToMainPanel()
    {
        GetUtility<UIManager>().PopPanel();
    }
    #endregion

    #region 属性绑定
    private void BindProperty()
    {
        var storageSystem = GetSystem<StorageSystem>();
        storageSystem.AdventureModelNum.OnValueChanged = value => { Records[0].text = value.ToString(); };
        storageSystem.BurriedLevelNum.OnValueChanged = value => { Records[1].text = value.ToString(); };
        storageSystem.BossModelNum.OnValueChanged = value => { Records[2].text = value.ToString(); };
        storageSystem.Coin.OnValueChanged = value => { Records[3].text = value.ToString(); };
        storageSystem.KillMonsterNum.OnValueChanged = value => { Records[4].text = value.ToString(); };
        storageSystem.KillBossNum.OnValueChanged = value => { Records[5].text = value.ToString(); };
        storageSystem.ClearItemNum.OnValueChanged = value => { Records[6].text = value.ToString(); };

        storageSystem.BackgroundMusic.OnValueChanged = value =>
        {
            if (value)
            {
                BackgroundMusicBtn.image.sprite = BackgroundMusicBtnSprite[0];
            }
            else
            {
                BackgroundMusicBtn.image.sprite = BackgroundMusicBtnSprite[1];
            }
        };
        storageSystem.SoundEffect.OnValueChanged = value =>
        {
            if (value)
            {
                SoundEffectBtn.image.sprite = SoundEffectBtnSprite[0];
            }
            else
            {
                SoundEffectBtn.image.sprite = SoundEffectBtnSprite[1];
            }
        };

        SoundEffectBtn.onClick.AddListener(() => { GetSystem<StorageSystem>().SoundEffect.Value = !GetSystem<StorageSystem>().SoundEffect.Value; });
        BackgroundMusicBtn.onClick.AddListener(() => { GetSystem<StorageSystem>().BackgroundMusic.Value = !GetSystem<StorageSystem>().BackgroundMusic.Value; });
    }
    #endregion

    #region 属性同步
    private void Synchronization()
    {
        OptionPage.SetActive(true);
        StatisicsPage.SetActive(false);
        ProducerPage.SetActive(false);

        var storageSystem = GetSystem<StorageSystem>();
        storageSystem.AdventureModelNum.Synchronization();
        storageSystem.BurriedLevelNum.Synchronization();
        storageSystem.BossModelNum.Synchronization();
        storageSystem.Coin.Synchronization();
        storageSystem.KillMonsterNum.Synchronization();
        storageSystem.KillBossNum.Synchronization();
        storageSystem.ClearItemNum.Synchronization();
        storageSystem.BackgroundMusic.Synchronization();
        storageSystem.SoundEffect.Synchronization();
    }
    #endregion

    #region 重置记录
    public void ResetGame()
    {
        SendCommand<ResetRecordCmd>();
    }
    #endregion
}
