using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollector : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PadelController paddle;
    public List<PowerUpBase> powerUps;

    [Header("Parameters")]
    [SerializeField] private string sfxItemPickedTag;

    private void OnTriggerEnter(Collider collision)
    {
        if (!paddle.isAlive) return;

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

    public void StopPowerUps()
    {
        foreach(PowerUpBase power in powerUps)
        {
            power.StopPowerUp();
        }
    }
}
