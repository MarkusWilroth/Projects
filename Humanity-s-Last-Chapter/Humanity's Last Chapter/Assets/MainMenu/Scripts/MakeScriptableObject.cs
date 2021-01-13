

//public class MakeScriptableObject {

//    #region ScriptableObjects
//    [MenuItem("Assets/Create/Mission/Quest Giver")]
//    public static void CreateQuestGiver() {
//        GiverObject asset = ScriptableObject.CreateInstance<GiverObject>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/CharacterFolder/QuestGivers").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/CharacterFolder/QuestGivers/qg" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Mission/Event Wave")]
//    public static void CreateWaveEvent() {
//        WaveObject asset = ScriptableObject.CreateInstance<WaveObject>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/MissionFolder/EventWave").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/MissionFolder/EventWave/we" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Mission/Location Objective")]
//    public static void CreateLocationObjective() {
//        LocationObject asset = ScriptableObject.CreateInstance<LocationObject>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/MissionFolder/LocationObjectives/").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/MissionFolder/LocationObjectives/lo" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Mission/Collection Objective")]
//    public static void CreateCollectionObjective() {
//        ScriptableCollection asset = ScriptableObject.CreateInstance<ScriptableCollection>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/MissionFolder/CollectionObjectives/").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/MissionFolder/CollectionObjectives/co" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Mission/Interaction Objective")]
//    public static void CreateInteractionObjective() {
//        InteractObject asset = ScriptableObject.CreateInstance<InteractObject>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/MissionFolder/InteractObjectives/").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/MissionFolder/InteractObjectives/io" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Mission/Quest Objective")]
//    public static void CreateQuestObject() {
//        ScriptableQuest asset = ScriptableObject.CreateInstance<ScriptableQuest>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/MissionFolder/Quests/").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/MissionFolder/Quests/mi" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Item/Weapon Object")]
//    public static void CreateWeaponObject() {
//        WeaponObject asset = ScriptableObject.CreateInstance<WeaponObject>();
//        int weaponCounter = Directory.GetFiles("Assets/Resources/WeaponFolder/").Length;

//        if (!(weaponCounter == 0)) {
//            weaponCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/WeaponFolder/wp" + weaponCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Item/Combat Item")]
//    public static void CreateCombatItem() {
//        CombatItemObject asset = ScriptableObject.CreateInstance<CombatItemObject>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/ItemsFolder/CombatItems/").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemsFolder/CombatItems/ci" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Item/Healing Item")]
//    public static void CreateHealingItem() {
//        HealingItemObject asset = ScriptableObject.CreateInstance<HealingItemObject>();
//        int missionCounter = 0;
//        missionCounter = Directory.GetFiles("Assets/Resources/ItemsFolder/HealingItems/").Length;

//        if (!(missionCounter == 0)) {
//            missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemsFolder/HealingItems/hi" + missionCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Item/Cloth Item")]
//    public static void CreateClothItem() {
//        ClothItemObject asset = ScriptableObject.CreateInstance<ClothItemObject>();
//        int itemCounter = 0;
//        itemCounter = Directory.GetFiles("Assets/Resources/ItemsFolder/Cloths/").Length;

//        if (!(itemCounter == 0)) {
//            itemCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemsFolder/Cloths/cloth" + itemCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Quirk Object")]
//    public static void CreateQuirkObject() {
//        QuirkObject asset = ScriptableObject.CreateInstance<QuirkObject>();
//        int quirkCounter = 0;
//        quirkCounter = Directory.GetFiles("Assets/Resources/QuirkFolder/").Length;

//        if (!(quirkCounter == 0)) {
//            quirkCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/QuirkFolder/qu" + quirkCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Character/Character")]
//    public static void CreateCharacterTemp() {
//        ObjectCharacter asset = ScriptableObject.CreateInstance<ObjectCharacter>();
//        int quirkCounter = 0;
//        quirkCounter = Directory.GetFiles("Assets/Resources/Characters/").Length;

//        if (!(quirkCounter == 0)) {
//            quirkCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/Characters/ch" + quirkCounter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }

//    [MenuItem("Assets/Create/Character/Role")]
//    public static void CreateRoleTemp() {
//        RoleObject asset = ScriptableObject.CreateInstance<RoleObject>();
//        int counter = 0;
//        counter = Directory.GetFiles("Assets/Resources/RoleFolder/").Length;

//        if (!(counter == 0)) {
//            counter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
//        }
//        AssetDatabase.CreateAsset(asset, "Assets/Resources/RoleFolder/role" + counter + ".asset");
//        AssetDatabase.SaveAssets();
//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = asset;
//    }
//    #endregion

//}
