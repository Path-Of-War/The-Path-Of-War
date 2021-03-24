using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AInteractable : MonoBehaviour
{
    public GameObject textInterface;
    public TMP_Text textRef;
    int currentTextIndex = 0;
    public GameObject pos;

    public List<string> textsToShow = new List<string>();

    public List<ALoot> items = new List<ALoot>();


    public virtual void InteractWith()
    {
        textInterface.SetActive(true);
        textRef.text = textsToShow[currentTextIndex];
    }

    public virtual void NextText()
    {
        if(currentTextIndex == textsToShow.Count -1 )
        {
            textInterface.SetActive(false);
            currentTextIndex = 0;
            Player.instance.interactable = null;
        }
        else
        {
            currentTextIndex++;
            textRef.text = textsToShow[currentTextIndex];
        }
    }
}
