using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGameData
{
    //public List<string> StringList;
    public string levelName;
    public int chapterIndex;
    public int levelIndex;
    public int hasPlayed;
    public SaveLoadGameData(string levelName, int chapterIndex, int levelIndex, int hasPlayed)
    {
        this.levelName = levelName;
        this.chapterIndex = chapterIndex;
        this.levelIndex = levelIndex;
        this.hasPlayed = hasPlayed;
    }

    public override string ToString()
    {
        return $"{levelName} {chapterIndex} {levelIndex} {hasPlayed}";
    }
}
