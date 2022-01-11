using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuirkScript : MonoBehaviour {

    public static QuirkScript quirkScript;

    public List<QuirkObject> birthQuirkList, healthQuirkList, woundQuirkList;
    public enum QuirkType {
        birthQuirk,
        healthQuirk,
        woundQuirk
    }

    public enum BodyPart { //Varje kropsdel förutom spirit har 7 steg där den 7:e är att kropsdelen är av, hospital kan aldrig hela det
        spirit,
        head,
        torso,
        leftArm,
        rightArm,
        leftLeg,
        rightLeg
    }

    private bool possibleQuirk;

    // Start is called before the first frame update
    public void PrepareQuirks() {
        birthQuirkList = new List<QuirkObject>();
        healthQuirkList = new List<QuirkObject>();
        woundQuirkList = new List<QuirkObject>();

        foreach (QuirkObject quirk in Assets.assets.quirkArray) {
            switch (quirk.quirkType) {
                case QuirkType.birthQuirk:
                    birthQuirkList.Add(quirk);
                    break;
                case QuirkType.healthQuirk:
                    healthQuirkList.Add(quirk);
                    break;
                case QuirkType.woundQuirk:
                    woundQuirkList.Add(quirk);
                    break;
                default:
                    Debug.Log("Quirk: " + quirk.name + " dosen't have a quirkType!");
                    break;
            }
        }
    }

    public void GetBirthQuirk(Stats stats) {    //Ska kallas på när en karaktär skapas
        int randAmmount = Random.Range(1, 3);   //Hur många quirks karaktären kommer starta med
        int quirkCounter = 0;

        while (quirkCounter <= randAmmount) {   //När karaktären har x antal birthQuirks kommer den sluta ta fram quirks
            possibleQuirk = true;               //Sätter så att den är redo att lägga till quirken
            int randQuirk = Random.Range(0, birthQuirkList.Count);  //tar fram en random birthQuirk

            foreach (QuirkObject quirk in stats.quirkList) {  //Kollar alla quirks som karaktären har
                if (quirk.name != birthQuirkList[randQuirk].name) {     //Om karaktären inte har samma quirk kommer den kolla sina quirks och vilka som går ihop

                    foreach (QuirkObject quirkPair in quirk.quirkPair) {        //Kollar alla quirks som är kopplade med quirken den har
                        
                        if (birthQuirkList[randQuirk] != null && //för att ta bort ett error
                            quirkPair !=  null &&
                            quirkPair.name == birthQuirkList[randQuirk].name) { //Om quirken som testas krockar med en existerande quirk måste processen börja om med en ny random quirk
                            possibleQuirk = false;  //Sätter possibleQuirk till false eftersom denna quirk inte kan användas
                            break;  
                        }
                    }
                } else {
                    possibleQuirk = false;  //Om den redan har den quirk som testas är det onödigt att kolla de andra quirksen
                    break;
                }
                
                if (!possibleQuirk) {       //Om en quirkPair krockar är det onödigt att kolla resten av quirken
                    break;
                }
            }
            if (possibleQuirk) {    //Om den gick igenom hela processen och possibleQuirk fortfarande är true kommer den lägga till quirken
                stats.AddQuirk(birthQuirkList[randQuirk]);    //Lägger till quirken
                quirkCounter++;     //Ökar quirkcountern så att den kommer it hur while loopen när den ska
            }
        }
    }

    public void GetWoundQuirk(CharacterScript characterScript, Stats stats, int damageTaken) {
        int randBodyPart = Random.Range(0, 6); //finns 6 st kropsdelar att träffa
        int woundLevel = 0;
        bool newWound = true;
        

        BodyPart partToHit = BodyPart.spirit;

        switch (randBodyPart) {
            case 0:
                partToHit = BodyPart.head;
                break;
            case 1:
                partToHit = BodyPart.torso;
                break;
            case 2:
                partToHit = BodyPart.leftArm;
                break;
            case 3:
                partToHit = BodyPart.rightArm;
                break;
            case 4:
                partToHit = BodyPart.leftLeg;
                break;
            case 5:
                partToHit = BodyPart.rightLeg;
                break;
            default:
                Debug.Log("Lucky no bodypart was hit! randomBodyPart: " + randBodyPart);
                break;
        }
        if (partToHit != BodyPart.spirit) {
            if (damageTaken >= 15 || stats.hp <= 10) { //Stor skada (skada level 0 - 5)
                foreach (QuirkObject quirk in stats.quirkList) {  //Kollar alla quirks hos karaktären ifall det finns en skada på denna kroppsdel
                    if (quirk.bodyPart == partToHit) {  //Om det hittas en skada på kroppsdelen kommer det kollas hur  stor skadan är
                        if (quirk.quirkLevel < 5) { //Om skadan är så liten att denna svaga attack kan skada den mer kommer den bli mer skadad
                            woundLevel = quirk.quirkLevel + 2; //WoundLevel blir den gamla skada +1;

                            if (woundLevel > 5) { //Då den ökas med 2 kan det sluta med att karaktären kan bli mer skadad än vad han borde
                                woundLevel = 5;
                            }
                            newWound = true;
                            if (characterScript.isEssential) { //Odödliga karaktärer ska inte kunna dö av sina skador
                                if (quirk.quirkLevel >= 4) { //Om den odödliga karaktären redan är max skadad ska den inte bli skadad igen
                                    newWound = false;
                                } else if (woundLevel > 4) { //Om den inte är max skadad men den ska bli mer än max skadad blir den max skadad
                                    woundLevel = 4;
                                }
                            }
                            if (newWound) {
                                stats.RemoveQuirk(quirk);
                            }


                        } else { //Om skadan är för stor kommer inte denna kroppsdel bli mer skadad (Man kan inte slå av en arm genom att slå på den)
                            newWound = false;
                        }
                        break;
                    }
                }

            }
            else if (damageTaken >= 10 && damageTaken < 15 || stats.hp <= 25 && damageTaken < 15) { //Medelskada (skada level 0 - 3)
                foreach (QuirkObject quirk in stats.quirkList) {  //Kollar alla quirks hos karaktären ifall det finns en skada på denna kroppsdel
                    if (quirk.bodyPart == partToHit) {  //Om det hittas en skada på kroppsdelen kommer det kollas hur  stor skadan är
                        if (quirk.quirkLevel < 3) { //Om skadan är så liten att denna svaga attack kan skada den mer kommer den bli mer skadad
                            woundLevel = quirk.quirkLevel + 1; //WoundLevel blir den gamla skada +1;
                            
                            newWound = true;
                            stats.RemoveQuirk(quirk);
                        } else { //Om skadan är för stor kommer inte denna kroppsdel bli mer skadad (Man kan inte slå av en arm genom att slå på den)
                            newWound = false;
                        }
                        break;
                    }
                }

            } else if (damageTaken >= 2 && damageTaken < 6 || stats.hp <= 40 && damageTaken < 6) { //Lite skada tagen kan bara få små skador (skada level 0 - 1)

                foreach (QuirkObject quirk in stats.quirkList) {  //Kollar alla quirks hos karaktären ifall det finns en skada på denna kroppsdel
                    if (quirk.bodyPart == partToHit) {  //Om det hittas en skada på kroppsdelen kommer det kollas hur  stor skadan är
                        if (quirk.quirkLevel < 1) { //Om skadan är så liten att denna svaga attack kan skada den mer kommer den bli mer skadad
                            woundLevel = quirk.quirkLevel + 1; //WoundLevel blir den gamla skada +1;
                            newWound = true;
                            stats.RemoveQuirk(quirk);
                        } else { //Om skadan är för stor kommer inte denna kroppsdel bli mer skadad (Man kan inte slå av en arm genom att slå på den)
                            newWound = false;
                        }
                        break;
                    }
                }


            }

            if (newWound) {
                foreach (QuirkObject quirk in woundQuirkList) {
                    if (quirk.bodyPart == partToHit && quirk.quirkLevel == woundLevel) {
                        stats.AddQuirk(quirk);
                        if (woundLevel >= 5 && (partToHit == BodyPart.head || partToHit == BodyPart.torso)) {
                            stats.hp = -stats.hp; //Karaktären död!
                        }
                        break;
                    }
                }
            }
        }
        

    }

    // Update is called once per frame
    void Update() {

    }





    int Quirkselection(int selectedNumber) {
        if (selectedNumber == 0) {

        } else if (selectedNumber == 1) {

        } else if (selectedNumber == 2) {

        } else if (selectedNumber == 3) {

        } else if (selectedNumber == 4) {

        }
        return 0;
    }


}
