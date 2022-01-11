using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static QuirkScript;

public class UIHospitalBoi : MonoBehaviour {
    
    public Text txtHead, txtTorso, txtLeftArm, txtRightArm, txtLeftLeg, txtRightLeg, txtHeal; //För att ändra texten på boxarna
    private Stats stats;
    public Color[] woundColour;
    public Color unharmedColour, healPossible, healUnpossible;
    private bool isHealable;
    public GameObject Hospital;
    private HospitalScript hospitalScript;
    private CharacterScript characterScript;

    public void BtnHeal() {
        if (isHealable && !characterScript.inHospital) {
            if (hospitalScript.healingSlots > 0) {
                hospitalScript.healingSlots--;
                characterScript.isEnlisted = false;
                characterScript.inHospital = true;
                txtHeal.text = "Remove";
                txtHeal.transform.parent.GetComponent<Image>().color = healUnpossible;
                hospitalScript.HealCharacter(gameObject);
            }
        } else if (characterScript.inHospital) {
            txtHeal.text = "Heal";
            txtHeal.transform.parent.GetComponent<Image>().color = healPossible;

            hospitalScript.healingSlots++;
            characterScript.inHospital = false;
            characterScript.isEnlisted = false;
            hospitalScript.RemoveCharacter(gameObject);
        }
    }

    private void Start() {
        hospitalScript = Hospital.GetComponent<HospitalScript>();
        characterScript = GetComponent<CharacterScript>();

        isHealable = false;
        stats = GetComponent<Stats>();  //Hämtar stats
        SetText(); //sätter texten till unharmed (Kommer att ändras om de inte är det)
        if (stats.hp < stats.maxHp) {
            isHealable = true;
        }

        foreach (QuirkObject quirk in stats.quirkList) { //Kollar alla quirks som karaktären har
            if (quirk.quirkType == QuirkType.woundQuirk) {  //Om quirken är av typen woundQuirk kommer den att kollas vilken del den rör

                if (quirk.quirkLevel <= WorldScript.world.hospitalLevel * 2 && !(quirk.quirkLevel >= 5)) {
                    isHealable = true;
                }
                switch (quirk.bodyPart) { //Vilken del den rör är den del som kommer att bli ändrad
                    case BodyPart.head:
                        txtHead.text = quirk.quirkName; //Sätter texten på rutan
                        txtHead.transform.parent.GetComponent<Image>().color = woundColour[quirk.quirkLevel]; //Ändrar färgen på rutan
                        break;
                    case BodyPart.torso:
                        txtTorso.text = quirk.quirkName;
                        txtTorso.transform.parent.GetComponent<Image>().color = woundColour[quirk.quirkLevel];
                        break;
                    case BodyPart.leftArm:
                        txtLeftArm.text = quirk.quirkName;
                        txtLeftArm.transform.parent.GetComponent<Image>().color = woundColour[quirk.quirkLevel];
                        break;
                    case BodyPart.rightArm:
                        txtRightArm.text = quirk.quirkName;
                        txtRightArm.transform.parent.GetComponent<Image>().color = woundColour[quirk.quirkLevel];
                        break;
                    case BodyPart.leftLeg:
                        txtLeftLeg.text = quirk.quirkName;
                        txtLeftLeg.transform.parent.GetComponent<Image>().color = woundColour[quirk.quirkLevel];
                        break;
                    case BodyPart.rightLeg:
                        txtRightLeg.text = quirk.quirkName;
                        txtRightLeg.transform.parent.GetComponent<Image>().color = woundColour[quirk.quirkLevel];
                        break;
                }
            }
        }
        if (isHealable) {
            txtHeal.text = "Heal";
            txtHeal.transform.parent.GetComponent<Image>().color = healPossible;
        } else {
            txtHeal.text = "Can't Heal";
            txtHeal.transform.parent.GetComponent<Image>().color = healUnpossible;
        }

        if (GetComponent<CharacterScript>().inHospital) {
            txtHeal.text = "Remove";
            txtHeal.transform.parent.GetComponent<Image>().color = healUnpossible;
        }
    }

    private void SetText() { //Sätter texten
        txtHead.text = "Head - Unharmed";
        txtHead.transform.parent.GetComponent<Image>().color = unharmedColour;

        txtTorso.text = "Torso - Unharmed";
        txtTorso.transform.parent.GetComponent<Image>().color = unharmedColour;

        txtLeftArm.text = "Left Arm - Unharmed";
        txtLeftArm.transform.parent.GetComponent<Image>().color = unharmedColour;

        txtRightArm.text = "Right Arm - Unharmed";
        txtRightArm.transform.parent.GetComponent<Image>().color = unharmedColour;

        txtLeftLeg.text = "Left Leg - Unharmed";
        txtLeftLeg.transform.parent.GetComponent<Image>().color = unharmedColour;

        txtRightLeg.text = "Right Leg - Unharmed";
        txtRightLeg.transform.parent.GetComponent<Image>().color = unharmedColour;
    }

}
