using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{

    public string titel, id, ID;
    private string objective, objDesc;
    public bool isCompleted;
    public int questStage;
    private ScriptableQuest quest;
    private GameObject MissionManager;

    private CollectionObjective coObjective;
    private LocationObjective loObjective;
    private InteractionObjective ioObjective;
    private WaveEvent waveEvent;
    private List<ScriptableQuest> nextMissionComplete;

    private Object[] startEvents;
    private Object[] endEvents;

    public QuestObject()
    {

    }



    public void GetData(ScriptableQuest quest, GameObject MissionManager)
    {
        this.quest = quest;
        this.MissionManager = MissionManager;

        startEvents = quest.startEvents;
        endEvents = quest.endEvents;
        titel = quest.missionName;
        id = quest.name;

        isCompleted = false;
        coObjective = MissionManager.GetComponent<CollectionObjective>();
        loObjective = MissionManager.GetComponent<LocationObjective>();
        ioObjective = MissionManager.GetComponent<InteractionObjective>();
        waveEvent = MissionManager.GetComponent<WaveEvent>();

        if (startEvents != null)
        {
            StartEvent(startEvents);
        }
        NextObjective();
    }

    public void UpdateQuest(GameObject MissionManager)
    {
        this.MissionManager = MissionManager;
        coObjective = MissionManager.GetComponent<CollectionObjective>();
        loObjective = MissionManager.GetComponent<LocationObjective>();
        ioObjective = MissionManager.GetComponent<InteractionObjective>();
    }

    //Alla objectives borde skapas samtidigt, men de blir checkade i ordning! Borde isåfall ha coQuest etc som array så att den vet vilken den ska kolla in inte srkiver över varandra...

    public void NextObjective()
    {

        if (questStage >= quest.objectives.Length)
        {
            CompletedQuest();
            objective = "Quest Completed!";
            objDesc = "Quest is finished, return to the hub!";
            MissionManager.GetComponent<MissionManagerScript>().NewObjective(objective, objDesc);
        }
        else
        {
            id = quest.objectives[questStage].name[0].ToString();
            ID = quest.objectives[questStage].name[0].ToString();
            switch (id)
            {
                case "c":
                    //MissionManager.GetComponent<CollectionObjective>().GetData(quest.objectives[objectiveCounter] as ScriptableCollection);
                    coObjective.GetData(quest.objectives[questStage] as ScriptableCollection, waveEvent);
                    objective = coObjective.title;
                    objDesc = coObjective.description;
                    break;

                case "l":
                    loObjective.GetData(quest.objectives[questStage] as LocationObject, waveEvent);
                    objective = loObjective.title;
                    objDesc = loObjective.description;
                    break;

                case "i":
                    ioObjective.GetData(quest.objectives[questStage] as InteractObject, waveEvent);
                    objective = ioObjective.title;
                    objDesc = ioObjective.description;
                    break;
                case "":
                    objective = "Error! no more Objectives";
                    break;
            }
            questStage++;
            MissionManager.GetComponent<MissionManagerScript>().NewObjective(objective, objDesc);
        }
    }

    public List<Transform> GetQuestLocation()
    {
 
        //switch (ID)
        //{
        //    case "c":

        //        CollectionObjective col = quest.objectives[questStage] as CollectionObjective;
        //        return col.SpawnPos();


        //    case "l":
        //        LocationObjective loc = quest.objectives[questStage] as LocationObjective;
        //        spawnArr = { loc.SpawnPos() };
        //        return;


        //    case "i":
        //        InteractionObjective ico = quest.objectives[questStage] as InteractionObjective;
        //        spawnArr = ico.SpawnPos();
        //        return icoco;

        //    case "":
        //        return null;

        //}
        //return null;
        switch (id)
        { //Jättefult, I know... kommer någon på bättre lösning vi kan använda?
            case "c": //CollectionObjective
               

                return(coObjective.SpawnPos());

                
            case "l": //LocationObjective
      

                return(loObjective.SpawnPos());

               
            case "i": //InteractiveObjective
                return(ioObjective.SpawnPos());

               
            case "":
                Debug.Log("Id error! " + id);
                break;
        }
        Debug.Log("Fuck you i'M NULL");
        return null;
    }
    private void CompletedQuest()
    {
        //txtObjective.text = "";
        //gameObject.SetActive(false);
        isCompleted = true;
        if (quest.isSale)
        {
            WorldScript.world.gold += WorldScript.world.goods * 10;
            WorldScript.world.goods = 0;
        }
        else
        {
            WorldScript.world.gold += quest.goldReward;
            WorldScript.world.rs += quest.rsReward;
            WorldScript.world.supplies += quest.supplies;
        }

        WorldScript.world.completedQuests.Add(quest);
        WorldScript.world.RemoveAvalible(quest);

        if (quest.nextMissionsComplete != null)
        {
            foreach (ScriptableQuest quest in quest.nextMissionsComplete)
            {
                WorldScript.world.avalibleQuests.Add(quest);
            }
        }

        if (endEvents != null)
        {
            StartEvent(endEvents);
        }
    }

    private void StartEvent(Object[] eventObj)
    {
        foreach (Object obj in eventObj)
        {
            switch (obj.name[0])
            {
                case 'w':
                    waveEvent.GetEvent(obj as WaveObject);
                    break;
            }
        }
    }

    public bool CheckObjective()
    {
        switch (id)
        { //Jättefult, I know... kommer någon på bättre lösning vi kan använda?
            case "c": //CollectionObjective
                if (coObjective.CheckProgress())
                {
                    return true;
                }
                break;
            case "l": //LocationObjective
                if (loObjective.CheckProgress())
                {
                    return true;
                }
                break;
            case "i": //InteractiveObjective
                if (ioObjective.CheckProgress())
                {
                    return true;
                }
                break;
            case "":
                Debug.Log("Id error! " + id);
                break;
        }
        //Debug.Log("Nothing is done");
        return false;
    }
}


