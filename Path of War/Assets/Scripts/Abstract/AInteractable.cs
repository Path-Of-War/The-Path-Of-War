using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AInteractable : MonoBehaviour
{


    public List<Quest> quests = new List<Quest>();
    public int questIndex = 0;

    public GameObject textInterface;
    public TMP_Text textRef;
    int currentTextIndex = 0;
    public GameObject pos;

    public List<string> textsToShow = new List<string>();

    public List<GameObject> items = new List<GameObject>();


    private void Update()
    {
        if(quests.Count > 0) { 
            if(quests[questIndex].isActive && quests[questIndex].type == Quest.QuestType.kill && !quests[questIndex].targetToKillInstance)
            {
                quests[questIndex].QuestFinish();

            }
            else if(quests[questIndex].isActive && quests[questIndex].type == Quest.QuestType.gather && !quests[questIndex].targetToGather)
            {
                quests[questIndex].QuestFinish();
            }
        }
    }

    public virtual void EndQuest()
    {
        if (questIndex < quests.Count - 1)
        {
            questIndex++;
            quests[questIndex].StartQuest(textRef, this);
        }
    }

    public virtual void InteractWith()
    {
        textInterface.SetActive(true);
        if (textsToShow.Count == 0 && items.Count > 0)
        {
            NextText();
        }
        else
        {
            textRef.text = textsToShow[currentTextIndex];
        }
    }

    public virtual void NextText()
    {
        if(quests.Count > 0) { 
            if(currentTextIndex >= textsToShow.Count -1 && items.Count == 0 && !quests[questIndex].isActive && quests[questIndex].type != Quest.QuestType.none && !quests[questIndex].isFinished)
            {
                quests[questIndex].StartQuest(textRef, this);
            }
            else if (currentTextIndex >= textsToShow.Count - 1 && items.Count == 0 )
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
        else if ((currentTextIndex >= textsToShow.Count - 1 ||textsToShow.Count ==0) && items.Count == 0 )
        {
            Debug.Log("interaction ended");
            textInterface.SetActive(false);
            currentTextIndex = 0;
            Player.instance.interactable = null;
        }
        else if (currentTextIndex == textsToShow.Count - 1 || textsToShow.Count ==0)
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
