using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerVision2 : MonoBehaviour    
{

    [SerializeField] GameObject followCamera;
    //[SerializeField] GameObject targetCamera;
    [SerializeField] GameObject MapCamera;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject CanvaPlayer;
    [SerializeField] GameObject TextIntro;
    [SerializeField] GameObject CanvasGender;
    [SerializeField] GameObject Camaragender;
    [SerializeField] GameObject CanvasGuia;

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



    public void changeStateMap()
    {
        if (!Inicio)
        {


            if (introm0)
            {

                if (audioController.ValIsPlay() && aux == 0)
                {
                    aux = 1;
                    TextIntro.SetActive(true);
                    btnsMap[0].SetActive(true);
                    this.GetComponent<ButtonGlowEffect>().enabled = true;
                    this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[0]);
                }

            }
            else
            {
                if (introm1)
                {
                    if (audioController.ValIsPlay() && aux == 1)
                    {
                        aux = 2;
                        TextIntro.SetActive(true);
                        btnsMap[1].SetActive(true);
                        this.GetComponent<ButtonGlowEffect>().enabled = true;
                        this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[1]);

                    }



                } else
                {
                    if (introm2)
                    {
                        if (audioController.ValIsPlay() && aux == 2)
                        {
                            aux = 3;
                            TextIntro.SetActive(true);
                            btnsMap[2].SetActive(true);
                            this.GetComponent<ButtonGlowEffect>().enabled = true;
                            this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[2]);

                        }
                    }
                    else
                    {
                        if (introm3)
                        {
                            if (audioController.ValIsPlay() && aux == 3)
                            {
                                aux = 4;
                                TextIntro.SetActive(true);
                                btnsMap[3].SetActive(true);
                                this.GetComponent<ButtonGlowEffect>().enabled = true;
                                this.GetComponent<ButtonGlowEffect>().changeColorbtn(btnsMap[3]);

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
            TextIntro.SetActive(false);
            introm0 = false;
            introm1 = true;
        } else
        {
            if (introm1)
            {
                audioController.PlayVoiceIni(2);
                this.GetComponent<ButtonGlowEffect>().enabled = false;
                btnsMap[1].SetActive(false);
                TextIntro.SetActive(false);
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
                    TextIntro.SetActive(false);
                    introm2 = false;
                    introm3 = true;
                }
                else
                {
                    if (introm3)
                    {
                        //desactivo elementos mapa
                        this.GetComponent<ButtonGlowEffect>().enabled = false;
                        btnsMap[3].SetActive(false);
                        //silueta.SetActive(false);
                        introm3 = false;
                        //Cambio camara
                        TextIntro.SetActive(false);
                        MapCamera.SetActive(false);

                        //activo elementos de canva guia
                        CanvasGuia.SetActive(true);
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
