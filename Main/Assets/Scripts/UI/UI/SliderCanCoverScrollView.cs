using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using EG;

public class SliderCanCoverScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private float mContentLength;
    private float mBeginMousePostionX;
    private float mEndMousePositionX;
    private ScrollRect mScrollRect;
    private float mLastProportion;//上一个滑动比例

    private float CellLength;//单元格长度
    private float Spacing;//间隙
    private float LeftOffset;//左偏移量
    private float mOneItemLength;
    private float mOneItemPrioirtion;

    public int totalItemNum;//共有几个单元格
    public BindableProperty<int> CurrentIndex { get; private set; } = new BindableProperty<int>();//当前单元格的索引

    private void Awake()
    {
        mScrollRect = GetComponent<ScrollRect>();
        GridLayoutGroup gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
        CellLength = gridLayoutGroup.cellSize.x;
        Spacing = gridLayoutGroup.spacing.x;
        LeftOffset = gridLayoutGroup.padding.left;
        ChangeItemCount(totalItemNum);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mBeginMousePostionX = Input.mousePosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float offsetX = 0;
        mEndMousePositionX = Input.mousePosition.x;
        offsetX = (mBeginMousePostionX - mEndMousePositionX);
        if (Mathf.Abs(offsetX) > 0)//执行滑动的前提是大于第一个滑动距离
        {
            if (offsetX > 0)//右滑动
            {
                if (CurrentIndex.Value >= totalItemNum)
                {
                    return;
                }
                CurrentIndex.Value += 1;
                mLastProportion += mOneItemPrioirtion;
            }
            else//左滑动
            {
                if (CurrentIndex.Value <= 1)
                {
                    return;
                }
                CurrentIndex.Value -= 1;
                mLastProportion -= mOneItemPrioirtion;
            }
        }
        DOTween.To(() => mScrollRect.horizontalNormalizedPosition, value => mScrollRect.horizontalNormalizedPosition = value, mLastProportion, 0.5f);
    }

    public void Reset()
    {
        if (mScrollRect == null)
        {
            mScrollRect = GetComponent<ScrollRect>();
        }
        mContentLength = mScrollRect.content.rect.width - LeftOffset - CellLength - (Spacing - LeftOffset);
        mOneItemLength = CellLength + Spacing;
        mOneItemPrioirtion = mOneItemLength / mContentLength;
        CurrentIndex.Value = 1;
        mScrollRect.horizontalNormalizedPosition = 0;
        mLastProportion = 0;
    }

    public void ChangeItemCount(int count)
    {
        totalItemNum = count;
        mScrollRect.content.sizeDelta = new Vector2((CellLength + Spacing) * count, 0);
        Reset();
    }

    public void MoveToNext()
    {
        if (CurrentIndex.Value >= totalItemNum)
        {
            return;
        }
        CurrentIndex.Value += 1;
        mLastProportion += mOneItemPrioirtion;
        DOTween.To(() => mScrollRect.horizontalNormalizedPosition, value => mScrollRect.horizontalNormalizedPosition = value, mLastProportion, 0.5f);
    }

    public void MoveToLast()
    {
        if (CurrentIndex.Value <= 1)
        {
            return;
        }
        CurrentIndex.Value -= 1;
        mLastProportion -= mOneItemPrioirtion;
        DOTween.To(() => mScrollRect.horizontalNormalizedPosition, value => mScrollRect.horizontalNormalizedPosition = value, mLastProportion, 0.5f);
    }
}
