using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem {
    public class Quest : MonoBehaviour {

        public Quest() {

        }

        //private List<IQuestObjectives> objectives;

        //private bool IsComplete() {
        //    for (int i = 0; i < objectives.Count; i++) {
        //        if(objectives[i].IsComplete != false && objectives[i].IsBonus == false) {
        //            return false;
        //        }
        //    }
        //    return true; //Get reward!!
        //}
    }
}
/* Objectives:
 * Collection Objective
 *  - 10 feathers
 *  - kill 4 enemies
 * 
 * Location Objective
 *  - go from point A to B
 *  - Timed, you have 10 mins to get to point B from A
 *  
 * Interact Objective
 *  - talk to this boi
 *  - interact with this item
 */
