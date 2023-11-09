using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadTest : MonoBehaviour
{
    [SerializeField] private GameObject burnSpot;
    private List<GameObject> burnSpots = new List<GameObject>();

    private bool stop = false;

    private float timer, seconds = 1.0f;
    private bool timerOnOff = true;

    private int nextBurn;
    private Vector3 burnPos;
    private Quaternion burnRot;




    private void Start()
    {
        timer = seconds;
        nextBurn = 0;
        burnPos = transform.position + Vector3.left * 0.8f;
        burnRot = Quaternion.identity; // plusser Vector3(0, 1 + nextBurn, 0), så den alltid mer opp idk
    }

    private void Update()
    {
        if (nextBurn > 10)
            return;

        if (timerOnOff == true)
            timer -= Time.deltaTime;

        if (timer < 0)
        {


            if (burnSpots.Count > 0)
                burnPos = burnSpots[nextBurn - 1].transform.position + Vector3.left * 0.8f;

            GameObject burnSpotSpawn = Instantiate(burnSpot, burnPos, burnRot, null);
            burnSpots.Add(burnSpotSpawn);
           
          

            nextBurn++;
            

            timer = seconds;
            
        }
    }
}
