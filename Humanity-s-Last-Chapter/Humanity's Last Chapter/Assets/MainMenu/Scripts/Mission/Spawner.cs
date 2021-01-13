using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour {
    public GameObject[] spawnableObjects;
    private GameObject parent, createdObject;
    public Tilemap tilemap;
    private Vector3 place;
    private Vector3Int localPlace;
    private int[] missionOrder;
    private int characterCounter;
    private string wpId;

    // Start is called before the first frame update
    void Start() {
        missionOrder = SaveSystem.LoadPartyOrder();
        characterCounter = 0;

        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++) {
            for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++) {
                localPlace = (new Vector3Int(i, j, (int)tilemap.transform.position.y));
                place = tilemap.CellToWorld(localPlace);
                if(tilemap.HasTile(localPlace)) {
                    for (int k = 0; k < spawnableObjects.Length; k++) {
                        if (spawnableObjects[k].name == tilemap.GetSprite(localPlace).name) {
                            if(spawnableObjects[k].name == "MarkusBoi") {
                                
                                CharacterSpawn(spawnableObjects[k], place);
                            }
                            else {
                                CreateTarget(place, spawnableObjects[k], gameObject.transform); //Inte säker hur vi ska göra parents...
                            }
                        }
                    }
                }
            }
        }
        
        tilemap.ClearAllTiles();
    }
    public void CharacterSpawn(GameObject character, Vector3 place) {
        //if (missionOrder[characterCounter] >= 0) {
        //    parent = GameObject.FindGameObjectWithTag("CharacterManager");
        //    CreateTarget(place, character, parent.transform);
        //    createdObject.transform.localScale = new Vector3(1, 1, 1);
        //    createdObject.GetComponent<CharacterScript>().LoadPlayer(missionOrder[characterCounter]);
        //    //createdObject.GetComponent<PersonalMovement>().relativePos = new Vector3(characterCounter * 0.5f, characterCounter * 0.5f);
        //    //createdObject.GetComponent<PersonalMovement>().AddRelativeWaypoint(createdObject.transform.parent.position);
        //    characterCounter++;
        //}

    }

    private void CreateTarget(Vector3 place, GameObject spawnTarget, Transform parent) {
        createdObject = Instantiate(spawnTarget, place, Quaternion.identity);
        createdObject.transform.SetParent(parent, false);
    }
}
