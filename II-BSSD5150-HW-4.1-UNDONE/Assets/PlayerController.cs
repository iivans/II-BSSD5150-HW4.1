using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    Rigidbody2D m_Rigidbody;
    public float m_Speed = 5f;
    Animator anim;
    Collider2D swordCollider;
    int goodSnowmenHit = 0;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey("space"))
        {
            anim.SetBool("Attacking", true);
        }
        else
        {
            anim.SetBool("Attacking", false);
        }
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            swordCollider.enabled = true;
        }
        else
        {
            swordCollider.enabled = false;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 m_Input = new Vector3(h, v, 0);

        if (h != 0 || v != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (anim.GetBool("Attacking"))
        {
            Animator snowmanAnimator = collision.gameObject.GetComponent<Animator>();
            if (snowmanAnimator != null)
            {
                if (collision.CompareTag("Enemy") && snowmanAnimator.GetBool("good"))
                {
                    goodSnowmenHit++;

                    if (goodSnowmenHit >= 2)
                    {
                        m_Rigidbody.gravityScale = 100;
                    }
                }

                snowmanAnimator.SetBool("dead", true);
            }
        }

        if (collision.CompareTag("CliffTrigger"))
        {
            m_Rigidbody.gravityScale = 10f; 
        }

        if (collision.CompareTag("OutOfBound"))
        {
            transform.position = spawnPoint.position;
            m_Rigidbody.gravityScale = 0f;
            goodSnowmenHit = 0; 
        }
    }
}
