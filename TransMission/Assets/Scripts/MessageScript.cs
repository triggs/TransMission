using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScript : MonoBehaviour {

    [SerializeField]
    Canvas messageCanvas;
    
    // Use this for initialization
    void Start () {
        messageCanvas.enabled = false;
        MessageOff();
    }

    //// Update is called once per frame
    //void Update () {

    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            messageCanvas.enabled = true;
            //MessageOn();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Enemy")
        {
            MessageOff();
        }
    }

    private void MessageOn(bool randomQuote = false)
    {
        // messageCanvas.guiText = SelectRandomSixteenthCenturyPoetryLine(messageCanvas.guiText);
        messageCanvas.enabled = true;
    }

    private void MessageOff()
    {
        messageCanvas.enabled = false;
    }

    private string SelectRandomSixteenthCenturyPoetryLine(string current)
    {
        return current;
    }

}
