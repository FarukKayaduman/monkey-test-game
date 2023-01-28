using UnityEngine;

public class LightController : MonoBehaviour
{
    public Vector3 mousePosition;
    public GameObject light2d;
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;
        light2d = this.gameObject;
    }

    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var adjustedMousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        light2d.transform.position = adjustedMousePosition;
    }
}
