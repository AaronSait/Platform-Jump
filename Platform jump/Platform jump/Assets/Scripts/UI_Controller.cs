using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    bool play_Button, settings_Button, back_Button;
    bool resume_Button, pause_Button, exit_Button;
    bool reset_Button, mute_Sound, submitt;
    int old_score;
    string player_Name;
    public bool gameover;
    public InputField name;
    public Text score, gameover_Score;
    public Toggle mute;
    public Slider sound_Vol;
    public GameObject home_pannal, settings_pannal, pause_pannal, game_UI, tutorial_Text;
    float tutorial_CD = 5.0f;
    public Text[] hsName, hsScore;
    Player_Data[] allScores = new Player_Data[5];
    // Start is called before the first frame update
    void Start()
    {
        submitt = false;
        play_Button = false;
        settings_Button = false;
        back_Button = false;
            allScores = Game_Master.get_High_Score();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            for (int i = 0; i < allScores.Length; i++)
            {
                hsName[i].text = "" + allScores[i].get_Name();
                hsScore[i].text = "" + allScores[i].get_Score();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            gameover_Score.text = old_score.ToString();
            if (submitt)
            { 
                Player_Data acceved = new Player_Data(name.text, old_score);
                Player_Data temp_PD = new Player_Data(name.text, old_score); 
                Debug.Log(allScores.Length);
                for (int i = 0; i < allScores.Length; i++)
                {
                    //check if the acceved score is grater than any score on the leader board
                    if (old_score > allScores[i].get_Score())
                    {
                        //check to see if the acceved score is the same as any score on the scoreboard
                        if (old_score == allScores[i].get_Score())
                        {
                            if (i == 4)
                            {
                                break;
                            }
                            temp_PD = allScores[i];                      
                            Game_Master.saveHighScores(acceved, i);
                            break;
                        }
                        temp_PD = allScores[i];
                        Game_Master.saveHighScores(acceved, i);
                        acceved = new Player_Data(temp_PD.get_Name(), temp_PD.get_Score());
                    }
                }

                SceneManager.LoadScene(0);
            }
        }

        mute_Sound = mute;
        if (reset_Button)
        {
            mute.isOn = false;
            sound_Vol.value = 1.0f;
            //reset Score Board
            for (int i = 0; i < allScores.Length; i++)
            {
                Game_Master.saveHighScores(new Player_Data("",0), i);
            }
            allScores = Game_Master.get_High_Score();
            for (int i = 0; i < allScores.Length; i++)
            {
                hsName[i].text = "" + allScores[i].get_Name();
                hsScore[i].text = "" + allScores[i].get_Score();
            }
            reset_Button = false;

        }        
        if (tutorial_Text != null && tutorial_CD <= 0)
            Destroy(tutorial_Text);
        tutorial_CD -= Time.deltaTime;
        if (play_Button)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(1);
        }
        if (settings_Button)
        {
            settings_pannal.SetActive(true);
            home_pannal.SetActive(false);
        }
        if (back_Button)
        {
            settings_pannal.SetActive(false);
            home_pannal.SetActive(true);
        }
        if (resume_Button)
        {
            game_UI.SetActive(true);
            pause_pannal.SetActive(false);
            Time.timeScale = 1.0f;
            resume_Button = false;
        }
        if (pause_Button)
        {
            pause_pannal.SetActive(true);
            game_UI.SetActive(false);
            Time.timeScale = 0.0f;
            pause_Button = false;
        }
        if (exit_Button)
        {
            Application.Quit();
        }
    }

    public void play_Button_Clicked ()
    {
        play_Button = true;
    }
    public void exit_Button_Clicked()
    {
        exit_Button = true;
    }

    public void pause_Button_Clicked()
    {
        pause_Button = true;
    }

    public void resume_Button_Clicked()
    {
        resume_Button = true;
    }

    public void settings_Button_Clicked()
    {
        settings_Button = true;
    }

    public void back_Button_Clicked()
    {
        back_Button = true;
    }

    public void game_Over()
    {
        SceneManager.LoadScene(0);
    }

    public void submitt_Button()
    {
        submitt = true;
    }
    public void reset_Button_Clicked()
    {
        reset_Button = true;
    }
    
    public void update_Score(int current_y_val)
    {
        if (old_score < current_y_val)
        {
            old_score = current_y_val;
            score.text = "Score: " + old_score;
        }
    }
}
