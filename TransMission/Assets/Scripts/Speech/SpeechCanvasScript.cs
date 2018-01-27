using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpeechCanvasScript : MonoBehaviour {

    // Public objects
    public GameObject nme;
    public Canvas canvas;
    public double speachRadius = 20;

    // Private objects
    public GameObject player;
    private float offX = 0.3f;
    private float offY = 2.6f;
    private float offZ = 0;

    // Use this for initialization
    void Start()
    {
        //rt = GetComponent<RectTransform>();
        MessageOff();

        //offX = 0.3f;
        //offY = 2.6f; // todo: get it from the enemy object
        //offZ = 0;

        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (nme != null)
        {
            //Vector2 pos = RectTransformUtility.WorldToScreenPoint(canvas, nme.transform.position);
            //rt.position = pos;

            // var position = nme.GetComponent<PositionReferences>().GetNextPosition();
            var position = nme.transform.position;
            //rt.position = position;
            canvas.transform.position = OffsetPosition(position);
        }

        if (player != null)
        {
            if (GetDistanceFromPlayer(player.transform.position, canvas.transform.position) < speachRadius)
            {
                MessageOn();
            }
            else
            {
                MessageOff();
            }
        }
    }

    private Vector3 OffsetPosition (Vector3 position)
    {
        position.x = position.x + offX;
        position.y = position.y + offY;

        return position;
    }

    private float GetDistanceFromPlayer(Vector3 playerPosition, Vector3 canvasPosition)
    {
        //float ret = 0;
        float ret = (playerPosition - canvasPosition).sqrMagnitude;
        return ret;
    }

    private void MessageOn(bool randomQuote = false)
    {
        // messageCanvas.guiText = SelectRandomSixteenthCenturyPoetryLine(messageCanvas.guiText);
        canvas.enabled = true;
    }

    private void MessageOff()
    {
        canvas.enabled = false;
    }

    private string SelectRandomSixteenthCenturyPoetryLine(string current)
    {
        return current;
    }


    /*

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        messageCanvas.enabled = true;
    //        //MessageOn();
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.name == "Enemy")
    //    {
    //        MessageOff();
    //    }
    //}

    private void MessageOn(bool randomQuote = false)
    {
        // messageCanvas.guiText = SelectRandomSixteenthCenturyPoetryLine(messageCanvas.guiText);
        this.enabled = true;
    }

    private void MessageOff()
    {
        this.enabled = false;
    }


    */
}

