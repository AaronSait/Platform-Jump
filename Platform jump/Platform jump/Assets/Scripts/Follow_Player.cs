using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    GameObject player;
    bool can_Spawn, yes_Or_No;
    Transform hight_To_Spawn;
    float old_Posion, new_Posion, spawn_CD, spawn_Timer;
    public GameObject Platform_1, Platform_2, Platform_3, Platform_4, Platform_5, test_Obj;
    public UI_Controller ui_Controller;
    void Start()
    {
        hight_To_Spawn = GameObject.Find("Hight To Spawn Platform").transform;
        can_Spawn = true;
        spawn_CD = 0.5f;
        player = GameObject.Find("Player");
        old_Posion = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(spawn_CD + "/t" + can_Spawn);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        if (player != null)
        {
            new_Posion = player.transform.position.y;
            if (new_Posion > old_Posion)
            {
                old_Posion = new_Posion;
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, old_Posion, 0);
            }
            if (spawn_Timer > 0)
                can_Spawn = true;
            else
                spawn_Timer -= Time.deltaTime;
        }
        else
        {
            ui_Controller.game_Over();
        }
    }

    public void spawn_Object(GameObject test)
    {
        int random_Number;
        random_Number = (int)Random.Range(0, 100);        
        
        //if (can_Spawn)
        //{            
            if (random_Number <= 30)
            {
                if (yes_Or_No)
                    Instantiate(Platform_1, test.transform.position, Quaternion.identity);
            }
            else if (random_Number <= 40)
            {
                if (yes_Or_No)
                    Instantiate(Platform_2, test.transform.position, Quaternion.identity);
            }
            else if (random_Number <= 60)
            {
                if (yes_Or_No)
                    Instantiate(Platform_3, test.transform.position, Quaternion.identity);
            }
            else if (random_Number <= 80)
            {
                if (yes_Or_No)
                    Instantiate(Platform_4, test.transform.position, Quaternion.identity);
            }
            else if (random_Number <= 100)
            {
                if (yes_Or_No)
                    Instantiate(Platform_5, test.transform.position, Quaternion.identity);
            }
            //spawn_Timer = spawn_CD;
            //can_Spawn = false;
        //}
        Destroy(test);
    }

    public void testing_area()
    {
        Vector3 position = new Vector3(Random.Range(-2.5f, 2.5f), hight_To_Spawn.position.y, 0);
        GameObject test = Instantiate(test_Obj, position, Quaternion.identity);
        StartCoroutine(waiting(test));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject parent = collision.gameObject;
        bool final_Parent = false;
        do
        {
            if (parent.transform.parent != null)
                parent = parent.transform.parent.gameObject;
            else
                final_Parent = true;
        }
        while (!final_Parent);
        testing_area();
        Destroy(parent);
    }

    IEnumerator waiting(GameObject test)
    {
        yield return new WaitForSeconds(0.25f);
        if (test != null)
        {
            yes_Or_No = test.GetComponent<test_Script>().all_Clear;
            Debug.Log(test.GetComponent<test_Script>().all_Clear);

            spawn_Object(test);
        }
    }

}
