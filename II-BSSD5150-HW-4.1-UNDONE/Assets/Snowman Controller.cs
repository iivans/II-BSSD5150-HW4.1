using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
    public float outTimeLimit = 3.0f;
    public float hideTimeLimit = 1.0f;
    private float currTime;
    private Rigidbody2D rb2d;
    private Animator anim;
    bool hidden = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currTime = outTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= Time.deltaTime;

        if (currTime <= 0)
        {
            if (hidden)
            {
                int randState = Random.Range(0, 2);
                bool setting = randState == 1;
                Animator anim = GetComponent<Animator>();
                anim.SetBool("good", setting);
                rb2d.gravityScale = -1;
                hidden = false;
                currTime = outTimeLimit;


                bool isDead = anim.GetBool("dead");
                if (isDead)
                {
                    rb2d.gravityScale = -1;
                }

                hidden = false;
                currTime = outTimeLimit;
            }
            else
            {
                rb2d.gravityScale = 1;
                hidden = true;
                currTime = hideTimeLimit;
            }
        }
    }

    public void SetDead()
    {
        //dead = true;
    }
    
}