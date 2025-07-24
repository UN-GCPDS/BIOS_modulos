using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ControllerCanvasGender : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Button mbtn;
    [SerializeField] Button fbtn;
    [SerializeField] GameObject PlayerM;
    [SerializeField] GameObject PlayerF;
    [SerializeField] GameObject gameController;
    [SerializeField] GameObject cineMachineBrain;
    [SerializeField] GameObject CanvasGender;
    [SerializeField] GameObject CamJuegoGender;

    bool mgender;
    bool fgender;
    void Start()
    {
        cineMachineBrain.GetComponent<CinemachineBrain>().DefaultBlend = new(CinemachineBlendDefinition.Styles.Cut, 2f);
    }

    // Update is called once per frame
    void Update()
    {

        if (fgender)
        {
            CamJuegoGender.SetActive(true);
            PlayerF.SetActive(true);
            Destroy(PlayerM);
            Destroy(CanvasGender);
            gameController.GetComponent<GameController>().introGender = true;
            gameController.GetComponent<GameController>().gender = "f";
            Destroy(this.gameObject);
        }
        if (mgender)
        {
            CamJuegoGender.SetActive(true);
            PlayerM.SetActive(true);
            Destroy(PlayerF);
            Destroy(CanvasGender);
            gameController.GetComponent<GameController>().introGender = true;
            gameController.GetComponent<GameController>().gender = "m";
            Destroy(this.gameObject);
        }

    }



    public void selFGender()
    {
        fgender = true;
        fbtn.GetComponent<Button>().enabled = false;
        mbtn.GetComponent<Button>().enabled = false;
    }

    public void selMGender()
    {
        mgender = true;
        mbtn.GetComponent<Button>().enabled = false;
        fbtn.GetComponent<Button>().enabled = false;
    }
}
