using UnityEngine;
using System.Collections.Generic;

public class AudioIni : MonoBehaviour
{
    [SerializeField] AudioSource dialogsSources;
    [SerializeField] List<AudioClip> clipsNarra;
    private bool audioFinalizo = false;
    
    public void PlayVoiceIni(int pos)
    {
        audioFinalizo = false;
        dialogsSources.clip = clipsNarra[pos];
        dialogsSources.Play();
        audioFinalizo = false;
    }



    public bool ValIsPlay()
    {
        
        if (!dialogsSources.isPlaying && !audioFinalizo )
        {
            audioFinalizo = true;
        }
        return audioFinalizo;
    }   
}
