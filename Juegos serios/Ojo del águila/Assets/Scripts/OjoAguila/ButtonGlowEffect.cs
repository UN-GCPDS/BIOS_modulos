using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonGlowEffect : MonoBehaviour
{
    private Color startColor = Color.white;
    private Color endColor = Color.red; // Color del "brillo"
    public float speed = 1.0f;

    private GameObject buttonImage;
    private float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine("rechange"); 
    
    }

    public void changeColorbtn(GameObject buttonImagechange)
    {
        buttonImage=buttonImagechange;
        float t = Mathf.PingPong(time * speed, 1f);
        buttonImage.GetComponent<Image>().color = Color.Lerp(startColor, endColor, t);
        time += Time.deltaTime;
    }

    IEnumerator rechange(){

        yield return new WaitForSeconds(time);
        float t = Mathf.PingPong(time * speed, 1f);
        buttonImage.GetComponent<Image>().color = Color.Lerp(endColor, startColor, t);
        time += Time.deltaTime;


    }
}