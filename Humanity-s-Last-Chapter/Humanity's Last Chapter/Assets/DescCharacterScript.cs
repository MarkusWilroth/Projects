using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescCharacterScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    private GameObject DescHolder, ScriptHolder;
    private CharacterScript characterScript;
    private Stats stats;
    private QuestDescScript questDescScript;

    private void Start() {
        DescHolder = GameObject.FindGameObjectWithTag("QuestDesc");
        questDescScript = DescHolder.GetComponent<QuestDescScript>();

        ScriptHolder = transform.parent.gameObject;
        //characterScript = GetComponentInParent<CharacterScript>();
        //stats = GetComponentInParent<Stats>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        questDescScript.CharacterInfo(ScriptHolder);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        questDescScript.ClearRole();
        questDescScript.QuestReset();
    }

}
