using UnityEngine;

public class IntroScreenManager : MonoBehaviour
{
    public void OnAnimationEndEvent()
    {
        Destroy(gameObject);
        MenuManager.Instance.tilesParent.SetActive(true);
    }
}
