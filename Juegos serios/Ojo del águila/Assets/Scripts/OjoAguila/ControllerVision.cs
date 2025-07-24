using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerVision : MonoBehaviour    
{

    [SerializeField] GameObject followCamera;
    //[SerializeField] GameObject targetCamera;
    [SerializeField] GameObject MapCamera;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject CanvaPlayer;
    [SerializeField] GameObject TextGuia;
    [SerializeField] GameObject TextIntro;
    //[SerializeField] GameObject silueta;

    public bool Inicio;
    private bool introm0 = false;
    private bool introm1 =false;
    private bool introm2 = false;
    private bool introm3 = false;
    private int aux = 0;


    [SerializeField] int tiempoEspera;

    [SerializeField] AudioIni audioController;
    [SerializeField] List<GameObject> btnsMap;

    void Start()
    {
        this.GetComponent<ButtonGlowEffect>().enabled = false;
        CanvaPlayer.SetActive(false);
        followCamera.SetActive(false);
        //targetCamera.SetActive(false);
        MapCamera.SetActive(true);
        //StartCoroutine("Esperar");
        audioController.PlayVoiceIni(0);
        introm0 = true;
       // silueta.GetComponent<Animator>().SetBool("isTalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        changeStateMap();
    }




    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(tiempoEspera);
        Player.SetActive(true);
        CanvaPlayer.SetActive(true);
        followCamera.SetActive(true);
        //targetCamera.SetActive(false);
        MapCamera.SetActive(false);
        Debug.Log("Cambio de camara");
    }



    public void changeStateMap()
    {
        if (!Inicio)
        {


            if (introm0)
            {

                if (audioController.ValIsPlay() && aux == 0)
                {
                    Debug.Log("btn 1" + aux);
                    aux = 1;
                    //silueta.GetComponent<Animator>().SetBool("isTalking", false);
                    TextGuia.SetActive(true);
                    btnsMap[0].SetActive(true);
                    this.GetComponent<ButtonGlowEffect>().enabled = true;
                    this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[0]);
                    TextIntro.SetActive(false);
                }

            }
            else
            {
                if (introm1)
                {
                    if (audioController.ValIsPlay() && aux == 1)
                    {
                        Debug.Log("btn 2");
                        aux = 2;
                        //silueta.GetComponent<Animator>().SetBool("isTalking", false);
                        positionTextguia(btnsMap[1], TextGuia);
                        TextGuia.SetActive(true);
                        btnsMap[1].SetActive(true);
                        this.GetComponent<ButtonGlowEffect>().enabled = true;
                        this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[1]);
                        TextIntro.SetActive(false);

                    }



                } else
                {
                    if (introm2)
                    {
                        if (audioController.ValIsPlay() && aux == 2)
                        {
                            Debug.Log("btn 3");
                            aux = 3;
                            //silueta.GetComponent<Animator>().SetBool("isTalking", false);
                            positionTextguia(btnsMap[2], TextGuia);
                            TextGuia.SetActive(true);
                            btnsMap[2].SetActive(true);
                            this.GetComponent<ButtonGlowEffect>().enabled = true;
                            this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[2]);
                            TextIntro.SetActive(false);

                        }
                    }
                    else
                    {
                        if (introm3)
                        {
                            if (audioController.ValIsPlay() && aux == 3)
                            {
                                Debug.Log("btn 4");
                                aux = 4;
                                //silueta.GetComponent<Animator>().SetBool("isTalking", false);
                                positionTextguia(btnsMap[3], TextGuia);
                                TextGuia.SetActive(true);
                                btnsMap[3].SetActive(true);
                                this.GetComponent<ButtonGlowEffect>().enabled = true;
                                this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[3]);
                                TextIntro.SetActive(false);

                            }
                        }
                    }
                }
            }
        }
    }

    public void clicM1()
    {
        
        if (introm0)
        {
            audioController.PlayVoiceIni(1);
            this.GetComponent<ButtonGlowEffect>().enabled = false;
            btnsMap[0].SetActive(false);
            TextGuia.SetActive(false);
            TextIntro.SetActive(true);
            TextIntro.GetComponent<TMP_Text>().text = "Hoy, serás parte del equipo de conservación encargado de documentar a los animales más esquivos";
            introm0 = false;
            introm1 = true;
        } else
        {
            if (introm1)
            {
                audioController.PlayVoiceIni(2);
                this.GetComponent<ButtonGlowEffect>().enabled = false;
                btnsMap[1].SetActive(false);
                TextGuia.SetActive(false);
                TextIntro.SetActive(true);
                TextIntro.GetComponent<TMP_Text>().text = "Usa tu cámara, mantén la calma y afina tu visión";
                introm1 = false;
                introm2 = true;
            }
            else
            {
                if (introm2)
                {
                    audioController.PlayVoiceIni(3);
                    this.GetComponent<ButtonGlowEffect>().enabled = false;
                    btnsMap[2].SetActive(false);
                    TextGuia.SetActive(false);
                    TextIntro.SetActive(true);
                    TextIntro.GetComponent<TMP_Text>().text = "un verdadero ojo de águila será necesario para triunfar";
                    introm2 = false;
                    introm3 = true;
                }
                else
                {
                    if (introm3)
                    {
                        this.GetComponent<ButtonGlowEffect>().enabled = false;
                        btnsMap[3].SetActive(false);
                        TextGuia.SetActive(false);
                        //silueta.SetActive(false);
                        introm3 = false;
                        //Cambio camara
                        
                        
                        MapCamera.SetActive(false);
                        Player.GetComponent<PlayerInput>().enabled = true;
                        CanvaPlayer.SetActive(true);
                        followCamera.SetActive(true);
                        // pendiente poner una corrutina que espere la transicion de camaras para destruirla
                        //Destroy(MapCamera);
                        Inicio = true;


                    }
                }
            }
        }        
    }

    public void positionTextguia(GameObject btn, GameObject text)
    {
        RectTransform textTransform = text.GetComponent<RectTransform>();
        RectTransform btnTransform = btn.GetComponent<RectTransform>();
        Vector2 btnPosition = btnTransform.anchoredPosition;
        textTransform.anchoredPosition = new Vector2(btnPosition.x, btnPosition.y + 8);
    }


    IEnumerator EsperaAnimator()
    {
        yield return new WaitForSeconds(5);
    }
}
