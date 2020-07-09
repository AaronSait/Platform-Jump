using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody2D rb2D;
    float movement_Speed, max_movement_Speed;
    float fall_Speed, jump_CD, jump_CDR;
    Vector3 jump_Force = new Vector3(0, 20f, 0);
    public UI_Controller uiController;
    public int score;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        movement_Speed = 10.0f;
        max_movement_Speed = 20f;
        jump_CD = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.acceleration.x * movement_Speed, rb2D.velocity.y, 0);
        if (movement.y > max_movement_Speed)
            movement.y = max_movement_Speed;
        rb2D.velocity = movement;
        if (jump_CDR >= 0)
            jump_CDR -= Time.deltaTime;
        uiController.update_Score((int)this.transform.position.y);
        score = (int)this.transform.position.y;
    }

    public void Jump_Up()
    {
        if (jump_CDR < 0)
        {
            rb2D.AddForce(jump_Force, ForceMode2D.Impulse);
            jump_CDR = jump_CD;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("HIT");
        if (collision.gameObject.tag == "Standard_Platform")
            Jump_Up();
    }
}

/* Attempt One WORKED but did not look natral
 * 
 * NEEDED VARIABLES
 * bool jumping = false;
 * 
 * 
 *  INSIDE SART
 * 
 *  fall_Speed = -5.0f;
 *  jump_Force = 7.5f;
 *  
 * 
*       Vector3 movement;
*       if (!jumping)
*           movement = new Vector3(Input.acceleration.x * movement_Speed, fall_Speed, 0);
*       else
*       {
*           movement = new Vector3(Input.acceleration.x * movement_Speed, jump_Force, 0);
*           if (jump_CDR < 0)
*           {
*               jump_CDR = jump_CD;
*               jumping = false;
*           }
*           else
*           {
*               jump_CDR -= Time.deltaTime;
*           }
*       }
*       rb2D.velocity = movement;
*       
*   }
*
*   public void Jump_Up()
*   {
*       Debug.Log("JUMP");
*       jumping = true;
*   }
*/
