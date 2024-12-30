using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPoints : MonoBehaviour
{
    public int pointsWorth;
    public ScoreManager scoreManager;

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Tip")
    //     {
    //         scoreManager.ChangeScore(pointsWorth);
    //     }
    // }

    public void UpdatePoints()
    {
        scoreManager.ChangeScore(pointsWorth);
    }
}
