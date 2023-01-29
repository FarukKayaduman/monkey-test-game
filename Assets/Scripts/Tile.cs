using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;

    [SerializeField] private GameObject highlight;

    [SerializeField] private new SpriteRenderer renderer;

    [HideInInspector] public int numberOnTileText;

    public void Init(bool isOffset)
    {
        renderer.color = isOffset ? offsetColor : baseColor;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public void OnMouseDown()
    {
        var disableMouseClickCount = GameManager.Instance.lightsOutCount;
        if (disableMouseClickCount > 0) return;
        Tile thisTile = this;
        GridManager.Instance.OnTileButtonClicked(thisTile);
    }
}
