using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : SingletonMB<GameManager>
{
    public Light2D spotLight2D;
    public Light2D globalLight2D;

    public float lightsOutCount;

    private bool isCalled;

    private void Start()
    {
        globalLight2D.intensity = lightsOutCount;
    }


    private void Update()
    {
        if(isCalled) return;
        if (lightsOutCount < 0)
        {
            isCalled = true;
            DisableImages();
        }
        if (lightsOutCount < 0) return;
        lightsOutCount -= Time.deltaTime;
        globalLight2D.intensity -= Time.deltaTime;
        if(spotLight2D.pointLightOuterRadius < 1.5f) return;
        spotLight2D.pointLightOuterRadius -= Time.deltaTime;
    }

    private void DisableImages()
    {
        var allTile = GridManager.Instance.tiles;
        var tileValues = allTile.Values;
        foreach (var imageList in tileValues)
        {
            var images = imageList.gameObject.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                var imageColor = image.color;
                var targetColor =new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
                image.color = targetColor;
            }
        }
    }
}
