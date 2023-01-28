using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class MenuManager : SingletonMB<MenuManager>
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject introPrefab;
    [SerializeField] private GameObject mainMenuPrefab;

    public GameObject tilesParent;

    private GameObject mainMenu;

    private int nextNumberToClick = 1;

    private void OnEnable()
    {
        mainMenu = Instantiate(mainMenuPrefab, canvasTransform);
    }

    private void Start()
    {
        Instantiate(introPrefab, canvasTransform);
    }

    public void OnTileButtonClicked(TileNoURP tile)
    {
        if (tile.numberOnTileText == nextNumberToClick)
        {
            tile.gameObject.SetActive(false);
            nextNumberToClick++;

            if (nextNumberToClick > 3)
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
