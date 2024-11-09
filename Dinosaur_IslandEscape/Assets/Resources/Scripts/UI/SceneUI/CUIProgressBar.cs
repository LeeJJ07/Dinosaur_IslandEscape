using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CUIProgressBar : CUIScene
{
    [Header("Porgress Bar")]
    [SerializeField] private float curProgress;
    [SerializeField] private float maxProgress = 100;
    [SerializeField] private float minProgress = 0;

    public Sprite mask;

    private void Update()
    {
            
    }

    private void FillProgressBar()
    {

    }
}
