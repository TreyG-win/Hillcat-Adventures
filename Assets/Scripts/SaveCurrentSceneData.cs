using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveCurrentSceneData : MonoBehaviour
{
    private SaveLoadGameData gameData;
    public string currentLevel;
    public int currentChapter;
    public int currentIndex;
    public int hasPlayed;

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        CreateData();
    }

    private void CreateData()
    {
        gameData = new SaveLoadGameData(currentLevel, currentChapter, currentIndex, hasPlayed);
        Debug.Log("Creating Data");
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Level", gameData.levelName);
        PlayerPrefs.SetInt("Chapter", gameData.chapterIndex);
        PlayerPrefs.SetInt("Index", gameData.levelIndex);
        PlayerPrefs.SetInt("hasPlayed", 1);

        PlayerPrefs.Save();
        Debug.Log("Saving");
    }

    public void LoadData()
    {
        gameData = new SaveLoadGameData(PlayerPrefs.GetString("Level"), PlayerPrefs.GetInt("Chapter"), PlayerPrefs.GetInt("Index"), PlayerPrefs.GetInt("hasPlayed"));
        Debug.Log("Loading");
        Debug.Log(gameData.ToString());
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
