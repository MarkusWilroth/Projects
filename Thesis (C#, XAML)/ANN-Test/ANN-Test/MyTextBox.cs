using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ANN_Test {
    public class MyTextBox : TextBox {

        public int parentNr;

        public MyTextBox(int parentNr) {
            this.parentNr = parentNr;
        }

    }
}
