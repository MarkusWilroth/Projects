using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Knapsack {
    class Item {
        public string itemName;
        public int itemWeight, itemValue;
        public double huValue;

        public Item(string itemName, Random rand) {
            this.itemName = itemName;
            int w = rand.Next(2, 7);
            int v = rand.Next(2, 7);
            itemWeight = w; //Alla random items får samma stats?...
            itemValue = v;
            huValue = (double)Decimal.Divide(itemValue, itemWeight);

            //switch (itemName) {
            //    case "axe":
            //        itemWeight = 2;
            //        itemValue = 4;
            //        break;
            //    case "pillow":
            //        itemWeight = 1;
            //        itemValue = 2;
            //        break;
            //    case "drugz":
            //        itemWeight = 1;
            //        itemValue = 3;
            //        break;
            //    case "computer":
            //        itemWeight = 4;
            //        itemValue = 5;
            //        break;
            //    case "nice rock":
            //        itemWeight = 5;
            //        itemValue = 2;
            //        break;
            //    case "ToroToro":
            //        itemWeight = 5;
            //        itemValue = 10;
            //        break;
            //}

            //if (itemWeight <= 0 || itemValue <= 0) { //Kommer hit om det inte är något av följande items
            //    int w = rand.Next(2, 7);
            //    int v = rand.Next(2, 7);
            //    itemWeight = w; //Alla random items får samma stats?...
            //    itemValue = v;
            //}
        }

    }
}
