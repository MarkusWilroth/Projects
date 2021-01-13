using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BringUpStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject Stats;
    private bool showStats;

    private void Start() {
        showStats = false;
    }

    private void Update() {
        if(showStats) {
            Stats.SetActive(true);
            Stats.GetComponent<CharacterInfo>().UpdateStats();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        showStats = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Stats.SetActive(false);
        showStats = false;
    }

}
