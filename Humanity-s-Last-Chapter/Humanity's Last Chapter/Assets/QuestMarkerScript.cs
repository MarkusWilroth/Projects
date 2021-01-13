using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarkerScript : MonoBehaviour
{
    List<Transform> position;
    QuestObject missionpos;
    // Start is called before the first frame update
    private void Awake()
    {
        //missionpos = new QuestObject();
        
        missionpos = GameObject.Find("MissionManager2").GetComponent<QuestObject>();
    }
    void Start()
    {
        position = new List<Transform>();
        //GetComponent<MissionMarker>;
        
        //position = missionpos.GetQuestLocation();
        //transform.position = position[0].position;
    }

    // Update is called once per frame
    public void MarkerPos(List<Transform> positions)
    {
        position = positions;
        transform.position = position[0].position;
        
    }

    void Update()
    {
        //position = missionpos.GetQuestLocation();
        //transform.position = position[0].position;
    }
}
