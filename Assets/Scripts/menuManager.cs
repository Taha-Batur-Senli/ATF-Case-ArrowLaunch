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
        string s = System.IO.File.ReadAllText(Application.persistentDataPath + "/levelInfo.json");
        levelsInJson = JsonUtility.FromJson<Levels>(s);
        setLevels(levelsInJson, currLevel);
        getrows();

        if(leftElems.Count == rightElems.Count)
        {
            for (int i = 0; i < leftElems.Count; i++)
            {
                left += leftElems[i].ToString();
                right += rightElems[i].ToString();
            }

            levelEndVal = level.endValue.ToString();
            levelLeftInfo = left.ToString();
            levelRightInfo = right.ToString();
            whenReady = true;
        }
    }

    public string[] getLeft()
    {
        return level.leftValues;
    }

    public string[] getRight()
    {
        return level.rightValues;
    }

    private void OnValidate()
    {
        if(whenReady)
        {
            if (levelEndVal.Trim() != level.endValue.ToString() && int.Parse(levelEndVal.Trim().ToString()) > 0)
            {
                level.endValue = int.Parse(levelEndVal.Trim().ToString());
                levelsInJson.levels[level.levelID].endValue = level.endValue;
                string s = JsonUtility.ToJson(levelsInJson);
                System.IO.File.WriteAllText(Application.persistentDataPath + "/levelInfo.json", s);
            }

            if (!levelLeftInfo.ToString().Equals(left))
            {
                int cnt = 0;
                int start = 0;
                int a = 0;
                int len = 0;

                while(len < levelLeftInfo.Length)
                {
                    if (cnt == level.leftValues.Length - 1)
                    {
                        level.leftValues[cnt] = levelLeftInfo.Trim().ToString().Substring(start);
                    }
                    else if (a != 0 && (levelLeftInfo[len] == '+' || levelLeftInfo[len] == '-' || levelLeftInfo[len] == '/' || levelLeftInfo[len] == '*'))
                    {
                        if (start == 0)
                        {
                            level.leftValues[cnt] = levelLeftInfo.Trim().ToString().Substring(start, a);
                            cnt++;
                            start = len;
                            a = -1;
                        }
                        else
                        {
                            level.leftValues[cnt] = levelLeftInfo.Trim().ToString().Substring(start, a + 1);
                            cnt++;
                            start = len;
                            a = -1;
                        }

                    }
                    len++;
                    a++;
                }

                levelsInJson.levels[level.levelID].leftValues = level.leftValues;
                string s = JsonUtility.ToJson(levelsInJson);
                System.IO.File.WriteAllText(Application.persistentDataPath + "/levelInfo.json", s);
            }

            if (!levelRightInfo.ToString().Equals(right))
            {
                int cnt = 0;
                int start = 0;
                int a = 0;
                int len = 0;

                while (len < levelRightInfo.Length)
                {
                    if (cnt == level.rightValues.Length - 1)
                    {
                        level.rightValues[cnt] = levelRightInfo.Trim().ToString().Substring(start);
                    }
                    else if (a != 0 && (levelRightInfo[len] == '+' || levelRightInfo[len] == '-' || levelRightInfo[len] == '/' || levelRightInfo[len] == '*'))
                    {
                        if (start == 0)
                        {
                            level.rightValues[cnt] = levelRightInfo.Trim().ToString().Substring(start, a);
                            cnt++;
                            start = len;
                            a = -1;
                        }
                        else
                        {
                            level.rightValues[cnt] = levelRightInfo.Trim().ToString().Substring(start, a + 1);
                            cnt++;
                            start = len;
                            a = -1;
                        }
                    }
                    len++;
                    a++;
                }

                levelsInJson.levels[level.levelID].rightValues = level.rightValues;
                string s = JsonUtility.ToJson(levelsInJson);
                System.IO.File.WriteAllText(Application.persistentDataPath + "/levelInfo.json", s);
            }
        }
    }

    public void setLevels(Levels given, int idL)
    {
        levels = given;
        level = levels.levels[idL];
    }

    public int getLevel()
    {
        return level.endValue;
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
