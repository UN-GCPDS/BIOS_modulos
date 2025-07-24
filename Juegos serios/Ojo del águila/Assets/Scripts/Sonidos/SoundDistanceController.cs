using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SoundDistanceController : MonoBehaviour
{
    public Transform player;
    public float maxPanAngle = 45f; // Ángulo máximo para pan completo

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);

        // Normaliza el ángulo a valor de panorama (-1 a 1)
        float panValue = Mathf.Clamp(angle / maxPanAngle, -1f, 1f);

        audioSource.panStereo = panValue;

        // Opcional: ajustar volumen por distancia
        float distance = directionToPlayer.magnitude;
        audioSource.volume = Mathf.Clamp01(1 - distance / audioSource.maxDistance);
    }
}