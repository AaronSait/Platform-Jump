using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Script : MonoBehaviour
{
    public bool all_Clear = true;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        all_Clear = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Test_Obj")
        {
            if (timer < collision.gameObject.GetComponent<test_Script>().timer)
            {
                all_Clear = false;
                Destroy(this.gameObject);
            }
        }
    }
}
