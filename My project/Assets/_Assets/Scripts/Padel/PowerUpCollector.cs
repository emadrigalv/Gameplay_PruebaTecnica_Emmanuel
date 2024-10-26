using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollector : MonoBehaviour
{
    public List<PowerUpBase> powerUps;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Power Up"))
        {
            Debug.Log("Si");

            int powerCollectedID =  collision.gameObject.GetComponent<Item>().ID;

            PowerUpBase power = powerUps[powerCollectedID];

            power.StartPowerUp();
        }  
    }
    
}
