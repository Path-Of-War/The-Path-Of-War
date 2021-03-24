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

    public List<GameObject> items = new List<GameObject>();


    public virtual void InteractWith()
    {
        textInterface.SetActive(true);
        textRef.text = textsToShow[currentTextIndex];
    }

    public virtual void NextText()
    {
        if(currentTextIndex >= textsToShow.Count -1 && items.Count == 0)
        {
            Debug.Log("interaction ended");
            textInterface.SetActive(false);
            currentTextIndex = 0;
            Player.instance.interactable = null;
        }
        else if(currentTextIndex == textsToShow.Count - 1)
        {
            string tempText = "You received the following Items !!! \n";
            foreach (GameObject item in items)
            {
                tempText += " " + item.GetComponent<ALoot>().lootName;
                Player.instance.EarnItem(item);
            }
            items = new List<GameObject>();
            textRef.text = tempText;
        }
        else
        {
            Debug.Log("next text");
            currentTextIndex++;
            textRef.text = textsToShow[currentTextIndex];
        }
    }
}
