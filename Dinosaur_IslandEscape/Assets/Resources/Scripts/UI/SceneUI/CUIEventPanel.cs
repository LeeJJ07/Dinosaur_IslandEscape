using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIEventPanel: CUIScene
{
    [SerializeField] private CUIProgressBar progressBar;
    public CUINote[] playerNotes = new CUINote[TOTALPLAYERS];

    public CUIProgressBar ProgressBar { get; }

    protected override void InitUI()
    {
       
    }

}
