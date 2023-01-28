using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelManager : MonoBehaviour
{
    public void OnRestartButtonClicked()
    {
        GridManager.Instance.OnRestartButtonClicked();
    }
}
