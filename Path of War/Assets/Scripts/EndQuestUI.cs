using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndQuestUI : MonoBehaviour
{
    public TMP_Text titleUI;
    public TMP_Text descriptionUI;
    public GameObject itemContainer;
    public TMP_Text xpText;
    public GameObject childContainer;

    List<GameObject> itemsGenerated = new List<GameObject>();
    Quest currentQuest;

    public void QuestPopup(Quest quest)
    {
        if (!childContainer.activeInHierarchy) { 
            currentQuest = quest;
            titleUI.text = quest.questName;
            descriptionUI.text = quest.questDescription;
            foreach(GameObject item in quest.lootReward)
            {
                GameObject temp = Instantiate(item, itemContainer.transform);
                itemsGenerated.Add(temp);
            }
            xpText.text = "experience point: " + quest.xpReward;
            childContainer.SetActive(true);
        }
    }

    public void ConfirmButton()
    {
        currentQuest.EndQuestPlayerImpact();
        childContainer.SetActive(false);
        foreach (GameObject item in itemsGenerated)
        {
            Destroy(item);
        }
        itemsGenerated.Clear();
    }
}
