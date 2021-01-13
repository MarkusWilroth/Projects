using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacter : MonoBehaviour {
    private CharacterScript characterScript;
    private Stats stats;

    private void Start() {
        characterScript = GetComponent<CharacterScript>();
        stats = GetComponent<Stats>();
    }
}
