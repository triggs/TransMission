using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpeechCanvasScript : MonoBehaviour {

    // Public objects
    public GameObject nme;
    public Canvas canvas;
    public double detectRadius = 20;
    public Text quote_Text;

    // Private objects
    private GameObject player;
    private float offX = 0.3f;
    private float offY = 1.0f;
    private float offZ = 0;
    private List<string> quotes;
    private System.Random rnd;

    // Use this for initialization
    void Start()
    {
        MessageOff();
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
        player = GetPlayer();
        ReadPoemsPlease();
        rnd = new System.Random();
        //offX = 0.3f;
        //offY = 2.6f; // todo: get it from the enemy object
        //offZ = 0;
    }

    private void Update()
    {
        if (nme != null)
        {
            Vector3 position = nme.transform.position;
            canvas.transform.position = OffsetPosition(position);
        }

        player = GetPlayer();

        if (player != null)
        {
            if (GetDistanceFromPlayer(player.transform.position, canvas.transform.position) < detectRadius)
            {
                MessageOn(true);
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
        if (canvas.enabled) { return; }
        if (randomQuote)
        {
            int lineToRead = rnd.Next(0, quotes.Count-1);
            string quote = quotes[lineToRead];
            quote_Text.text = quote;
        }
        canvas.enabled = true;
    }

    private void MessageOff()
    {
        if (!canvas.enabled)
        {
            return;
        }
        canvas.enabled = false;
    }

    private string SelectRandomSixteenthCenturyPoetryLine(string current)
    {
        return current;
    }

    private void ReadPoemsPlease()
    {
        quotes = new List<string>();
        foreach (string s in Wigglesworth.Split('\n'))
        {
            string line = s.Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            quotes.Add(line);
        }
    }

    private string Wigglesworth =
@"God doth chastise his own
In love, their Souls to save:
And lets them not run wilde▪ with them
That no Correction have.
Now as the Rod restrains*
From posting down to Hell:
So by the same God doth excite
And teach us to do well.

Affliction is Christ's School*
Wherein he teacheth his
To know and do their Duty, and
To mend what is amiss.
For though Afflictions may
Unto the Flesh be painful:
David and other Saints of God
Have found them very gainful.";

//    private string Jabberwocky = 
//@"Twas brillig, and the slithy toves 
//   Did gyre and gimble in the wabe;
//    All mimsy were the borogoves,
//   And the mome raths outgrabe.

//Beware the Jabberwock, my son
//   The jaws that bite, the claws that catch!
//Beware the Jubjub bird, and shun
//   The frumious Bandersnatch!

//He took his vorpal sword in hand; 
//   Long time the manxome foe he sought—
//So rested he by the Tumtum tree, 
//   And stood awhile in thought.

//And, as in uffish thought he stood,
//   The Jabberwock, with eyes of flame,
//Came whiffling through the tulgey wood, 
//   And burbled as it came!

//One, two! One, two! And through and through
//   The vorpal blade went snicker-snack!
//He left it dead, and with its head
//   He went galumphing back.

//And hast thou slain the Jabberwock? 
//   Come to my arms, my beamish boy!
//O frabjous day! Callooh! Callay!” 
//   He chortled in his joy.

//‘Twas brillig, and the slithy toves
//   Did gyre and gimble in the wabe;
//    All mimsy were the borogoves,
//   And the mome raths outgrabe.";

    private GameObject GetPlayer()
    {
        GameObject go = GameObject.FindGameObjectsWithTag("Player")[0];
        if (go == null)
        {
            go = GameObject.Find("Female");
        }
        if (go == null)
        {
            go = GameObject.Find("Male");
        }
        return go;
    }
}

