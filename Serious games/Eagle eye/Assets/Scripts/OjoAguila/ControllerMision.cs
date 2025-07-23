using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


[System.Serializable]public struct Posanimal
{

    public GameObject pos;
    public bool aire;
}

public struct TuplePoint
{
    public string nombre;
    public int aciertos;
}
public class ControllerMision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public List<Animal> ltsAnimals;
    private int cantidadAparicion;
    private int cantidadObjetivo;
    private int tamaListaMision;
    private float tiempoAparicion;
    [SerializeField] GameController gameController;
    [SerializeField] AudioIni audioController;
    [SerializeField] private GameObject TextAlertGame;
    [SerializeField] private GameObject TextPuntaje;
    [SerializeField] private GameObject Texterrores;
    [SerializeField] private List<Posanimal> posAparicion;
    [SerializeField] GameObject followCamera;
    [SerializeField] GameObject btnCanvas;
    [SerializeField] GameObject CanvaPlayer;
    [SerializeField] GameObject ControllerZone;
    [SerializeField] GameObject TextCanvaPlayer;
    [SerializeField] GameObject CanvaJuego;
    [SerializeField] GameObject player;
    [SerializeField] GameObject ActiveMision;
    [SerializeField] GameObject eventDis;
    private Mision StartingMision;
    private bool listfin;
    private List<TuplePoint> tuplePoint; 
    private int totalaciertos;
    private int totalerrores; 
    int cont;
    int aciertosAnimal = 4;
    private  GameObject lastclone;
    DiccionarioEventos events = new DiccionarioEventos();

    void OnEnable()
    {
        Debug.Log("REINICIO");
        tuplePoint = new List<TuplePoint>();
        listfin = false;
        totalaciertos = 0;
        totalerrores = 0;
        cont = 0;
        TextPuntaje.GetComponent<TMP_Text>().text = "Capturas exitosas: " + totalaciertos;
        Texterrores.GetComponent<TMP_Text>().text = "Intentos fallidos: " + totalerrores;
        StartingMision = gameController.getCurrentMission();
        tiempoAparicion = StartingMision.tiempoAnimal;
        cantidadAparicion = StartingMision.CantidadAparicion;
        cantidadObjetivo = StartingMision.ltsAnimalsTarget.Count;
        tamaListaMision = (cantidadAparicion * cantidadObjetivo) + ((cantidadAparicion * cantidadObjetivo));
        Debug.Log("TAMA " +tamaListaMision);
        ltsAnimals = new List<Animal>(tamaListaMision);
        InicializarLista();
        createListAnimal();
        for (int i = 0; i < cantidadObjetivo; i++)
        {
            tuplePoint.Add(new TuplePoint {nombre = StartingMision.ltsAnimalsTarget[i].nombre,aciertos= 0 });
        }
        TextAlertGame.SetActive(true);
        TextAlertGame.GetComponent<TMP_Text>().text = "Iniciará la misión en...";
        StartCoroutine(ControllerCorrutine());        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (totalaciertos == (aciertosAnimal * cantidadObjetivo) && cont <1)
        {
            MoneLibrary.SendUsbData(events.misionok);
            StartCoroutine(eventDisplay());
            MoneLibrary.SendUsbData(events.fin);
            StartCoroutine(eventDisplay());
            StopCoroutine(ControllerCorrutine());
            StopCoroutine(LookAnimals());
            TextCanvaPlayer.GetComponent<TMP_Text>().text = "Misión finalizada con éxito";
            TextCanvaPlayer.SetActive(true);
            StartingMision.finalizo = true;
            player.GetComponent<PlayerInput>().enabled = true;
            followCamera.SetActive(true);
            CanvaPlayer.SetActive(true);
            cont++;
            ActiveMision.SetActive(false);
            btnCanvas.SetActive(false);
            StartCoroutine (MensajefinMision());
            CanvaJuego.SetActive(false);
            Destroy(ControllerZone);
            Destroy(gameObject);
        }
        if (listfin && totalaciertos < (aciertosAnimal * cantidadObjetivo))
        {
            totalaciertos = 0;
            cont = 0;
            listfin = false;
            StopCoroutine(LookAnimals());
            StopCoroutine(ControllerCorrutine());
            TextCanvaPlayer.GetComponent<TMP_Text>().text = "Intenta de nuevo";
            TextCanvaPlayer.SetActive(true);
            player.GetComponent<PlayerInput>().enabled = true;
            followCamera.SetActive(true);
            CanvaPlayer.SetActive(true);
            StartCoroutine(MensajefinMision());
            CanvaJuego.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        Destroy(lastclone);
    }


    public void InicializarLista()
    {
        for (int i = 0; i < tamaListaMision; i++)
        {
            ltsAnimals.Add(null); // Llena con valores nulos inicialmente
        }
    }

    public void createListAnimal()
    {
        
        int aux1 = 0, aux2 = 0;        
        bool llena = false;
        while (!llena)
        {
            int pos = UnityEngine.Random.Range(0, tamaListaMision - 1);
            if (ltsAnimals[pos] == null)
            {
                //Debug.Log("Posicion:" + pos);
                if (cantidadObjetivo == 1)
                {
                    //Debug.Log("Lista insertando");
                    ltsAnimals[pos] = StartingMision.ltsAnimalsTarget[0];
                    aux1++;
                    if (aux1 == (cantidadAparicion))
                    {
                        //Debug.Log("Se lleno");
                        llena = true;
                    }
                }
                else
                {
                    if (aux1 == aux2)
                    {
                        ltsAnimals[pos] = StartingMision.ltsAnimalsTarget[0];
                        aux1++;
                    }
                    else
                    {
                        if (aux2 < aux1)
                        {
                            ltsAnimals[pos] = StartingMision.ltsAnimalsTarget[1];
                            aux2++;
                        }
                    }

                    if (aux1 == cantidadAparicion && aux2 == cantidadAparicion)
                    {
                        llena = true;
                    }
                }

            }

        }

        llenarNoTarget();
    }

    public void llenarNoTarget()
    {
        for (int i = 0; i < ltsAnimals.Count; i++)
        {
                        
            int rangeAnimal = UnityEngine.Random.Range(0, StartingMision.ltsAnimalsNOTarget.Count-1);
            if (ltsAnimals[i] == null)
            {
                ltsAnimals[i] = StartingMision.ltsAnimalsNOTarget[rangeAnimal];                   
                
            }
            
        }
    }

    IEnumerator conteoRegresivo()
    {
        yield return new WaitForSeconds(1f);
        TextAlertGame.GetComponent<TMP_Text>().text = "3";
        yield return new WaitForSeconds(1f);

        TextAlertGame.GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1f);

        TextAlertGame.GetComponent<TMP_Text>().text = "1";
        yield return new WaitForSeconds(1f);

        TextAlertGame.GetComponent<TMP_Text>().text = "INICIA!";
        yield return new WaitForSeconds(1f);
        MoneLibrary.SendUsbData(events.inicio);
        StartCoroutine(eventDisplay());

        TextAlertGame.gameObject.SetActive(false);
    }

    public int getPosAire()
    {
        int pos = UnityEngine.Random.Range(0, posAparicion.Count - 1);
        while(!posAparicion[pos].aire)
        {
            pos = UnityEngine.Random.Range(0, posAparicion.Count - 1);
        }
        return pos;
    }

    public int getPosTierra()
    {
        int pos = UnityEngine.Random.Range(0, posAparicion.Count - 1);
        while (posAparicion[pos].aire)
        {
            pos = UnityEngine.Random.Range(0, posAparicion.Count - 1);
        }
        return pos;
    }


    IEnumerator LookAnimals()
    {

        TextAlertGame.gameObject.SetActive(false);
        for (int i = 0; i < ltsAnimals.Count; ++i)
        {
            GameObject Animal;
            int pos;
            MoneLibrary.SendUsbData(events.aparicion);
            StartCoroutine(eventDisplay());
            if (ltsAnimals[i].aire)
            {
                pos = getPosAire();
                Animal = Instantiate(ltsAnimals[i].obj3d, posAparicion[pos].pos.transform.position, posAparicion[pos].pos.transform.rotation);
                lastclone = Animal;



            }
            else
            {
                pos = getPosTierra();
                Animal = Instantiate(ltsAnimals[i].obj3d, posAparicion[pos].pos.transform.position, posAparicion[pos].pos.transform.rotation);
                lastclone = Animal;
            }           
            yield return new WaitForSeconds(tiempoAparicion);
            Destroy(Animal);
            MoneLibrary.SendUsbData(events.desaparicion);
            StartCoroutine(eventDisplay());
        }
        listfin = true;
        
    }

    IEnumerator ControllerCorrutine()
    {
        yield return StartCoroutine(conteoRegresivo());
        yield return StartCoroutine(LookAnimals());
    }


    public void ValTouch(GameObject touchObject)
    {
        if (touchObject.TryGetComponent<ControllerIDAnimal>(out ControllerIDAnimal controanimal))
        {
            bool acerto = false;
            for (int i = 0; i < tuplePoint.Count; i++)
            {
                if (touchObject.GetComponent<ControllerIDAnimal>().nombre.Equals(tuplePoint[i].nombre) && tuplePoint[i].aciertos < aciertosAnimal)
                {
                    var newvalue = tuplePoint[i];
                    newvalue.aciertos += 1;
                    tuplePoint[i] = newvalue;
                    aciertos();
                    MoneLibrary.SendUsbData(events.tomaok);
                    StartCoroutine(eventDisplay());
                    StartCoroutine(MensajeRetro("Lo hiciste bien, sigue así"));
                    Destroy(touchObject);
                    acerto = true;
                    audioController.PlayVoiceIni(9);
                    break;
                }
            }
            if (!acerto)
            {
                Destroy(touchObject);
                MoneLibrary.SendUsbData(events.tomako);
                StartCoroutine(eventDisplay());
                totalerrores = totalerrores + 1;
                Texterrores.GetComponent<TMP_Text>().text = "Intentos fallidos: " + totalerrores;
                audioController.PlayVoiceIni(8);
                StartCoroutine(MensajeRetro("Estuviste cerca, pero lo puedes hacer mejor"));
            }
        }

        
    }

    IEnumerator MensajeRetro(string alerta)
    {
        
        TextAlertGame.GetComponent<TMP_Text>().text = alerta;
        TextAlertGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        TextAlertGame.gameObject.SetActive(false);
    }


    public void aciertos()
    {
        int aux = 0;
        for (int i = 0; i < tuplePoint.Count; i++)
        {

            aux = aux + tuplePoint[i].aciertos;
        }
        totalaciertos = aux;
        TextPuntaje.GetComponent<TMP_Text>().text = "Capturas exitosas: " + totalaciertos;
    }

    

    IEnumerator MensajefinMision()
    {
                
        yield return new WaitForSeconds(2f);
        TextCanvaPlayer.gameObject.SetActive(false);
    }

    public void finalizarMision()
    {
        totalaciertos = 0;
        cont = 0;
        listfin = false;
        StopCoroutine(ControllerCorrutine());
        StopCoroutine(LookAnimals());
        TextCanvaPlayer.GetComponent<TMP_Text>().text = "Intenta de nuevo";
        TextCanvaPlayer.SetActive(true);
        player.GetComponent<PlayerInput>().enabled = true;
        followCamera.SetActive(true);
        CanvaPlayer.SetActive(true);
        StartCoroutine(MensajefinMision());
        CanvaJuego.SetActive(false);
    }

    IEnumerator eventDisplay()
    {
        Color changeColor = Color.white;
        Color originalColor = Color.black;
        eventDis.GetComponent<Image>().color = changeColor;
        yield return new WaitForSeconds(0.2f);
        eventDis.GetComponent<Image>().color = originalColor;
    }


}
