using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting_Walls : MonoBehaviour
{
    GameObject left_Wall_Spawn, right_Wall_Spawn;


    // Start is called before the first frame update
    void Start()
    {
        left_Wall_Spawn = GameObject.Find("Left_Spawn");
        right_Wall_Spawn = GameObject.Find("Right_Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.gameObject.name == "Right_Boundry")
            {
                collision.gameObject.transform.parent.gameObject.transform.position = new Vector3(left_Wall_Spawn.gameObject.transform.position.x, collision.gameObject.transform.parent.gameObject.transform.position.y, 0);
            }
            else
            {
                collision.gameObject.transform.parent.gameObject.transform.position = new Vector3(right_Wall_Spawn.gameObject.transform.position.x, collision.gameObject.transform.parent.gameObject.transform.position.y, 0);
            }
        }
    }
}
