using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Quest
{
    public enum QuestType
    {
        kill,
        gather,
        none
    }

    public bool isActive = false;
    public bool isFinished = false;

    public QuestType type;

    public string questName;
    public string questDescription;

    public int xpReward;

    public List<GameObject> lootReward = new List<GameObject>();

    public GameObject targetToKillPrefab;
    public GameObject targetToKillPos;
    public GameObject targetToKillInstance;

    public GameObject targetToGather;


    public void StartQuest()
    {
        isActive = true;
        isFinished = false;
        if(type == QuestType.gather)
        {
            Debug.Log(targetToGather);
        }

        if(type == QuestType.kill)
        {
            targetToKillInstance = Player.Instantiate(targetToKillPrefab, targetToKillPos.transform);
        }
        if(type == QuestType.gather && targetToGather == null)
        {
            QuestFinish();
        }
    }

    public void QuestFinish()
    {
        if(isActive && type == QuestType.kill && targetToKillInstance == null)
        {
            EndQuestPlayerImpact();
        }
        else if(isActive && type == QuestType.gather && targetToGather == null)
        {
            EndQuestPlayerImpact();
        }

    }

    void EndQuestPlayerImpact()
    {
        Player.instance.pEffect.EarnExpereience(xpReward);
        Player.instance.EarnItem(lootReward);
        Player.instance.popupText.SetActive(true);
        Player.instance.popupText.GetComponent<TMP_Text>().text = "You finished the quest: \n" + questName;
        Player.instance.popupDeactivate = Player.instance.popupTime + Time.time;
        isActive = false;
        isFinished = true;
    }
}