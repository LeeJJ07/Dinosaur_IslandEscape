using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIRunningPanel : CUIScene
{
    [SerializeField] private CUIProgressBar progressBar;
    [SerializeField] private CUINote player1Note;
    [SerializeField] private CUINote player2Note;

    public CUIProgressBar ProgressBar { get; }
    public CUINote Player1Note { get; }
    public CUINote Player2Note { get; }

    protected override void InitUI()
    {
       
    }

}
