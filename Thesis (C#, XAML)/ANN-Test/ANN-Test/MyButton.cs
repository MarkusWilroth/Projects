using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ANN_Test {
    public class MyButton : Button {
        public int parentNr;

        public MyButton(int parentNr) {
            this.parentNr = parentNr;
        }
    }
}
