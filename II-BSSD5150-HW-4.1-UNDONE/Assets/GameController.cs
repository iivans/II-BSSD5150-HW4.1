using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float timeLimit = 10f;
    private float currTime;
    private SnowmanController[] SnowmanP;

    // Start is called before the first frame update
    void OnEnable()
    {
        SnowmanP = FindObjectsOfType<SnowmanController>();
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        foreach (SnowmanController sm in SnowmanP)
        {
            sm.hideTimeLimit = Random.Range(1.0f, 3.0f);
            sm.outTimeLimit = Random.Range(3.0f, 5.0f);
        }
    }

    // Update is called once per frame
    void Start()
    {
        currTime = timeLimit;
    }

    void Update()
    {
        currTime -= Time.deltaTime;

        if (currTime <= 0)
        {
            Debug.Log("You Lose");

            // Call SetDead for each snowman to stop animations and hide them
            foreach (SnowmanController sm in SnowmanP)
            {
                sm.SetDead();
            }
        }
        else if (currTime < timeLimit / 2)
        {
            timeLimit /= 2;
            UpdateTimers();
        }
    }
}
