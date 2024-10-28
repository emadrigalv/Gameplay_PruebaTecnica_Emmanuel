using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin cbmcp;

    private float shakeTotalDuration;
    private float shakeTimer;
    private float startingIntensity;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        ShakeCountDown();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        cbmcp.m_AmplitudeGain = intensity;
        startingIntensity = intensity;

        shakeTotalDuration = duration;
        shakeTimer = duration;
    }

    private void ShakeCountDown()
    {
        if (shakeTimer > 0) { 
            shakeTimer -= Time.deltaTime;
            cbmcp.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer/shakeTotalDuration);
        }
        else
        {
            cbmcp.m_AmplitudeGain = 0f;
        }
    }
}
