using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData {
    public string id;
    public bool isComplet, isFailed;

    public QuestData(string id, bool isComplet, bool isFailed) {
        this.id = id;
        this.isComplet = isComplet;
        this.isFailed = isFailed;
    }
}
