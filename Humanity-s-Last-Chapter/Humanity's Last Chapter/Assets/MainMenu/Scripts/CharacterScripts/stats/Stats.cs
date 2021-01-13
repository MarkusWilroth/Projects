using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stats : MonoBehaviour {
    public int hp, maxHp, str, def, Int, dex, cha, ldr, nrg, snt, level, exp, nextLevel;
    private int cost;
    private int randomQuirk;
    private string quirkName;
    public GameObject characterUI;
    public GameObject prefabCharacterUI;
    CharacterStatWriter writer;
    public GameObject ItemPrefab;
    private GameObject itemGrid;
    public List<GameObject> items = new List<GameObject>();
    public List<QuirkObject> quirkList;
    public bool shit;
    public ParticleSystem bloodEffect;
    public List<string> quirkIDList;
    public GameObject PCCorpse;
    public bool isTesting = false;

    // Start is called before the first frame update
    void Start() {
        if (isTesting) {
            maxHp = 60000;
            hp = maxHp;
        }

        if (maxHp == 0)
        {
            maxHp = 60;
            hp = maxHp;
        }

        if (shit == false) {
            quirkIDList = new List<string>();
            shit = true;
            str = 10;
            def = 10;
            Int = 10;
            dex = 10;
            cha = 10;
            ldr = 10;
            snt = 100;

            nextLevel = 10 + (5 * level);
            //randomQuirk = Random.Range(0, 9);
            //randomQuirk *= 2;
            //AddQuirk(Assets.assets.quirkArray[randomQuirk]);
            QuirkScript.quirkScript.GetBirthQuirk(this);

        }
        #region Gammal kod
        //Debug.Log("Str is " + str);
        //characterUI = Instantiate(prefabCharacterUI, new Vector3(0, 0, 0), Quaternion.identity);
        //characterUI.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //characterUI.GetComponent<Canvas>().sortingOrder = 1;
        //characterUI.GetComponentInChildren<Button>().onClick.AddListener(delegate { characterUI.SetActive(false); });

        ////Set the script to the instance of the CharacterCanvas object, and then run the method in it.
        //writer = prefabCharacterUI.GetComponent<CharacterStatWriter>();
        //foreach (string quirkId in quirkIDList) {
        //    foreach (QuirkObject quirk in Assets.assets.quirkArray) {
        //        if (quirk.name == quirkId) {
        //            writer.GetStats(hp, str, def, Int, dex, cha, quirk.quirkName);
        //            break;
        //        }
        //    }


        //}


        ////Create items
        //foreach (Transform t in characterUI.transform) {
        //    if (t.tag == "ItemGrid") {
        //        itemGrid = t.gameObject;
        //    }
        //}
        //for (int x = 0; x < 6; x++)
        //    items.Add(Instantiate(ItemPrefab, itemGrid.transform));
        ////GetComponent<CharacterScript>().LoadPlayer(GetComponent<CharacterScript>().id);
        //for (int x = 0; x < GetComponent<CharacterScript>().itemID.Length; x++) {
        //    string itemId = GetComponent<CharacterScript>().itemID[x];
        //    if (itemId != null)
        //        items[x].GetComponent<ItemScript>().CreateItem(itemId);
        //}

        ////This is dumb, but CharacterCanvas is a canvas, so it's dumb.
        //for (int x = 0; x < characterUI.transform.childCount; x++)
        //    characterUI.transform.GetChild(x).transform.position += new Vector3(-4f, 1.9f, 0);

        ////Configure items
        //int i = 0;
        //int j = 0;
        //foreach (GameObject item in items) {
        //    item.GetComponent<ItemScript>().SetSlot(item.transform.position + new Vector3(0 + i * 0.6f, 0 - j * 0.6f, 0)); // + new Vector3(-2f + i * 0.5f, 2.1f - j * 0.5f, 0));//new Vector3(-2.2f + i*0.5f, 3.9f, 0));
        //    i++;
        //    if (i > 2) {
        //        i = 0;
        //        j++;
        //    }
        //}
        #endregion
    }

    public int GetCost() {
        cost = (maxHp) +  (def);
        return cost;
    }

    public void BringUpStats() {
        //GetComponentInParent<AddToPlayerRoster>().controller.GetComponent<HubCharController>().CloseAllWindows();
        //characterUI.SetActive(true);
    }

    public void NewCharacter(Stats stats) {
        if (stats == null) {
            Debug.Log("Error");
        }
        maxHp = stats.maxHp;
        str = stats.str;
        def = stats.def;
        Int = stats.Int;
        dex = stats.dex;
        cha = stats.cha;
        ldr = stats.ldr;
        nrg = stats.nrg;
        snt = stats.snt;
        hp = maxHp;
    }

    //public void GetStats(int maxHp, int hp) { //Orkar inte skriva över alla stats... Senare kommer character scripts och stats vara samma script så detta steg kommer inte behövas!
    //    this.maxHp = maxHp;
    //    this.hp = hp;
    //}

    public void AddQuirk(QuirkObject quirk) {
        if (quirkList == null) {
            InitiateList();
        }
        quirkIDList.Add(quirk.name);
        quirkList.Add(quirk);

        quirkName = quirk.quirkName;
        maxHp += quirk.hp;
        def += quirk.def;
        dex += quirk.dex;

        if (hp > maxHp) {
            hp = maxHp;
        }
        

    }

    public void RemoveQuirk(QuirkObject quirk) {

        foreach (QuirkObject quirkTest in quirkList) {
            if (quirkTest == quirk) {
                quirkList.Remove(quirkTest);
                break;
            }
        }

        foreach (string quirkId in quirkIDList) {
            if (quirk.name == quirkId) {
                quirkIDList.Remove(quirkId);
                break;
            }
        }
        maxHp -= quirk.hp;
        def -= quirk.def;
        dex -= quirk.dex;
        if (hp > maxHp) {
            hp = maxHp;
        }
        
    }

    void InitiateList() {

        quirkList = new List<QuirkObject>();
    }

    public void TakeDamage(int damage) {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        int procentage = 100;
        int randomChance = 0;

        damage -= def;
        if (damage < 1) {
            damage = 1;
        }
        procentage -= dex;
        randomChance = Random.Range(0, 101);
        if (randomChance > procentage) {
            damage = 0;
        }
        hp -= damage;
        if (hp <= 0) {//ded
            if (GetComponent<CharacterScript>().role != null && GetComponent<CharacterScript>().role.roleName.Equals("Commander"))//om commandern dog
                GetComponent<PersonalMovement>().manager.GetComponent<CharacterMovement>().hasCommander = false;
            GameObject corpse = Instantiate(PCCorpse);
            corpse.transform.position = transform.position;
            corpse.transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, 180);
            GetComponent<CharacterScript>().OnDeath();
            gameObject.SetActive(false);
        } else {
            if (damage >= 2) { //Skada som är mindre än 2 är bara ondödig att skicka in!
                QuirkScript.quirkScript.GetWoundQuirk(GetComponent<CharacterScript>(), this, damage);
            }
        }
    }

    public void HealCharacter(int healAmount) { //Det sparas inte...
        hp += healAmount;

        if (hp >= maxHp) {
            hp = maxHp;
        }
    }

    public void LoadPlayer(Stats data) { //Vill ersätta detta med något som typ "this.stats = data" men vet inte hur...
        quirkIDList = data.quirkIDList;
        shit = data.shit;
        hp = data.hp;
        maxHp = data.maxHp;
        str = data.str;
        def = data.def;
        Int = data.Int;
        dex = data.dex;
        cha = data.cha;
        ldr = data.ldr;
        nrg = data.nrg;
        snt = data.snt;
        level = data.level;
        exp = data.exp;
        nextLevel = data.nextLevel;

        quirkList = new List<QuirkObject>();
        foreach (string quirkId in quirkIDList) {
            foreach (QuirkObject quirk in Assets.assets.quirkArray) {
                if (quirkId == quirk.name) {
                    quirkList.Add(quirk);
                    break;
                }
            }
        }
        
    }
}
