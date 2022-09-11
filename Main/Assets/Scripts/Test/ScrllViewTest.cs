using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrllViewTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ScrollRect scrollRect;
    private RectTransform contentRectTransform;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("开始滑动");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("滑动中");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("滑动结束");
    }

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();

        contentRectTransform = scrollRect.content;

        Debug.Log("当前UI的世界坐标:" + contentRectTransform.position);
        Debug.Log("当前UI的局部坐标:" + contentRectTransform.localPosition);
        Debug.Log("宽度：" + contentRectTransform.rect.size);
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x * 2, contentRectTransform.sizeDelta.y);

        scrollRect.horizontalNormalizedPosition = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
