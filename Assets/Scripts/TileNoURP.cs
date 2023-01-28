using UnityEngine;
using UnityEngine.UI;

public class TileNoURP : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;

    [SerializeField] private GameObject highlight;

    [SerializeField] private new SpriteRenderer renderer;

    public int numberOnTileText;

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
        TileNoURP thisTile = this;
        MenuManager.Instance.OnTileButtonClicked(thisTile);
    }
}
