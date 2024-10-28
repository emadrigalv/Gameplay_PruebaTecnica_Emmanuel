using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollector : MonoBehaviour
{
    [Header("Dependencies")]
    public List<PowerUpBase> powerUps;

    [Header("Parameters")]
    [SerializeField] private string sfxItemPickedTag;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Power Up"))
        {
            Item itemCollected = collision.gameObject.GetComponent<Item>();
            itemCollected.ItemPicked();

            AudioManager.instance.Play(sfxItemPickedTag);

            int powerCollectedID = itemCollected.ID;
            PowerUpBase power = powerUps[powerCollectedID];

            power.StartPowerUp();
        }  
    }
}
