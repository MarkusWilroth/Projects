using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleScript : MonoBehaviour {

    public enum RoleCategory {
        Mobile,     //En roll som fungerar för båda Missions och Hub
        Military,   //Roller för missions
        Support     //Roller för hubben
    }

    private RoleObject role;
    private CharacterScript characterScript;
    private Stats stats;
    private PortraitScript portraitScript;

    private void Start() {
        characterScript = GetComponent<CharacterScript>();
        stats = GetComponent<Stats>();
        portraitScript = GetComponent<PortraitScript>();
        role = characterScript.role;

        if (role != null) {
            portraitScript.Background.GetComponent<Image>().sprite = role.portraitBackground;
        } else {
            portraitScript.NoRole();
        }
    }

    public void ChangeRole(RoleObject newRole) {    //Ändrar rollen, om karaktären har en roll tar den först bort buffarna från den rollen
        if (role != null) {     //Kollar om karaktären har en roll
            ChangeStats(role, -1);  //Har karaktärns stats ändrade med modifieringen (-1) - Tar bort buffarna rollen har
        }
        ChangeStats(newRole, 1);    //Lägger till en nya rollen och ändrar karaktärens stats med modifieringen (1) - Lägger till buggar rollen har
        role = newRole; //Upptaterar så att rollen är den nya rollen
        characterScript.role = role;    //Uppdatera karaktärens roll till den nya rollen
        characterScript.title = role.title;

        if (role != null) {     //kollar om role inte är null (Borde inte kunna vara det här)
            portraitScript.Background.GetComponent<Image>().sprite = role.portraitBackground;   //Ändrar Portrait (Blir ändringen sparad? Borde detta ändras?)
        } else {
            portraitScript.NoRole();    
        }
    }

    public void RemoveRole() {
        ChangeStats(role, -1);    //Lägger till en nya rollen och ändrar karaktärens stats med modifieringen (1) - Lägger till buggar rollen har
        role = null; //Upptaterar så att rollen är den nya rollen
        characterScript.role = null;    //Uppdatera karaktärens roll till den nya rollen
        characterScript.title = "";

        if (role != null) {     //kollar om role inte är null (Borde inte kunna vara det här)
            portraitScript.Background.GetComponent<Image>().sprite = role.portraitBackground;   //Ändrar Portrait (Blir ändringen sparad? Borde detta ändras?)
        } else {
            portraitScript.NoRole();
        }
    }

    public void ChangeStats(RoleObject role, int modifier) { //Ändrar stats för karaktären
        if(stats == null) {
            stats = GetComponent<Stats>();
        }
        stats.maxHp += (role.maxHp * modifier);
        stats.hp += (role.maxHp * modifier);
        stats.def += (role.def * modifier);
        stats.dex += (role.dex * modifier);
    }
}
