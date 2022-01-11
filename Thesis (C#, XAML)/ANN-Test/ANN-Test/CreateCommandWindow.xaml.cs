using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VCTUtility;

namespace ANN_Test {
    /// <summary>
    /// Interaction logic for CreateCommandWindow.xaml
    /// </summary>
    public partial class CreateCommandWindow : Window {
        private List<VoiceBox> voiceBoxList;
        private string text, command, tag;
        private StackPanel[] voicePanel;
        private StackPanel[] recPanel;
        private int voicePCount = 5; //Hur många voicePanels det ska vara (Hur många som ska spela in röst)
        private int recPCount = 10; //Hur många max rec per voice (Hur många gånger en person ska spela in ett command)
        private int recSize = 20;
        private int boxSpace = 10; //Spaces between commands
        private int boxSize = 40, startOff = 10;
        private double scrollOff, displayed;

        private int windowSize = 311;
        private int recCount = 0;
        private int vdCount = 0;
        private int activeVD = 0;

        private bool activeScroll = false;
        private bool isRec = false;
        //private RecordAudio recAudio; //När klasserna tas bort härifrån behöver man inte skriva VCTUtility framför
        private string id;
        private MainWindow main;

        public CreateCommandWindow(MainWindow main, string id, int sampleRate) {
            InitializeComponent();
            this.main = main;
            this.id = id;
            
            voiceBoxList = new List<VoiceBox>();
            //recAudio = new RecordAudio(sampleRate);
            InitializeWindow();
        }

        public CreateCommandWindow(MainWindow main, CommandData cData, int sampleRate) {
            InitializeComponent();
            InitializeWindow();

            RecordAudio.recAudio = new RecordAudio(sampleRate);

            this.main = main;
            id = cData.id;
            text = cData.text;
            command = cData.command;
            tag = cData.tag;

            int count = 0;
            voiceBoxList = new List<VoiceBox>();
            foreach (VoiceData vd in cData.voiceDataList) {
                VoiceBox vb = new VoiceBox(recPCount, vd);
                voiceBoxList.Add(vb);
                voicePanel[count].Visibility = Visibility.Visible;

                foreach (UIElement child in voicePanel[count].Children) {
                    if (child is TextBox) {
                        TextBox sName = (TextBox)child;
                        if (sName.Name == "tbSpeaker") {
                            sName.Text = vd.speakerName;
                            break;
                        }
                    }
                }
                count++;
            }

            TBText.Text = text;
            TBCommand.Text = command;
            TBTag.Text = tag;
            ScrollChecker();
            ApplyOffset();
        }

        private void InitializeWindow() {
            voicePanel = new StackPanel[] { VD1, VD2, VD3, VD4, VD5, VD6, VD7, VD8, VD9, VD10 };
            recPanel = new StackPanel[] { Rec1, Rec2, Rec3, Rec4, Rec5, Rec6, Rec7, Rec8, Rec9, Rec10, Rec11, Rec12, Rec13, Rec14, Rec15, Rec16, Rec17, Rec18, Rec19, Rec20 };
            voiceBoxList = new List<VoiceBox>();

            voicePCount = Settings.settings.voicePCount;
            recPCount = Settings.settings.recPCount;

            if (voicePCount > voicePanel.Length) {
                voicePCount = voicePanel.Length;
            }

            if (recPCount > recPanel.Length) {
                recPCount = recPanel.Length;
            }

            for (int i = 0; i < voicePCount; i++) {
                voicePanel[i] = CreateVDPanel(voicePanel[i], i);
            }

            for (int i = 0; i < recPCount; i++) {
                recPanel[i] = CreateRecPanel(recPanel[i], i);
            }
        }

        private void ApplyOffset() {
            RemoveOffset();
            if (voiceBoxList[activeVD].isExpanded) {
                recCount = voiceBoxList[activeVD].bufferList.Count;
                if (recCount < recPCount) {
                    recCount++; //Gör plats för knappen
                }
            } else {
                recCount = 0;
            }
            
            Thickness margin;
            int vdOffset = (recCount * recSize) + boxSpace;
            margin = voicePanel[activeVD + 1].Margin;
            margin.Top = vdOffset;
            voicePanel[activeVD + 1].Margin = margin;

            int recOffset = (activeVD + 1) * (boxSize + boxSpace);
            margin = recPanel[0].Margin;
            margin.Top = recOffset + scrollOff;
            recPanel[0].Margin = margin;

            vdOffset += recSize;
            int btnOffset = vdOffset + (activeVD * (boxSize + boxSpace));
            margin = BtnCreateRec.Margin;
            margin.Top = btnOffset + scrollOff;
            BtnCreateRec.Margin = margin;
            if (recCount < recPCount) {
                //if (voiceBoxList[activeVD].isExpanded) { //Borde hända när man klickar på expand...
                //    BtnCreateRec.Visibility = Visibility.Visible;
                //} else {
                //    BtnCreateRec.Visibility = Visibility.Hidden;
                //}
                
            } else {
                //BtnCreateRec.Visibility = Visibility.Hidden;
            }
            if (voiceBoxList.Count != 0) {
                ApplyScrollOffset(recCount);
                UpdateBtnCreateVD(recCount);
            }
            
        }

        private void ApplyScrollOffset(int recCount) {
            int boxes = voiceBoxList.Count;
            int recOff = 0;
            //int recOff = recCount * recSize;
            for (int i = 0; i < boxes; i++) {
                int boxOff = (boxSize + 10) * i;
                if (scrollOff > (boxOff + recOff) * -1) { //Tänk om detta! - Gör så att vi endast flyttar de som är närmast toppen
                    break;
                } else {
                    double activeOffset = scrollOff + boxOff + recOff + 10; //Flyttar VoiceBox (recOff får sitt värde efter VoiceBox som är extended får sitt)
                    Thickness margin = voicePanel[i].Margin;
                    margin.Top = activeOffset;
                    voicePanel[i].Margin = margin;

                    if (voiceBoxList[i].isExpanded) {
                        recOff = recCount * recSize;
                        //Thickness recMargin = recPanel[0].Margin;
                        //recMargin.Top += scrollOff + boxOff;
                        //recPanel[0].Margin = recMargin;
                    }
                }
            }
        }

        private void UpdateBtnCreateVD(int recCount) { //Move back?
            vdCount = voiceBoxList.Count;

            int vdOffset = (recCount * recSize) + boxSpace;
            if (vdCount < voicePCount) {
                int btnOffset = (vdCount * (boxSize + boxSpace)) + vdOffset;
                Thickness margin = BtnCreateVD.Margin;
                margin.Top = btnOffset;
                BtnCreateVD.Margin = margin;
            }
            else {
                BtnCreateVD.Visibility = Visibility.Hidden;
            }
        }

        private void RemoveOffset() {
            for (int i = activeVD; i < voicePCount; i++) {
                Thickness margin = voicePanel[i].Margin;
                margin.Top = boxSpace;
                voicePanel[i].Margin = margin;
            }
        }

        #region scroller
        private void ScrollChecker() {
            vdCount = voiceBoxList.Count;
            if (vdCount < voicePCount) {
                vdCount++; //Ökar för knappen
            }
            displayed = vdCount * (boxSize + boxSpace); //Displayed är hur stor plats allt tar på skärmen
            if (voiceBoxList[activeVD].isExpanded) {
                recCount = voiceBoxList[activeVD].bufferList.Count;
                if (recCount < recPCount) { //För att göra plats för Rec knappen
                    recCount++;
                }
                displayed += recSize * recCount;
            }

            if (displayed >= windowSize) {
                Scroller.Visibility = Visibility.Visible;
                activeScroll = true;
            }
            else {
                Scroller.Visibility = Visibility.Hidden;
                scrollOff = 0;
                Thickness margin = voicePanel[0].Margin;
                margin.Top = 10; //ersätt 10 med startOff
                voicePanel[0].Margin = margin;
                activeScroll = false;
            }
        }

        private void ScrollerManager(double scrollValue) {
            double maxOffset = displayed - windowSize; //MaxOffset är hur mycket den kan flyttas som max
            if (maxOffset < 0) {
                maxOffset = 0;
            }
            scrollOff = maxOffset * scrollValue * -1;
            ApplyOffset();
            //int boxSpace = 
            //for (int i = 0; i < voiceBoxList.Count; i++) {
            //    if (scrollOffset <)
            //}
        }
        #endregion

        private CommandData Save() {
            text = TBText.Text;
            command = TBCommand.Text;
            tag = TBTag.Text;
            List<VoiceData> vdList = new List<VoiceData>();

            for (int i = 0; i < voiceBoxList.Count; i++) {
                string speakerName = "";
                foreach (var element in voicePanel[i].Children) {
                    if (element is MyTextBox) {
                        MyTextBox tb = (MyTextBox)element;
                        voiceBoxList[i].GetSpeaker(tb.Text);
                        speakerName = tb.Text;
                        break;
                    }
                }
                VoiceData vd = new VoiceData(speakerName, voiceBoxList[i].bufferList);
                vdList.Add(vd);
            }
            CommandData cData = new CommandData(id, text, command, tag, vdList);
            return cData;
        }
        private void DeExpandVD(int boxNr) {
            voiceBoxList[boxNr].DeExpand();
            for (int i = 0; i < voiceBoxList[boxNr].bufferList.Count; i++) {
                recPanel[i].Visibility = Visibility.Hidden;
            }
            BtnCreateRec.Visibility = Visibility.Hidden;
        }

        private void ExpandVD(int activeVD) {
            voiceBoxList[this.activeVD].Expand();
            DisplayRec(activeVD);
            DispalyBtnRec(activeVD);
            
        }

        private void DispalyBtnRec(int activeVD) {
            if (voiceBoxList[activeVD].bufferList.Count >= recPCount) {
                BtnCreateRec.Visibility = Visibility.Hidden; //Onödig?
            }
            else {
                BtnCreateRec.Visibility = Visibility.Visible;
            }
        }

        private void DisplayRec(int boxNr) {
            int recBoxes = voiceBoxList[boxNr].bufferList.Count;
            for (int i = 0; i < recBoxes; i++) {
                recPanel[i].Visibility = Visibility.Visible;
            }
            if (recBoxes < recPCount) {
                for (int i = recBoxes; i < recPCount; i++) {
                    recPanel[i].Visibility = Visibility.Hidden;
                }
            }
        }

        #region Buttons
        private void BtnReturn_Click(object sender, RoutedEventArgs e) {
            CommandData cData = Save();
            RecordAudio.recAudio.Dispose();
            main.GetCommand(cData);
            Hide();
            main.Show();
            //Return to Main
        }

        private void BtnExpand_Click(object sender, RoutedEventArgs e) { //Is expanded is clicked
            MyButton button = (MyButton)sender;
            if (button.parentNr < voiceBoxList.Count) { //Checks if the button is valid
                if (activeVD != button.parentNr) { //Kollar ifall man klickar på den som redan är aktiv
                    DeExpandVD(activeVD); //Om man inte gör det ska den gamla aktiva inaktiveras
                    activeVD = button.parentNr; //Activa ska sättas till den nya
                    ExpandVD(activeVD); //Expandera den nya
                } else {
                    if (voiceBoxList[activeVD].isExpanded) {
                        DeExpandVD(activeVD);
                    } else {
                        ExpandVD(activeVD);
                    }
                }
                ApplyOffset();
                ScrollChecker();
            } else {
                System.Diagnostics.Debug.WriteLine("ERROR! Button: " + button.parentNr + " was higher than voiceDataList Count: " + voiceBoxList.Count);
            }
        }

        private void BtnCreateVD_Click(object sender, RoutedEventArgs e) {
            if (voiceBoxList.Count > 0) {
                DeExpandVD(activeVD);
            }
            
            VoiceBox vd = new VoiceBox(recPCount);
            
            voiceBoxList.Add(vd);

            voicePanel[voiceBoxList.Count - 1].Visibility = Visibility.Visible;
            activeVD = voiceBoxList.Count - 1;
            ExpandVD(activeVD);
            ScrollChecker();
            ApplyOffset();
            //Add Offset? Can that happend so that it is needed
        }

        private void BtnCreateRec_Click(object sender, RoutedEventArgs e) {
            if (!isRec) {
                BtnCreateRec.Content = "Press To Stop Recording!";
                RecordAudio.recAudio.StartRec();
                isRec = true;
                //Start Recording
            } else {
                BtnCreateRec.Content = "Press To Record Audio!";
                byte[] buffer = RecordAudio.recAudio.StopRecB();
                voiceBoxList[activeVD].AddRec(buffer);
                isRec = false;
                ApplyOffset();

                //ExpandVD(activeVD);
                ScrollChecker();
                DisplayRec(activeVD);
                DispalyBtnRec(activeVD);
                //Stop Recording
            }
        }

        private void BtnPlayRec_Click(object sender, RoutedEventArgs e) {
            MyButton btnPlay = (MyButton)sender;
            byte[] buffer = voiceBoxList[activeVD].GetBuffer(btnPlay.parentNr);
            RecordAudio.recAudio.PlayAudio(buffer);
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e) {
            MyButton btnDel = (MyButton)sender;
            voiceBoxList[activeVD].DeleteRec(btnDel.parentNr);
            DisplayRec(activeVD);
            DispalyBtnRec(activeVD);
            ApplyOffset();
        }

        private void Scroller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (activeScroll) {
                double scrollerValue = Scroller.Value;
                ScrollerManager(scrollerValue);
            }
        }
        #endregion

        #region CreatePanels
        private StackPanel CreateVDPanel(StackPanel sp, int i) {
            SolidColorBrush borderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF6ABE30");
            SolidColorBrush insideBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF51A482");
            SolidColorBrush insideBrush2 = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF7EBDA3");
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            FontFamily timesNewRoman = new FontFamily("Times New Roman");

            sp.Margin = new Thickness(0, 0, 0, 0);
            sp.Visibility = Visibility.Hidden;

            sp.Children.Add(new Border {
                BorderBrush = borderBrush,
                Background = insideBrush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                Height = 40,
                Width = 580
            });

            sp.Children.Add(new Label {
                Name = "lbNr" + i,
                Content = i + 1,
                FontFamily = timesNewRoman,
                FontSize = 25,
                Width = 35,
                Margin = new Thickness(-530, -40, 0, 0),
                Foreground = whiteBrush,
                FontWeight = FontWeights.Bold
            });
            MyTextBox tbSpeaker = new MyTextBox(i) {
                Name = "tbSpeaker",
                Text = "Speaker Name",
                FontFamily = timesNewRoman,
                FontSize = 20,
                Width = 250,
                Height = 32,
                Margin = new Thickness(-260, -40, 0, 0),
                Background = insideBrush2,
                BorderBrush = borderBrush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                SelectionTextBrush = whiteBrush,
                Foreground = whiteBrush,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            sp.Children.Add(tbSpeaker);

            sp.Children.Add(new Border {
                Width = 200,
                Height = 32,
                Margin = new Thickness(220, -40, 0, 0),
                BorderBrush = borderBrush,
                Background = insideBrush2,
                BorderThickness = new Thickness(2, 2, 2, 2)
            });

            sp.Children.Add(new Label {
                Name = "lbVD" + i,
                Margin = new Thickness(220, -40, 0, 0),
                Width = 180,
                Height = 32,
                Content = "99/99 Completed",
                FontFamily = timesNewRoman,
                FontSize = 20,
                Foreground = whiteBrush,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center
            });

            MyButton btnExpand = new MyButton(i) {
                Name = "BtVD1Expand",
                Margin = new Thickness(500, -40, 0, 0),
                Width = 50,
                Height = 32,
                Content = "v",
                FontFamily = timesNewRoman,
                FontSize = 20,
                Foreground = whiteBrush,
                BorderBrush = borderBrush,
                Background = insideBrush2,
                BorderThickness = new Thickness(2, 2, 2, 2)

            };
            btnExpand.Click += new RoutedEventHandler(BtnExpand_Click);
            sp.Children.Add(btnExpand);


            return sp;
        }

        private StackPanel CreateRecPanel(StackPanel sp, int i) {
            SolidColorBrush borderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF6ABE30");
            SolidColorBrush insideBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF51A482");
            SolidColorBrush insideBrush2 = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF7EBDA3");
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            FontFamily timesNewRoman = new FontFamily("Times New Roman");

            sp.Margin = new Thickness(0, 0, 0, 0);
            sp.Visibility = Visibility.Hidden;

            sp.Children.Add(new Border {
                BorderBrush = borderBrush,
                Background = insideBrush,
                BorderThickness = new Thickness(2,0,2,2),
                Width = 400,
                Height = 20
            });

            sp.Children.Add(new Label {
                Margin = new Thickness(-370,-22,0,0),
                Content = i+1,
                FontFamily = timesNewRoman,
                FontSize = 15,
                Foreground = whiteBrush,
                Width = 24,
                Height = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Padding = new Thickness( 0,0,0,0)
            });

            MyButton btnPlay = new MyButton(i) {
                Name = "BtnPlay" + i, //needed?
                Margin = new Thickness(-100, -22, 0, 0),
                Content = "Play Recording",
                FontFamily = timesNewRoman,
                FontSize = 12,
                Foreground = whiteBrush,
                Width = 110,
                Height = 15,
                Padding = new Thickness(1, -1, 1, 0),
                VerticalContentAlignment = VerticalAlignment.Center,
                BorderBrush = borderBrush,
                Background = insideBrush2,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            btnPlay.Click += new RoutedEventHandler(BtnPlayRec_Click);
            sp.Children.Add(btnPlay);

            MyButton btnDel = new MyButton(i) {
                Name = "BtnDel" + i,
                Margin = new Thickness(200, -22, 0, 0),
                Content = "Delete Recording",
                FontFamily = timesNewRoman,
                FontSize = 12,
                Foreground = whiteBrush,
                Width = 110,
                Height = 15,
                Padding = new Thickness(1, -1, 1, 0),
                VerticalContentAlignment = VerticalAlignment.Center,
                BorderBrush = borderBrush,
                Background = insideBrush2,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            btnDel.Click += new RoutedEventHandler(BtnDel_Click);
            sp.Children.Add(btnDel);
            return sp;
        }
        #endregion
    }
}
