using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {
    public Slider slider;
    private int minValue, maxValue;
    private Stats statsScript;
    public string type;
    
    void Start() {
        //statsScript = GetComponent<stats>();
        //if (type == "sliderHealth") { //snyggare sätt plz
        //    minValue = statsScript.hp;
        //    maxValue = statsScript.maxHp;
        //}
        //if (type == "sliderExp") {
        //    minValue = statsScript.exp;
        //    maxValue = statsScript.nextLevel;
        //}
        //slider.value = minValue / maxValue;
        
    }
    public void GetSlider(int value) {
        slider.value = value;
    }
}
