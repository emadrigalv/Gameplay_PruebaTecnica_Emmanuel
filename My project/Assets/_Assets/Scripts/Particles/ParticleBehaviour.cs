using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private string collisionPlaneTag;

    private Transform[] collisionPlanes;

    private void Start()
    {
        var main = particle.main;
        main.stopAction = ParticleSystemStopAction.Disable;
    }

    private void OnEnable()
    {
        collisionPlanes = GetPlanesFromScene();

        if (collisionPlanes != null && collisionPlanes.Length > 0) SetupCollisionPlanes();

        particle.Play();
    }

    private Transform[] GetPlanesFromScene()
    {
        GameObject[] planeObjects = GameObject.FindGameObjectsWithTag(collisionPlaneTag);
        Transform[] planes = new Transform[planeObjects.Length];

        for (int i = 0; i < planeObjects.Length; i++) 
        {
            planes[i] = planeObjects[i].transform;
        }

        return planes;  
    }

    private void SetupCollisionPlanes()
    {
        var collisionModule = particle.collision;

        for (int i = 0; i < collisionPlanes.Length; i++)
        {
            collisionModule.SetPlane(i, collisionPlanes[i]);
        }
    }
}
