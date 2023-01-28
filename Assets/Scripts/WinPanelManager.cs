using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelManager : MonoBehaviour
{
    public void OnRestartButtonClicked()
    {
        GridManager.Instance.OnRestartButtonClicked();
    }
    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
