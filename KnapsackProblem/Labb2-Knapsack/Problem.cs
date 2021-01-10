using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Knapsack {

    class Problem {

        static void Main(string[] ags) {

            int amountOfKnaps, amountOfItems, knapSize;
            string[] itemName; //Kan tas bort med finns lite för att det blir roligare och för att garantera att maxItemWeight > maxKnapWeight
            List<Knapsack> greedySackList; //För greedy algorithm
            List<Knapsack> hoodSackList; //För neighborhood search algorithm
            List<Item> itemList; //Alla våra items
            List<Item> itemsLeft;
            Random rand = new Random();
            
            amountOfKnaps = 2; //Hur många knaps vi vill ha
            amountOfItems = 30; //Hur många items vi vill ha
            knapSize = 10; //Hur mycket vikt varje knap kan hålla

            greedySackList = new List<Knapsack>();
            hoodSackList = new List<Knapsack>();
            itemList = new List<Item>();
            itemsLeft = new List<Item>();
            itemName = new string[] { "axe", "pillow", "drugz", "computer", "nice rock", "ToroToro" };

            greedySackList = CreateKnapsacks(amountOfKnaps, knapSize); //Skapar greedySackList
            hoodSackList = CreateKnapsacks(amountOfKnaps, knapSize); //Skapar hooSackList (Anledningen för att vara uppdelade är för att de ska jämföras, hood måste vara bättre eller lika bra som greedy!
            itemList = CreateItems(amountOfItems, itemName, rand); //Skapar items

            Greedy(greedySackList, itemList , itemsLeft); //Ska fylla greedySackList enligt en Greedy Algorithm
            Hood(hoodSackList, greedySackList, itemsLeft);

            PrintResult(itemList, greedySackList, hoodSackList);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public static List<Knapsack> CreateKnapsacks(int amountOfKnaps, int knapSize) {
            List<Knapsack> knapList = new List<Knapsack>();
            for (int i = 0; i < amountOfKnaps; i++) {
                Knapsack knap = new Knapsack(knapSize);
                knapList.Add(knap);
            }
            return knapList;
        }

        public static List<Item> CreateItems(int amountOfItems, string[] itemName, Random rand) {
            List<Item> itemList = new List<Item>();
            Item item;
            for (int i = 0; i < amountOfItems; i++) {
                if (i < itemName.Length) { //Om vi har flera items än vad jag har orkat ge namn till blir det ett vanligt Item
                    item = new Item(itemName[i], rand);
                } else {
                    item = new Item("Item("+ (i + 1) +")", rand);
                }
                itemList.Add(item);
                
            }
            return itemList;
        }

        public static void Greedy(List<Knapsack> greedyKnap, List<Item> items, List<Item> itemsLeft) { //Greedy Algorithm
            List<Item> greedyItem = new List<Item>(); //För att se till att vi inte behöver återställa items
            List<Item> greedyItemLeft = itemsLeft;
            foreach (Item item in items) {
                greedyItem.Add(item);
            }

            int itemA = 0; //Första item i listan
            int itemB = 1; //Andra item i listan (Eftersom items tas bort stämmer detta alltid)

            bool isDone = false;

            while (!isDone) {
                
                //Kollar ifall det finns mer att göra
                if (CheckIfDone(greedyKnap, greedyItem)) {
                    isDone = true;
                    break;
                }
                
                //Själva greedy Algorithm
                if (greedyItem.Count > 1) { //Im det inte finns flere än 1 item 
                    for (int i = 0; i < greedyItem.Count; i++) {
                        if (greedyItem[itemA].huValue >= greedyItem[itemB].huValue) { //Kollar vilket  av items som är bäst
                            if (!AddItem(greedyKnap, greedyItem[itemA])) { //Lägger till item om det går
                                greedyItemLeft.Add(greedyItem[itemA]); //Om det inte går hamnar items i greedyItemLeft
                            }
                            greedyItem.Remove(greedyItem[itemA]); //Tar bort item så att det inte testas igen

                        } else if (greedyItem[itemA].huValue < greedyItem[itemB].huValue) {
                            if(!AddItem(greedyKnap, greedyItem[itemB])) {
                                greedyItemLeft.Add(greedyItem[itemB]);
                            }
                            greedyItem.Remove(greedyItem[itemB]);
                        }
                    }
                } else {
                    if (!AddItem(greedyKnap, greedyItem[itemA])) {
                        greedyItemLeft.Add(greedyItem[itemA]);
                    }
                    greedyItem.Remove(greedyItem[itemA]);
                }
            }
        }
        /* ToDo:
         * 1. Start using the solution from the Greedy Algorithm
         * 2. Find some near-solutions
         * 3. Replace current solution if the new one is better
         */

        public static void Hood(List<Knapsack> hoodKnap, List<Knapsack> greedKnap, List<Item> itemsLeft) {
            //1. Ser till att hoodKnap börjar som det greedKnap fick fram
            for (int i = 0; i < greedKnap.Count; i++) {
                foreach (Item item in greedKnap[i].itemsInKnap) {
                    hoodKnap[i].AddItem(item);
                }
            }
            //Tar fram alla items som är "utanför" som kanske kan komma in
            
            List<Item> hoodItemLeft = new List<Item>();
            foreach (Item item in itemsLeft) {
                hoodItemLeft.Add(item);
            }

            bool isDone = false;

            while (!isDone) {
                //Kollar ifall det finns mer att göra
                if(CheckIfDone(hoodKnap, hoodItemLeft)) {
                    break;
                }

                //Själva hood Algorithm
                //Step 1: Kollar så det är de bästa items som ligger i väskan
                bool foundChange = false; 
                foreach (Knapsack hoodSack in hoodKnap) {
                    foreach (Item sackItem in hoodSack.itemsInKnap) {
                        foreach (Item leftItem in hoodItemLeft) {
                            if (leftItem.itemValue >= sackItem.itemValue && leftItem.itemWeight < sackItem.itemWeight) { //Om detta är sant är det lika värdefullt men väger mindre (
                                hoodSack.ReplaceItem(sackItem, leftItem);
                                hoodItemLeft.Remove(leftItem);
                                hoodItemLeft.Add(sackItem);
                                foundChange = true;
                                break;
                            }
                        }
                        if (foundChange) {
                            break;
                        }
                    }
                }
                //Step 2: kolla om vi kan rotera några föremål så att det blir mer plats
                int moveSize = 0;
                Knapsack knapToGetItem = null;
                foreach (Knapsack hoodSack in hoodKnap) {
                    if (hoodSack.maxWeight - hoodSack.totWeight > 0) { //Den har plats för flera items, om den är smockfull vill vi inte röra den

                        if (knapToGetItem != null) { //Om det redan finns en knap som behöver items
                            moveSize = knapToGetItem.maxWeight - knapToGetItem.totWeight;
                            foreach (Item item in hoodSack.itemsInKnap) {
                                if (item.itemWeight <= moveSize) {
                                    knapToGetItem.AddItem(item);
                                    hoodSack.RemoveItem(item);
                                    knapToGetItem = null;
                                    foundChange = true;
                                    break;
                                }
                            }
                        } else {
                            knapToGetItem = hoodSack;
                        }
                    }
                }
                //Step 3: Kollar alla items ifall de kan få plats i någon knapsack
                foreach(Knapsack hoodSack in hoodKnap) {
                    if (hoodSack.maxWeight - hoodSack.totWeight > 0) {
                        foreach (Item item in hoodItemLeft) {
                            if (item.itemWeight <= hoodSack.maxWeight - hoodSack.totWeight) {
                                hoodSack.AddItem(item);
                                hoodItemLeft.Remove(item);
                                foundChange = true;
                                break;
                            }
                        }
                    }
                }

                if (!foundChange) {
                    isDone = true;
                    break;
                } else {
                    Console.WriteLine("Found a change");
                }

                //Idé kollar bland itemsLeft och items efter någon som har bättre eller lika huValue fast väger mindre
                //Om en knapsack inte är helt full, testa att flytta föremål mellan varandra så kanske ett itemLeft får plats i en knapsack
            }
        }

        public static bool AddItem(List<Knapsack> knapSack, Item item) {
            List<Knapsack> knapList = knapSack;
            bool isPlaced = false;
            int sizeLeft = int.MinValue;
            Knapsack bestKnap = knapList[0];

            foreach (Knapsack knap in knapList) {
                if (knap.maxWeight - knap.totWeight >= sizeLeft) {
                    bestKnap = knap;
                    sizeLeft = (knap.maxWeight - knap.totWeight);
                }
            }

            foreach (Knapsack knap in knapList) {
                if (knap == bestKnap) {
                    isPlaced = knap.AddItem(item);
                    break;
                }
            }
            
            return isPlaced;

        }

        public static bool CheckIfDone(List<Knapsack> knapSack, List<Item> item) {
            bool isDone = false;
            int knapsWithPlace = 0;
            foreach (Knapsack knap in knapSack) { //Kollar alla knaps ifall de har plats kvar
                if (knap.totWeight < knap.maxWeight) { //Om derast totWeight är mindre än max weight har de plats kvar
                    knapsWithPlace++;
                }
            }
            if (knapsWithPlace <= 0) { //Om det inte finns någon plats kvar är den klar
                isDone = true;
                return isDone;
            }
            if (item.Count == 0) { //Om det inte finns några items kvar att kolla är den klar
                isDone = true;
                return isDone;
            }

            return isDone; //Borde returna false (Kan egentligen ta bort isDone och bara ha return false, return true)
        }

        public static void PrintResult(List<Item> itemList, List<Knapsack> greedySackList, List<Knapsack> hoodSackList) { //Skriver ut resultatet
            int totItemValue = 0;
            int totItemWeight = 0;
            int totGreedValue = 0;
            int totHoodValue = 0;

            Console.WriteLine("\n----------------------------------------\n");

            Console.WriteLine("The items(" + itemList.Count + ") that have to be desputed:");
            foreach (Item item in itemList) {
                Console.WriteLine("Item: " + item.itemName + ", value: " + item.itemValue + ", weight: " + item.itemWeight + ", huValue: " + item.huValue);
                totItemValue += item.itemValue;
                totItemWeight += item.itemWeight;
            }
            Console.WriteLine("\nTotal item value: " + totItemValue + "\nTotal item weight: " + totItemWeight);

            Console.WriteLine("\n----------------------------------------\n");

            foreach (Knapsack greedyKnap in greedySackList) {
                Console.WriteLine("The GreedyKnap contains: " + greedyKnap.itemsInKnap.Count + " items\nTotal value: " + greedyKnap.totValue + "\nTotal weight: " + greedyKnap.totWeight + "\n");
                Console.WriteLine("This knap contains these items:");
                foreach (Item item in greedyKnap.itemsInKnap) {
                    Console.WriteLine("Item: " + item.itemName + ", value: " + item.itemValue + ", weight: " + item.itemWeight + ", huValue: " + item.huValue);
                    totGreedValue += item.itemValue;
                }
                Console.WriteLine("\n----------------------------------------\n");
            }
            
            foreach (Knapsack hoodKnap in hoodSackList) {
                Console.WriteLine("The HoodKnap contains: " + hoodKnap.itemsInKnap.Count + " items\nTotal value: " + hoodKnap.totValue + "\nTotal weight: " + hoodKnap.totWeight + "\n");
                Console.WriteLine("This knap contains these items:");
                foreach (Item item in hoodKnap.itemsInKnap) {
                    Console.WriteLine("Item: " + item.itemName + ", value: " + item.itemValue + ", weight: " + item.itemWeight + ", huValue: " + item.huValue);
                    totHoodValue += item.itemValue;
                }
                Console.WriteLine("\n----------------------------------------\n");
            }
            Console.WriteLine("Total Greed Value: " + totGreedValue);
            Console.WriteLine("Total Hood Value: " + totHoodValue);
            Console.WriteLine("\n----------------------------------------\n");
        }
    }
}
