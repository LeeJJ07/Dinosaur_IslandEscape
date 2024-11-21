using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HakSeung
{
    public abstract class CUIPopup : CUIBase
    {
        public virtual void ClosePopupUI()
        {
            UIManager.Instance.ClosePopupUI(this);
        }
    }
}
