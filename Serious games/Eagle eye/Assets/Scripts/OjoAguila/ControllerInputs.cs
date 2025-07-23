using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class ControllerInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public bool Inicio;
    private bool introm0 = false;
    private bool introm1 = false;
    private bool introm2 = false;
    private bool introm3 = false;
    [SerializeField] GameObject TextoGuia;
    [SerializeField] GameObject btnsprint;
    [SerializeField] GameObject btnJump;
    [SerializeField] GameObject btnCam;
    [SerializeField] GameObject btnContinuar;
    [SerializeField] AudioIni audioController;
    [SerializeField] GameObject ControllerMision0;
    private GameObject gamecontroller;

    private int aux = 0;

    void Start()
    {
        gamecontroller = GameObject.Find("GameController");
        TextoGuia.SetActive(true);
        btnContinuar.SetActive(false);
        btnJump.SetActive(false);
        btnCam.SetActive(false);
        btnsprint.SetActive(false);
        audioController.PlayVoiceIni(4);
        introm0 = true;
    }

    // Update is called once per frame
    void Update()
    {
        changeStateIntro();
    }

    private void changeStateIntro()
    {

        if (!Inicio)
        {


            if (introm0)
            {

                if (audioController.ValIsPlay() && aux == 0)
                {
                    aux = 1;
                    btnContinuar.SetActive(true);

                }

            }
            else
            {
                if (introm1)
                {

                    if (audioController.ValIsPlay() && aux == 1)
                    {
                        aux = 2;
                        //positionTextguia(btnsprint, TextoGuia);
                        btnContinuar.SetActive(true);
                        btnsprint.SetActive(true);
                        TextoGuia.GetComponent<TMP_Text>().text= "Mantén presionado el botón de Sprint y al moverte con el Joystick podrás correr";
                        TextoGuia.SetActive(true);

                    }

                } else
                {
                    if (introm2)
                    {

                        if (audioController.ValIsPlay() && aux == 2)
                        {
                            aux = 3;
                            //positionTextguia(btnJump, TextoGuia);
                            btnContinuar.SetActive(true);
                            btnJump.SetActive(true);
                            TextoGuia.GetComponent<TMP_Text>().text = "Presiona el botón para saltar";
                            TextoGuia.SetActive(true);

                        }

                    }
                    else
                    {
                        if (introm3)
                        {

                            if (audioController.ValIsPlay() && aux == 3)
                            {
                                aux = 4;
                                //positionTextguia(btnCam, TextoGuia);
                                btnContinuar.SetActive(true);
                                btnCam.SetActive(true);
                                btnCam.GetComponent<Button>().enabled = false;
                                TextoGuia.GetComponent<TMP_Text>().text = "En las zonas de avistamiento, usa el botón de cámara para visualizar los animales";
                                TextoGuia.SetActive(true);

                            }

                        }
                    }
                }

            }

        }
    }



    public void tutorialClick()
    {

        if (introm0)
        {
            audioController.PlayVoiceIni(5);
            btnContinuar.SetActive(false);
            TextoGuia.SetActive(false);
            introm0 = false;
            introm1 = true;
        }
        else
        {
            if (introm1)
            {
                audioController.PlayVoiceIni(6);
                btnContinuar.SetActive(false);
                TextoGuia.SetActive(false);
                introm1 = false;
                introm2 = true;
            }
            else
            {
                if (introm2)
                {
                    audioController.PlayVoiceIni(7);
                    btnContinuar.SetActive(false);
                    TextoGuia.SetActive(false);
                    introm2 = false;
                    introm3 = true;
                }
                else
                {
                    if (introm3)
                    {
                        gamecontroller.GetComponent<GameController>().mision0 = true;
                        ControllerMision0.SetActive(true);
                        TextoGuia.GetComponent<TMP_Text>().text = "Pista: Busca el trípode o la cámara para iniciar la misión de entrenamiento.";
                        introm3 = false;
                        //Cambio camara
                        btnContinuar.SetActive(false);
                        btnCam.GetComponent<Button>().enabled = true;
                        btnCam.SetActive(false);
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
        textTransform.anchoredPosition = new Vector2(btnPosition.x -8 , btnPosition.y + 8);
    }

}
