using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCamPlayer2 : MonoBehaviour
{
    [SerializeField] GameObject followCamera;
    [SerializeField] GameObject CanvaPlayer;
    [SerializeField] GameObject textPlayer;

    //CanvaAmbunyMision
    [SerializeField] GameObject CanvaMision;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveCam()
    {
        followCamera.SetActive(false);
        CanvaPlayer.SetActive(false);
        this.GetComponent<PlayerInput>().enabled = false;
        CanvaMision.SetActive(true);
        textPlayer.GetComponent<TMP_Text>().text = "";

    }

    public void desactivoCam()
    {
        CanvaMision.SetActive(false);
        this.GetComponent<PlayerInput>().enabled = true;
        followCamera.SetActive(true);
        CanvaPlayer.SetActive(true);
        
    }
}
