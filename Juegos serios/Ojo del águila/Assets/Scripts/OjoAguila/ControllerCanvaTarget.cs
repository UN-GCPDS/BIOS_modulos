using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerCanvaTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] public GameController controllergame;
    void OnEnable()
    {
        chargeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chargeTarget()
    {
        Mision ActualMision = controllergame.getCurrentMission();
        Debug.Log("Nom mision:: " + ActualMision.nombre);
        Debug.Log("canti animals " + ActualMision.ltsAnimalsTarget.Count);
        if (ActualMision.ltsAnimalsTarget.Count <2){
            
            GameObject gameObject = GameObject.Find("Target0");
            gameObject.GetComponent<Image>().sprite = ActualMision.ltsAnimalsTarget[0].renderImage.sprite;
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else{
            Debug.Log("canti animals");
            for (int i=0; i<ActualMision.ltsAnimalsTarget.Count; i ++){
                GameObject gameObject = GameObject.Find("Target"+i);
                gameObject.GetComponent<Image>().sprite = ActualMision.ltsAnimalsTarget[i].renderImage.sprite;
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }  

}
