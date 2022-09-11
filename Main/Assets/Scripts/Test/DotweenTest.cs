using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    private Image maskImage;
    void Start()
    {
        maskImage = GetComponent<Image>();

        DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
