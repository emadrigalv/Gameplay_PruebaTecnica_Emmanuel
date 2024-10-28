using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    [Header("Dependecies")]
    [SerializeField] private BlockColorHandler colorHandler;

    [Header("Parameters")]
    [SerializeField, Range(0,2)] private int blockLevel;
    [SerializeField] private int powerUpChance = 4;
    [SerializeField] private float shakeIntensity = 2;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private string vfxTag;
    [SerializeField] private string sfxBlockDestroyedTag;
    [SerializeField] private string sfxBlockHitTag;
    [SerializeField] private List<string> powerUpsList;

    private int blockHealthPoints;

    void Start()
    {
        InitializeBlock();
    }

    private void InitializeBlock()
    {
        // Defiune block health using ecauation (2*n + 1)
        blockHealthPoints = (2 * blockLevel) + 1;

        colorHandler.InitializeBlockColor(blockLevel);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            blockHealthPoints--;

            if (blockHealthPoints <= 0)
            {
                // TODO update score
                CameraShake.instance.ShakeCamera(shakeIntensity, shakeDuration);
                AudioManager.instance.Play(sfxBlockDestroyedTag); 
                Pooler.instance.SpawnFromPool(vfxTag, transform.position);
                ChanceToPowerUp();
                Destroy(gameObject);
            }
            else
            {
                colorHandler.BlockHitAnimation();
                AudioManager.instance.Play(sfxBlockHitTag);
            }
        }
    }

    private void ChanceToPowerUp()
    {
        int randomNumber = Random.Range(1, (powerUpChance + 1));
        int randomPowerUp = Random.Range(0, powerUpsList.Count);

        if (randomNumber == 1)
        {
            Pooler.instance.SpawnFromPool(powerUpsList[randomPowerUp], transform.position);
        }
        else return;
    }

}
