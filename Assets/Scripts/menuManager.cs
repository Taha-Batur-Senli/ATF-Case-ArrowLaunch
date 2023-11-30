using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public TextAsset jsonFile;
    Levels levelsInJson;

    [TextAreaAttribute]
    public string levelEndVal;

    [TextAreaAttribute]
    public string levelLeftInfo;

    [TextAreaAttribute]
    public string levelRightInfo;

    public static int currLevel = 0;
    public static Level level;
    public static Levels levels;
    public static List<string> leftElems = new List<string>();
    public static List<string> rightElems = new List<string>();

    string left = "";
    string right = "";
    bool whenReady = false;

    private void Start()
    {
        levelsInJson = JsonUtility.FromJson<Levels>(jsonFile.text);
        setLevels(levelsInJson, currLevel);
        getrows();

        if(leftElems.Count == rightElems.Count)
        {
            for (int i = 0; i < leftElems.Count; i++)
            {
                left += " " + leftElems[i];
                right += " " + rightElems[i];
            }

            levelEndVal = level.endValue.ToString();
            levelLeftInfo = left.ToString();
            levelRightInfo = right.ToString();
            whenReady = true;
        }
    }

    private void OnValidate()
    {
        if(whenReady)
        {
            if (levelEndVal.Trim() != level.endValue.ToString())
            {
                Debug.Log("ss");
            }

            if (!levelLeftInfo.ToString().Equals(left))
            {
                Debug.Log("ss2");
            }

            if (!levelRightInfo.ToString().Equals(right))
            {
                Debug.Log("ss3");
            }
        }
    }

    public void setLevels(Levels given, int idL)
    {
        levels = given;
        level = levels.levels[idL];
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public int getID()
    {
        return level.levelID;
    }

    public void getrows()
    {
        foreach(string id in level.leftValues)
        {
            leftElems.Add(id);
        }

        foreach (string id in level.rightValues)
        {
            rightElems.Add(id);
        }
    }

}

[System.Serializable]
public class Levels
{
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    public int levelID;
    public int endValue;
    public string[] leftValues;
    public string[] rightValues;
}
