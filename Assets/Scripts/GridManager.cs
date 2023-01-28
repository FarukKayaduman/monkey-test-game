using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform tilesParentTransform;
    [SerializeField] private Transform mainCameraTransform;

    [HideInInspector] public Dictionary<Vector3, Tile> tiles;

    private List<TextMeshProUGUI> tilesTextsList;

    private int numbersCount = 10;

    private int nextNumberToClick = 1;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        GenerateTiles();
        tilesTextsList = tilesParentTransform.GetComponentsInChildren<TextMeshProUGUI>(true).ToList();
        
        ClearTileTexts();
        GenerateRandomNumbers(numbersCount);
    }

    private void GenerateTiles()
    {
        tiles = new();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity, tilesParentTransform);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = (x + y) % 2 == 1;
                spawnedTile.Init(isOffset);

                tiles[new Vector3(x, y)] = spawnedTile;
            }
        }

        mainCameraTransform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    private void GenerateRandomNumbers(int numbersCount)
    {
        int tilesCount = height * width;
        List<int> numberedTileList = new();

        for (int i = 0; i < numbersCount; i++)
        {
            int randomTile;
            do
            {
                randomTile = UnityEngine.Random.Range(0, tilesCount);
            } while (numberedTileList.Contains(randomTile));

            numberedTileList.Add(randomTile);

            tiles.ElementAt(randomTile).Value.numberOnTileText = i + 1;

            tilesTextsList[randomTile].gameObject.SetActive(true);
            tilesTextsList[randomTile].text = (i + 1).ToString();
        }
    }

    public void OnTileButtonClicked(Tile tile)
    {
        if (tile.numberOnTileText == nextNumberToClick)
        {
            tile.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
            nextNumberToClick++;
        }
        if(nextNumberToClick > numbersCount)
        {
            winPanel.SetActive(true);
        }
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (tiles.TryGetValue(position, out Tile tile))
            return tile;
        return null;
    }

    private void ClearTileTexts()
    {
        int tilesCount = height * width;

        for (int i = 0; i < tilesCount; i++)
        {
            tilesTextsList[i].text = "";
        }
    }

    public void OnRestartButtonClicked()
    {
        winPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
