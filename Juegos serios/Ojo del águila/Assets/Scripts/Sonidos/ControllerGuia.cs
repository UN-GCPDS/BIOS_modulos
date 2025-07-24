using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerGuia : MonoBehaviour
{

    private bool audioFinalizo = false;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject CanvaPlayer;
    [SerializeField] GameObject followCamera;
    [SerializeField] GameObject Camaragender;
    public bool Inicio=false;


    public void PlaySound(AudioClip clipSound)
    {

        audioFinalizo = false;
        gameObject.GetComponent<AudioSource>().clip = clipSound;
        gameObject.GetComponent<AudioSource>().Play();
        audioFinalizo = false;
    }



    public bool ValIsPlay()
    {

        if (!gameObject.GetComponent<AudioSource>().isPlaying && !audioFinalizo)
        {
            audioFinalizo = true;
        }
        return audioFinalizo;
    }

    public void cerrarGuia()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        //elementos para jugar
        Player.GetComponent<PlayerInput>().enabled = true;
        CanvaPlayer.SetActive(true);
        followCamera.SetActive(true);
        Destroy(Camaragender);
        gameObject.SetActive(false);
        Inicio = true;

    }

}
