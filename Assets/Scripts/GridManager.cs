using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private Tile tilePrefab;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI wrongCountText;

    [SerializeField] private Transform tilesParentTransform;
    [SerializeField] private Transform mainCameraTransform;

    [HideInInspector] public Dictionary<Vector3, Tile> tiles;

    private List<TextMeshProUGUI> tilesTextsList;
    private List<Image> tilesImageList;

    private int numbersCount = 10;

    private int nextNumberToClick = 1;
    
    private int score;
    private int wrongCount;

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

            // tilesTextsList[randomTile].gameObject.SetActive(true);
            tilesTextsList[randomTile].text = (i + 1).ToString();
            tilesImageList = tilesTextsList[randomTile].gameObject.transform.parent.gameObject
                .GetComponentsInChildren<Image>().ToList();
            tilesImageList[i].enabled = true;
        }
    }

    public void OnTileButtonClicked(Tile tile)
    {
        if (tile.numberOnTileText == nextNumberToClick)
        {
            // tile.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
            var allImages = tile.gameObject.GetComponentsInChildren<Image>().ToList();
            var enabledImages = allImages.FindIndex(x => x.enabled);
            allImages[enabledImages].enabled = false;
            score += 10;
            scoreText.text = score.ToString();
            nextNumberToClick++;
        }
        else
        {
            score -= 5;
            wrongCount++;

            scoreText.text = score.ToString();
            wrongCountText.text = wrongCount.ToString();
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
        wrongCount = 0;
        wrongCountText.text = "0";
        scoreText.text = "0";
        winPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
