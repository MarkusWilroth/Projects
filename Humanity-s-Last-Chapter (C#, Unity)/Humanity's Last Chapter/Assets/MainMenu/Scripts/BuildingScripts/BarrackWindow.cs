using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackWindow : MonoBehaviour {

    public GameObject BarrackBoi;
    private GameObject holder;
    private Vector3 roasterPos;

    private int rand;

    private void Start() {
        roasterPos = new Vector3(-600, 180, 1); //sjukt fult måste göras snyggare!
        for (int i = 0; i < WorldScript.world.charBarrackPepList.Count; i++) {
            if (i == 3) {
                roasterPos.y = 180;
                roasterPos.x += 700;
            }
            holder = Instantiate(BarrackBoi);                                                   //Skapar rutan
            holder.GetComponent<CharacterScript>().LoadPlayer(WorldScript.world.charBarrackPepList[i]);                       //Sätter in characterScript
            holder.GetComponent<Stats>().LoadPlayer(WorldScript.world.staBarrackPepList[i]);    //sätter in stats
            holder.transform.position = roasterPos;
            //holder.GetComponent<HireCharacter>().WorldObject = holder;
            holder.transform.SetParent(gameObject.transform, false);                            //Sätter så att den har BarrackWindow som parent
            
            roasterPos.y -= 200;
        }
    }

    public void BtnExit() {
        Destroy(gameObject);
    }
}
