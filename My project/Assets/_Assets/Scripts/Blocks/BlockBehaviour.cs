using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    [Header("Dependecies")]
    [SerializeField] private BlockColorHandler colorHandler;

    [Header("Parameters")]
    [SerializeField] [Range(0,2)] private int blockLevel;

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
                // TODO instance VFX block destroyed
                Pooler.instance.SpawnFromPool("VFX_Pink", transform.position);

                Destroy(gameObject);
            }
            else colorHandler.BlockHitAnimation();
        }
    }

}
