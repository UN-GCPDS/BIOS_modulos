using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerCanvaMision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] public GameController controllergame;
    void OnEnable()
    {
        chargeAlbum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chargeAlbum()
    {
        int posAlbum = 0;
        List<Mision> misiones = controllergame.ltsMision;

        for (int i = 1; i < misiones.Count-1; i++)
        {
            List<Animal> objetivos = misiones[i].ltsAnimalsTarget;

            for (int k = 0; k < objetivos.Count; k++)
            {
                    
                if (misiones[i].finalizo)
                {
                    GameObject gameObject = GameObject.Find("Album" + posAlbum);
                    gameObject.GetComponent<Image>().sprite = objetivos[k].renderImage.sprite;
                    gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    posAlbum++;
                }
                else
                {
                    GameObject gameObject = GameObject.Find("Album" + posAlbum);
                    gameObject.GetComponent<Image>().sprite = objetivos[k].renderImage.sprite;
                    posAlbum++;
                }

            }
                    

        }

    }
    
        
}
