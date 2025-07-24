using System.Collections.Generic;
using UnityEngine;

public class ControllerCanvas : MonoBehaviour
{
    [SerializeField] private GameObject CanvasAlbum;
    [SerializeField] private GameObject CanvasMision;
    [SerializeField] private GameObject CanvasConfig;
    [SerializeField] private GameObject CanvasJuego;
    [SerializeField] public GameController controllergame;
    [SerializeField] public List<GameObject> ObjectsNivel;

    public void nextMision()
    {
        CanvasAlbum.SetActive(false);
        CanvasMision.SetActive(true);
        CanvasConfig.SetActive(false);
        CanvasJuego.SetActive(false);
    }

    public void nextConfig()
    {
        CanvasAlbum.SetActive(false);
        CanvasMision.SetActive(false);
        CanvasConfig.SetActive(true);
        CanvasJuego.SetActive(false);
    }

    public void StartChangedMision()
    {

        controllergame.ChangeCantCurrentMission(CanvasConfig.GetComponent<CanvaConfigController>().getNewCant());
        controllergame.ChangeTimeCurrentMission(CanvasConfig.GetComponent<CanvaConfigController>().getNewTime());
        CanvasAlbum.SetActive(false);
        CanvasMision.SetActive(false);
        CanvasConfig.SetActive(false);
        startNivel();
        CanvasJuego.SetActive(true);

        //activeMision();

    }

    public void StartDefaultMision()
    {
        Debug.Log("activo2");
        CanvasAlbum.SetActive(false);
        CanvasMision.SetActive(false);
        CanvasConfig.SetActive(false);
        startNivel();
        Debug.Log("activo3");
        CanvasJuego.SetActive(true);
        Debug.Log("activo4");

    }

    public void startNivel()
    {
        Mision actual = controllergame.getCurrentMission();
        if (actual.nombre == 0) { 
        ObjectsNivel[0].SetActive(true);
        }
        if (actual.nombre == 1)
        {
            ObjectsNivel[1].SetActive(true);
        }
        if (actual.nombre == 2)
        {
            ObjectsNivel[2].SetActive(true);
        }
        if (actual.nombre == 3)
        {
            ObjectsNivel[3].SetActive(true);
        }
        if (actual.nombre == 4)
        {
            ObjectsNivel[4].SetActive(true);
        }



    }
}
