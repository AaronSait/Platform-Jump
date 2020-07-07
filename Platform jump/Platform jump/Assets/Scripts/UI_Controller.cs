using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    bool play_Button, settings_Button, back_Button;
    bool resume_Button, pause_Button, exit_Button;
    int old_score;
    public Text Score;
    public GameObject home_pannal, settings_pannal, pause_pannal, game_UI, tutorial_Text;
    float tutorial_CD = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        play_Button = false;
        settings_Button = false;
        back_Button = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void update_Score(int current_y_val)
    {
        if (old_score < current_y_val)
        {
            old_score = current_y_val;
            Score.text = "Score: " + old_score;
        }
    }
}
