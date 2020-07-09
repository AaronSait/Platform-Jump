using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Master : MonoBehaviour
{



    public static Game_Master instanc = null;
    private void Awake()
    {
        if (instanc == null)
        {
            instanc = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public static Player_Data[] get_High_Score()
    {
        Player_Data[] allScores = new Player_Data[5];
        for (int i = 0; i < allScores.Length; i++)
        {
            allScores[i] = new Player_Data
                (
                PlayerPrefs.GetString("high_score" + i + "name", ""),
                PlayerPrefs.GetInt("high_score" + i + "score", 0)
                );
        }
        return allScores;
    }
    public static void saveHighScores(Player_Data scores, int i)
    {
        PlayerPrefs.SetString("high_score" + i + "name", scores.get_Name());
        PlayerPrefs.SetInt("high_score" + i + "score", scores.get_Score());
    }
}
