using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIActiveMission : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text txtMissionName, txtObjectiveDesc;
    public GameObject objectiveDescBox;

    public void OnPointerEnter(PointerEventData eventData) {
        objectiveDescBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        objectiveDescBox.SetActive(false);
    }
}
