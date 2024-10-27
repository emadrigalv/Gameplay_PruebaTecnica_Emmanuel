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

            Item itemCollected = collision.gameObject.GetComponent<Item>();
            itemCollected.ItemPicked();

            int powerCollectedID = itemCollected.ID;

            PowerUpBase power = powerUps[powerCollectedID];

            power.StartPowerUp();
        }  
    }
    
}
