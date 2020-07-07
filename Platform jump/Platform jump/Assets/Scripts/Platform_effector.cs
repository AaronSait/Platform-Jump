using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Platform_effector : MonoBehaviour
{

    // Use this for initialization
    public Transform[] waypoints;
    public Transform[] max_Distance;
    public float speed;
    public Transform center;
    bool passed;
    Rigidbody2D rb2D;
    public bool movingsidways = false;
    Follow_Player follow_player;
    public Follow_Player spawn;

    void Start()
    {
        spawn = GameObject.Find("Follow_Player_Object").GetComponent<Follow_Player>();
        follow_player = GameObject.Find("Follow_Player_Object").GetComponent<Follow_Player>();
        if (gameObject.GetComponent<Rigidbody2D>())
        {
            rb2D = gameObject.GetComponent<Rigidbody2D>();
            StartCoroutine("Move");
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    if (this.gameObject.transform.parent.name == "sidways moving platform")
                        waypoints[i].position = new Vector3(Random.Range(center.position.x, max_Distance[i].position.x), waypoints[i].position.y, 0);
                    else
                        waypoints[i].position = new Vector3(max_Distance[i].position.x, Random.Range(max_Distance[i].position.y, center.position.y), 0);
                }
                else
                {
                    if (this.gameObject.transform.parent.name == "sidways moving platform")
                        waypoints[i].position = new Vector3(Random.Range(max_Distance[i].position.x, center.position.x), waypoints[i].position.y, 0);
                    else
                        waypoints[i].position = new Vector3(max_Distance[i].position.x, Random.Range(max_Distance[i].position.y, center.position.y), 0);
                }
            }
        }
    }

    void MoveTowards(int moveDir)
    {
        if (!movingsidways)
            rb2D.velocity = new Vector2(rb2D.velocity.x, moveDir * speed);
        else
            rb2D.velocity = new Vector2(moveDir * speed, rb2D.velocity.y);
    }

    IEnumerator Move()
    {
        // Debug.Log("IN CORUTINE");
        while (true)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Vector2 dir = (waypoints[i].position - transform.position);
                int go = 0;
                if (!movingsidways)
                {
                    if (dir.y < 0)
                    {
                        //Debug.Log("MOVING UP");
                        //Debug.Log(dir.y);
                        go = -1;

                    }
                    else if (dir.y > 0)
                    {
                        //Debug.Log("MOVING DOWN");
                        //Debug.Log(dir.y);
                        go = 1;
                    }
                }
                else if (dir.x < 0)
                {
                   // Debug.Log("MOVING LEFT");
                    go = -1;

                }
                else if (dir.x > 0)
                {
                   // Debug.Log("MOVING RIGHT");
                    go = 1;
                }

                while (Vector2.Distance(waypoints[i].position, transform.position) > 0.1f)
                {
                    // Debug.Log("CHECKING PASED");
                    if (passed)
                    {
                        //Debug.Log("MOVING");
                        go = 1;
                        passed = false;
                    }
                    MoveTowards(go);
                    yield return null;
                }
                yield return null;
            }
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.tag == "Insta_Break")
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            spawn.testing_area();
        }
        else if (collision.gameObject.tag == "Player" && this.tag == "One_Jump")
        {
            collision.gameObject.transform.GetChild(1).GetComponent<Player_Movement>().Jump_Up();
            spawn.testing_area();
            Destroy(this.gameObject.transform.parent.gameObject);
        }
        else if (collision.gameObject.tag == "Player" && this.tag == "Standard_Platform")
        {
            collision.gameObject.transform.GetChild(1).GetComponent<Player_Movement>().Jump_Up();
        }
        else if (collision.gameObject.tag != "Player" && collision.gameObject.name != "Follow_Player_Object")
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
            Destroy(parent);
        }
    }
}
