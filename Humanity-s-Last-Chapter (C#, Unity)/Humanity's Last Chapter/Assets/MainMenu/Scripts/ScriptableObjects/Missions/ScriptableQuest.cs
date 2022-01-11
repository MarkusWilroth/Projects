using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class ScriptableQuest : ScriptableObject {

    public string missionName = "Mission name Here";    //Name
    public string description;                          //Description
    public int missionMap;                           //Vilken missionMap som det ska spela på
    public List<ScriptableQuest> nextMissionsComplete;  //Next mission if the mission was sucessfull
    public List<ScriptableQuest> nextMissionsFail;      //Next mission if the mission was failed
    public int goldReward, rsReward, supplies;          //Reward
    public bool completed, isSale;                      //Missionstatus
    public Object[] objectives;                         //Quests that will be used...

    public Object[] startEvents;
    public Object[] endEvents;
}
