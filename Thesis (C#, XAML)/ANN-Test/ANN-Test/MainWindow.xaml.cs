using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime;
using VCTUtility;
using System.Threading;

namespace ANN_Test {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private bool isRec;
        //private RecordAudio recAudio;
        public Network network;
        private DataTuple dTuple;
        private List<string> tagList;
        private double annAcc = 0.8;
        //private MFCC mfcc;
        //private int sampleRate = 8000;
        private string path;
        private MFCC mfcc;
        private MarkusMFCC mMFCC;
        private List<StackPanel> comPanelList;
        //private int numberOfCoef = 20;
        private int boxSize = 40;
        private int windowSize = 281; //Byt namn - inte MFCC WindowSize utan hur stor rutan där boxes är
        private bool isTesting = false;
        private Settings s = new Settings();
        private string prog = "Progress: ";

        public List<CommandData> cDataList = new List<CommandData>();

        private double[][] testValues;

        public MainWindow() {
            InitializeComponent();
            //TestANN();
            comPanelList = new List<StackPanel>();
            foreach (StackPanel sp in ComPanel.Children) {
                comPanelList.Add(sp);
            }

            Settings.settings = new Settings();
            s = Settings.settings;
            mfcc = new MFCC(s.sampleRate, s.windowSize, s.nrOfCoef, s.useFirstCoef, s.minFreq, s.maxFreq, s.nrOfFilters);
            mMFCC = new MarkusMFCC(s.sampleRate, s.windowSize, 0.025, 0.01, s.minFreq, s.maxFreq, 512, 40);
            RecordAudio.recAudio = new RecordAudio(s.sampleRate);
            //recAudio = new RecordAudio(s.sampleRate);
            BtnCreateANN.IsEnabled = true;
            BtnTestANN.IsEnabled = true;
            //TxtProg.Content = "Test";
            //TxtProg.Content = "HMMMM";
            //tdataList = new List<TrainingData>();
            //tagList = new List<string>();
            ////mfcc = new MFCC(sampleRate, 512, 20, true, 20.0, 8000.0, 40);
            //newMFCC = new NewMFCC();
        }

        public void GetCommand(CommandData cData) {
            bool isEdit = false;
            int boxNr = 0;
            for (int i = 0; i < cDataList.Count; i++) {
                if (cDataList[i].id.Equals(cData.id)) { //If this aldready exists it will be replaced
                    cDataList[i] = cData;
                    isEdit = true;
                    boxNr = i;
                    break;
                }
            }

            if (!isEdit) {
                cDataList.Add(cData);
                boxNr = cDataList.Count - 1;
            }
            else {
                comPanelList[boxNr].Children.Clear();
            }
            comPanelList[boxNr] = CreateComPanel(comPanelList[boxNr], boxNr, cData);
            comPanelList[boxNr].Visibility = Visibility.Visible;
            BtnCreateANN.IsEnabled = true;
            //ShowCommandBoxes();
        }

        private void LoadCommand(CommandData cData) {

        }

        private void ShowCommandBoxes() {
            int commands = cDataList.Count;

            for (int i = 0; i < commands; i++) {
                comPanelList[i] = CreateComPanel(comPanelList[i], i, cDataList[i]);
                comPanelList[i].Visibility = Visibility.Visible;
            }

            for (int i = commands; i < comPanelList.Count; i++) {
                comPanelList[i].Visibility = Visibility.Hidden;
            }

            int counter = 0;
            for (int i = 0; i < comPanelList.Count; i++) {
                if (comPanelList[i].Visibility == Visibility.Visible) {
                    counter++;
                }
            }
        }

        #region SaveLoad
        private void Load() {
            path = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true) {
                path = ofd.FileName;
                if (path != null) {
                    SaveData sData = SaveSystem.Load(path);
                    Open(sData);
                }
            }
        }

        private void LoadANN() {
            path = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true) {
                path = ofd.FileName;
                if (path != null) {
                    NetworkData nData = SaveSystem.LoadANN(path);
                    network = new Network(nData);

                    if (nData.settings != null) {
                        Settings.settings = nData.settings;
                        s = Settings.settings;
                    }
                }
            }
        }

        private void Open(SaveData sData) {
            cDataList = sData.cDataList;
            int commands = cDataList.Count;

            if (sData.savedNetwork.settings != null) {
                Settings.settings = sData.savedNetwork.settings;
                s = Settings.settings;
            }

            if (cDataList.Count > 0) {
                for (int i = 0; i < cDataList.Count; i++) {
                    comPanelList[i].Children.Clear();
                    comPanelList[i] = CreateComPanel(comPanelList[i], i, cDataList[i]);
                    comPanelList[i].Visibility = Visibility.Visible;
                }
                if (commands > cDataList.Count) {
                    for (int i = cDataList.Count; i < commands; i++) {
                        comPanelList[i].Visibility = Visibility.Hidden;
                    }
                }
                BtnCreateANN.IsEnabled = true;
            }
            else {
                BtnCreateANN.IsEnabled = false;
            }

            if (network != null) {
                network = new Network(sData.savedNetwork);
                BtnTestANN.IsEnabled = true;
            }
            else {
                BtnTestANN.IsEnabled = true;
            }

            //ShowCommandBoxes();
        }

        private void SaveAs() {
            path = "";
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true) {
                path = sfd.FileName;
                if (path != null) {
                    Save();
                }
            }
        }

        private void Save() {
            System.Diagnostics.Debug.WriteLine("Saving...");
            SaveSystem.Save(path, cDataList, network);
            System.Diagnostics.Debug.WriteLine("Saving Complete!");
        }
        #endregion

        #region ConvertTypeForm
        public static float[] ConvertByteToFloat(byte[] array) {
            int offset = 4;
            float[] floatResult = new float[array.Length / offset];

            for (int i = 0; i < floatResult.Length; i = i++) {
                if (BitConverter.IsLittleEndian) {
                    Array.Reverse(array, i * offset, offset);
                }
                floatResult[i] = BitConverter.ToSingle(array, i * offset);
            }
            return floatResult;
        }

        private double[] ByteToDouble(byte[] buffer) {
            double[] values = new double[buffer.Length / 8];

            for (int i = 0; i < values.Length; i++) {
                values[i] = BitConverter.ToInt32(buffer, i * 8);
            }
            return values;
        }

        private short[] ByteToInt16(byte[] buffer) {
            short[] sBuffer = buffer.Select(n => Convert.ToInt16(n)).ToArray();
            return sBuffer;
        }

        private static float ReadSingle(byte[] data, int offset) {
            if (BitConverter.IsLittleEndian != false) {
                byte tmp = data[offset];
                data[offset] = data[offset + 3];
                data[offset + 3] = tmp;
                tmp = data[offset + 1];
                data[offset + 1] = data[offset + 2];
                data[offset + 2] = tmp;
            }
            return BitConverter.ToSingle(data, offset);
        }
        #endregion

        private void AddTag(string newTag) {
            foreach (string tag in tagList) {
                if (newTag == tag) {
                    return;
                }
            }
            tagList.Add(newTag);
        }

        private void DeleteComPanel(int delNr) {
            int commands = cDataList.Count;
            cDataList.RemoveAt(delNr);
            for (int i = delNr; i < commands; i++) {
                foreach (UIElement child in comPanelList[i].Children) {
                    comPanelList[i].Children.Remove(child);
                }
                if (i >= commands - 1) {
                    comPanelList[i].Visibility = Visibility.Hidden;
                }
                else {
                    comPanelList[i] = CreateComPanel(comPanelList[i], i, cDataList[i]);
                }
            }
        }

        private void ApplyOffset(double scrollOff, int boxes) {

            for (int i = 0; i < boxes; i++) {
                int boxOff = (boxSize + 10) * i;
                if (scrollOff > (boxOff * -1)) {
                    break;
                }
                else {
                    double activeOffset = scrollOff + boxOff;
                    Thickness margin = comPanelList[i].Margin;
                    margin.Top = activeOffset;
                    comPanelList[i].Margin = margin;
                }
            }
        }

        private void ScrollBoxes(double value) {
            int boxes = cDataList.Count;
            int displayed = boxes * (boxSize + 10);
            double maxOffset = displayed - windowSize;
            if (maxOffset < 0) {
                maxOffset = 0;
            }
            double offset = maxOffset * value;
            offset *= -1;
            ApplyOffset(offset, boxes);
        }

        private string CreateId() {
            int idNr = 0;
            string baseId = "com";
            string id = baseId + idNr;
            List<CommandData> tempList = cDataList;
            bool hasChanged = false;
            for (int i = 0; i <= cDataList.Count; i++) {
                id = baseId + i;
                foreach (CommandData cData in tempList) {
                    if (cData.id.Equals(id)) {
                        hasChanged = true;
                        //tempList.Remove(cData);
                        break;
                    }
                }
                if (!hasChanged) {
                    return id;
                }
            }
            id = baseId + cDataList.Count;
            return id;
        }

        #region CreateNetwork

        private Network CreateNetwork() {
            //Dispatcher.BeginInvoke(new Action(() => {
            //    TxtProg.Content = "Creating Trainingdata...";
            //}));
            //Thread.Sleep(1);
            Dispatcher.BeginInvoke(new Action(() => {
                TxtProg.Content = "Creating Trainingdata...";
            }));
            Thread.Sleep(10);
            System.Diagnostics.Debug.WriteLine("Creating Trainingdata...");

            List<TrainingData> tDataList = new List<TrainingData>();
            List<string> tagList = new List<string>();

            //NAudio.Wave.WaveBuffer waveBuffer;

            foreach (CommandData cData in cDataList) {
                string tag = cData.tag;
                tagList.Add(tag);
                //System.Diagnostics.Debug.WriteLine("- Tag: " + tag);
                foreach (VoiceData vd in cData.voiceDataList) {
                    if (vd.speakerName != "Peow Wiyada W.") {
                        //System.Diagnostics.Debug.WriteLine("VoiceData: ");
                        int i = 0;
                        foreach (byte[] buffer in vd.bufferList) {
                            i++;
                            //byte[] newBuff = EmptyRemoval(buffer);
                            short[] sBuffer = ByteToInt16(buffer);
                            //System.Diagnostics.Debug.WriteLine("sBuffer length: " + sBuffer.Length);
                            if (sBuffer.Length <= 3 || sBuffer == null) {
                                System.Diagnostics.Debug.WriteLine("Check Command: " + cData.command + " speaker: " + vd.speakerName + " recording: " + i);
                            }
                            else {

                                double[,] mCoef = mMFCC.GetMFCC(sBuffer);
                                TrainingData tData = new TrainingData(tag, mCoef);

                                //double[] coef = mfcc.ProcessToVector(sBuffer);
                                //TrainingData tData = new TrainingData(tag, coef);
                                tDataList.Add(tData);
                            }
                            //double[] dBuff = ByteToDouble(buffer);
                            //short[] mCoef = mMFCC.GetMFCC(sBuffer);

                            //foreach (float f in tData.attributes.Values) {
                            //    System.Diagnostics.Debug.Write(f + " ");
                            //}
                            //System.Diagnostics.Debug.WriteLine("");
                        }
                    }
                    
                    //System.Diagnostics.Debug.WriteLine("");
                }
            }
            dTuple = new DataTuple(tagList.Count, tDataList);
            Dispatcher.BeginInvoke(new Action(() => {
                TxtProg.Content = "Creating Network...";
            }));


            System.Diagnostics.Debug.WriteLine("Creating Network...");
            Network network = GetNetwork(tagList, dTuple, 0);
            Dispatcher.BeginInvoke(new Action(() => {
                TxtProg.Content = "Network Creation Completed";
            }));
            return network;
        }

        private Network GetNetwork(List<string> tags, DataTuple dTuple, int attempt) { //Byta bort tags till int?
            Network network = new Network(tags, dTuple); //Skapar ett nytt nätverk med den träningsdata som samlats
            if (ValidatedNetwork(network, attempt)) {
                return network; //Om den klarar valideringstestet skickas denna netverk tillbaka och är den som används
            }
            else {
                attempt++;
                return GetNetwork(tags, dTuple, attempt); //Klarar den inte testet kallas GetNetwork på och ett nytt nätverk skapas
            }
        }

        private bool ValidatedNetwork(Network network, int attempt) { //Goes through some of the recordings to check if the ANN returns the right values
            System.Diagnostics.Debug.WriteLine("Validating...");
            Dispatcher.BeginInvoke(new Action(() => {
                TxtProg.Content = "Validating...";
            }));
            //TxtProg.Content = "Validating...";
            int counter = 0;
            int totalCount = 0;
            int allowedMisses = 3;
            foreach (CommandData cData in cDataList) {
                totalCount += counter;
                counter = 0;
                foreach (VoiceData vd in cData.voiceDataList) {
                    string spkName = vd.speakerName;
                    string recordedCommand = cData.command;

                    for (int i = 0; i < vd.bufferList.Count; i++) {
                        byte[] buffer = vd.bufferList[i];
                        byte[] newBuff = EmptyRemoval(buffer);
                        short[] sBuffer = ByteToInt16(newBuff);
                        //double[] dBuff = ByteToDouble(buffer);
                        double[] coef = mfcc.ProcessToVector(sBuffer);
                        Dictionary<string, double> attributes = new Dictionary<string, double>();

                        for (int k = 0; k < s.nrOfCoef; k++) {
                            attributes.Add("IN-" + k, coef[k]);
                        }
                        string result = network.GetResult(attributes, annAcc);
                        if (!result.Equals(cData.tag)) { //Om resultatet inte är samma som det det ska vara
                            if (counter >= allowedMisses) {
                                TxtProg.Content = "Validation[" + attempt + "] failed! Speaker: " + spkName + " :Incoming Command: " + recordedCommand + " | Command Recognised: " + result + " Index of Buffer Breaking: " + (i + 1) + "! Making a new attempt...";
                                System.Diagnostics.Debug.WriteLine("Validation[" + attempt + "] failed! Speaker: " + spkName + " :Incoming Command: " + recordedCommand + " | Command Recognised: " + result + " Index of Buffer Breaking: " + (i + 1) + "! Making a new attempt...");
                                return false;
                            }
                            else {
                                counter++;
                                TxtProg.Content = prog + "Speaker: " + spkName + " :Incoming Command: " + recordedCommand + " | Command Recognised: " + result + " Index of Buffer Breaking: " + (i + 1);
                                System.Diagnostics.Debug.WriteLine("Speaker: " + spkName + " :Incoming Command: " + recordedCommand + " | Command Recognised: " + result + " Index of Buffer Breaking: " + (i + 1));
                            }
                        }
                    }
                }
            }
            //for (int i = 0; i < cDataList.Count; i++) {
            //    CommandData cData = cDataList[i];
            //for (int z = 0; z < vd.bufferList.Count; z++)
            //{
            //    var byteBuff = vd.bufferList.ToArray()[z];
            //    double[] dBuff = ByteToDouble(byteBuff);
            //    double[] coef = mfcc.ProcessToVector(dBuff);
            //    Dictionary<string, double> attributes = new Dictionary<string, double>();
            //    for (int k = 0; k < s.nrOfCoef; k++)
            //    {
            //        attributes.Add("IN-" + k, coef[k]);
            //    }
            //    string result = network.GetResult(attributes, true);
            //    if (!result.Equals(cData.tag))
            //    { //Om resultatet inte är samma som det det ska vara
            //        if (counter >= allowedMisses)
            //        {
            //            System.Diagnostics.Debug.WriteLine("Validation[" + attempt + "] failed! Speaker: " + spkName + " :Incoming Command: " + recordedCommand + " | Command Recognised: " + result + " Index of Buffer Breaking: " + z + "! Making a new attempt...");
            //            return false;
            //        }
            //        else
            //        {
            //            System.Diagnostics.Debug.WriteLine("Speaker: " + spkName + " :Incoming Command: " + recordedCommand + " | Command Recognised: " + result + " Index of Buffer Breaking: " + z);

            //            //System.Diagnostics.Debug.WriteLine(counter + " - Error: " + "Validation[" + attempt + "] failed with command: " + cData.command + " as it returned: " + result);
            //            counter++;
            //        }
            //    }

            //}
            //}
            TxtProg.Content = prog + " Validating was successfull! With: " + totalCount + " misses! Network Creation was completed!";
            System.Diagnostics.Debug.WriteLine("Validation was successfull! With: " + totalCount + " misses!");
            return true;
        }
        #endregion

        #region StackPanel
        private StackPanel CreateComPanel(StackPanel sp, int i, CommandData cData) {
            SolidColorBrush borderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF6ABE30");
            SolidColorBrush insideBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF51A482");
            SolidColorBrush insideBrush2 = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF7EBDA3");
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            FontFamily tnr = new FontFamily("Times New Roman");

            sp.Margin = new Thickness(0, 0, 0, 0);
            sp.Visibility = Visibility.Hidden;

            sp.Children.Add(new Border {
                Height = 40,
                Width = 580,
                Margin = new Thickness(0, 10, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = borderBrush,
                Background = insideBrush
            });

            sp.Children.Add(new Label {
                Content = i + 1,
                FontFamily = tnr,
                FontSize = 22,
                Foreground = whiteBrush,
                Margin = new Thickness(-530, -40, 0, 0),
                Width = 45,
                Height = 40,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            });

            sp.Children.Add(new Border {
                Height = 32,
                Width = 140,
                Margin = new Thickness(-350, -40, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = borderBrush,
                Background = insideBrush2
            });

            sp.Children.Add(new Label {
                Content = cData.command,
                FontFamily = tnr,
                FontSize = 18,
                Foreground = whiteBrush,
                Margin = new Thickness(-350, -40, 0, 0),
                Width = 140,
                Height = 40,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            });

            sp.Children.Add(new Border {
                Height = 32,
                Width = 180,
                Margin = new Thickness(-20, -40, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = borderBrush,
                Background = insideBrush2
            });

            sp.Children.Add(new Label {
                Content = cData.text,
                FontFamily = tnr,
                FontSize = 15,
                Foreground = whiteBrush,
                Margin = new Thickness(-20, -40, 0, 0),
                Width = 180,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            });

            sp.Children.Add(new Border {
                Height = 32,
                Width = 70,
                Margin = new Thickness(240, -40, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = borderBrush,
                Background = insideBrush2
            });

            sp.Children.Add(new Label {
                Content = cData.tag,
                FontFamily = tnr,
                FontSize = 15,
                Foreground = whiteBrush,
                Margin = new Thickness(240, -40, 0, 0),
                Width = 70,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            });

            MyButton btnEdit = new MyButton(i) {
                Width = 50,
                Height = 32,
                Margin = new Thickness(370, -40, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF5E6601"),
                Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF73833A"),
                Content = "Edit",
                FontFamily = tnr,
                FontSize = 20,
                Foreground = whiteBrush,
                FontWeight = FontWeights.Bold
            };
            btnEdit.Click += new RoutedEventHandler(BtnEdit_Click);
            sp.Children.Add(btnEdit);

            MyButton btnSelect = new MyButton(i) {
                Width = 55,
                Height = 32,
                Margin = new Thickness(485, -40, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFBF6600"),
                Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFBF6600"),
                Content = "Unselect",
                FontFamily = tnr,
                FontSize = 12,
                Foreground = whiteBrush,
            };
            btnSelect.Click += new RoutedEventHandler(BtnUnselect_Click);
            sp.Children.Add(btnSelect);

            MyButton btnDelete = new MyButton(i) {
                Width = 25,
                Height = 24,
                Margin = new Thickness(570, -40, 0, 0),
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF570000"),
                Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFBD5858"),
                Content = "D",
                FontFamily = tnr,
                FontSize = 12,
                Foreground = whiteBrush,
            };
            btnDelete.Click += new RoutedEventHandler(BtnDelete_Click);
            sp.Children.Add(btnDelete);

            return sp;
        }
        #endregion

        #region Buttons
        private void BtnCreateCommand_Click(object sender, RoutedEventArgs e) {
            string id = CreateId();
            //int commandCount = cDataList.Count;
            Hide();
            CreateCommandWindow ccW = new CreateCommandWindow(this, id, s.sampleRate); //Replace commandCount with ID
            ccW.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            MyButton btnEdit = (MyButton)sender;
            CommandData cData = cDataList[btnEdit.parentNr];
            //int commandCount = cData.id;

            Hide();
            CreateCommandWindow ccW = new CreateCommandWindow(this, cData, s.sampleRate);
            ccW.Show();

        }

        private void BtnUnselect_Click(object sender, RoutedEventArgs e) {
            MyButton btnSelect = (MyButton)sender;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            MyButton btnDelete = (MyButton)sender;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            if (cDataList.Count > 0) {
                if (path == "" || path == null) {
                    SaveAs();
                }
                else {
                    Save();
                }
            }
            else {
                System.Diagnostics.Debug.WriteLine("Nothing to Save!");
            }
        }

        private void BtnSaveAs_Click(object sender, RoutedEventArgs e) {
            SaveAs();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e) {
            Load();

        }

        private void BtnCreateNetwork_Click(object sender, RoutedEventArgs e) {
            if (isTesting) {
                TestANN();
            }
            else {
                network = CreateNetwork();
                BtnTestANN.IsEnabled = true;
            }
        }

        private void BtnTestNetwork_Click(object sender, RoutedEventArgs e) {
            if (isRec) {
                //double[] dBuff = RecordAudio.recAudio.StopRecD();
                //byte[] newBuff = EmptyRemoval(buffer);
                //short[] sBuffer = ByteToInt16(newBuff);
                ////double[] dBuff = ByteToDouble(buffer);
                //double[] coef = mfcc.ProcessToVector(dBuff);

                //NAudio.Wave.WaveBuffer waveBuffer = new NAudio.Wave.WaveBuffer(buffer);
                //float[] bufferFloat = ConvertByteToFloat(buffer);

                //Dictionary<string, double> attributes = new Dictionary<string, double>();
                //int count = 0;
                //foreach (double co in coef) {
                //    if (count < s.nrOfCoef) {
                //        attributes.Add("IN-" + count, co); //MarkusConvert(co) -- Currently the normalization is done before its sent back to main
                //        count++;
                //    }
                //    else {
                //        break;
                //    }
                //}
                //string result = network.GetResult(attributes, 0.6);
                //System.Diagnostics.Debug.WriteLine("");
                //System.Diagnostics.Debug.WriteLine("Result: " + result);
                //BtnTestANN.Content = "Test Network";
                isRec = false;
            }
            else {
                if (network != null) {
                    RecordAudio.recAudio.StartRec();
                    isRec = true;
                    BtnTestANN.Content = "Stop Recording";
                }
            }
        }


        private void Scroller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            double value = Scroller.Value;
            ScrollBoxes(value);
        }

        #endregion

        #region ANNProof
        private void TestANN() { //CreatesTestANN
            int outputs = 10; //Hur många outputs
            int testLength = 50; //Hur många gånger varje output har gjorts
            int values = 240; //Hur många variabler det ska vara (20 standard)
            testValues = new double[outputs * testLength][];

            Random rand = new Random();
            List<TrainingData> tDataList = new List<TrainingData>();
            tagList = new List<string>();

            for (int i = 0; i < outputs; i++) {
                double[] inputValues = new double[values];
                tagList.Add(i.ToString());
                for (int j = 0; j < values; j++) {
                    //inputValues[j] = rand.NextDouble();
                    inputValues[j] = (float)((-1) + (rand.NextDouble() * (1 - (-1))));
                }
                for (int j = 0; j < testLength; j++) {
                    testValues[i * testLength + j] = inputValues;
                    TrainingData tData = new TrainingData(i.ToString(), inputValues);
                    tDataList.Add(tData);
                }
            }
            dTuple = new DataTuple(tagList.Count, tDataList);
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            network = new Network(tagList, dTuple);
            timer.Stop();
            System.Diagnostics.Debug.WriteLine($"Network creation Time: {timer.ElapsedMilliseconds} ms. Trainingdata samples: " + tDataList.Count);
            VarTestANN(outputs, testLength, values);
        }

        private bool VarTestANN(int outputs, int testLength, int values) { //VarifyTestANN
            for (int i = 0; i < outputs; i++) {
                Dictionary<string, double> valueDir = new Dictionary<string, double>();
                for (int j = 0; j < values; j++) {
                    valueDir.Add(j.ToString(), testValues[i * testLength][j]);
                }
                string result = network.GetResult(valueDir, annAcc);
                if (!result.Equals(i.ToString())) {
                    System.Diagnostics.Debug.WriteLine("Validation failed at [" + i + "]! ANN is broken :(");
                    return false;
                }
            }
            System.Diagnostics.Debug.WriteLine("Validation is successfull!!! YAY");
            return true;
        }
        #endregion

        #region ToRemove
        //private double MarkusConvert(double coef) {
        //    int stepSize = 15000;
        //    int min = 110000;
        //    for (int i = 0; i < 40; i++) {
        //        if (coef <= min + (i * stepSize)) {
        //            double d = (double)i / 40;
        //            double co = d;
        //            return co;
        //        }
        //    }
        //    return 1;
        //}

        //private List<TrainingData> ANNProf() {
        //    int length = 20;
        //    int maxValue = 255;
        //    int recordings = 50;
        //    testValues = new double[commands][];
        //    Random rand = new Random();
        //    List<TrainingData> tDataList = new List<TrainingData>();
        //    tagList = new List<string>();

        //    for (int i = 0; i < commands; i++) {

        //        double[] test = new double[length];

        //        for (int j = 0; j < length; j++) {
        //            test[j] = rand.Next(0, maxValue);
        //        }
        //        testValues[i] = test;
        //        for (int j = 0; j < recordings; j++) {
        //            TrainingData tData = new TrainingData(i.ToString(), test);
        //            tDataList.Add(tData);
        //            tagList.Add(i.ToString());
        //        }
        //    }
        //    return tDataList;
        //}

        //private void ANNProof(int commandToTest) {
        //    CommandData cData = cDataList[commandToTest];
        //    System.Diagnostics.Debug.WriteLine("- Tag To Test[" + commandToTest + "]: " + cData.tag);
        //    Random rand = new Random();
        //    int randRecording = rand.Next(0, cData.voiceDataList.Count);
        //    VoiceData vd = cData.voiceDataList[randRecording];
        //    int randBuffer = rand.Next(0, vd.bufferList.Count);
        //    byte[] buffer = vd.bufferList[randBuffer];

        //    List<double> testMFCC = new List<double>(); //test
        //    testMFCC.Add(Convert.ToDouble(buffer));
        //    //waveBuffer = new NAudio.Wave.WaveBuffer(buffer); //removed for test
        //    //float[] bufferFloat = ConvertByteToFloat(buffer); //Fix 
        //    double[] doubleMFcc = testMFCC.ToArray();
        //    double[] coef = mFCC.ProcessToVector(doubleMFcc);
        //    Dictionary<string, double> attributes = new Dictionary<string, double>();
        //    int count = 0;
        //    System.Diagnostics.Debug.WriteLine("Coef values: ");
        //    foreach (double co in coef) {
        //        if (count < numberOfCoef) {
        //            attributes.Add("IN-" + count, MarkusConvert(co));
        //            System.Diagnostics.Debug.Write(MarkusConvert(co) + " ");
        //            count++;
        //        }
        //        else {
        //            break;
        //        }
        //    }
        //    string result = network.GetResult(attributes);
        //    System.Diagnostics.Debug.WriteLine("Result: " + result);
        //}
        #endregion

        private void BtnLoadANN_Click(object sender, RoutedEventArgs e) {
            LoadANN();
        }
        #region RemoveEmpty
        private byte[] EmptyRemoval(byte[] bufferInput) {
            int noiseStart = FrontRemoval(bufferInput);
            int firstEmpty = BackRemoval(bufferInput);

            if (firstEmpty != -1) {
                byte[] newInput = new byte[firstEmpty - noiseStart];
                for (int i = noiseStart; i < firstEmpty; i++) {
                    newInput[i - noiseStart] = bufferInput[i];
                }
                return newInput;
            }
            return bufferInput;
        }

        private int FrontRemoval(byte[] bufferInput) {
            int startPoint = 100; //Kan noise starta innan dess? Hitta en finare algorithm (Vill inte behöva göra undantag för de 6 första "klick" värderna)
            int emptyStartCont = 3;
            int noiseStartCont = 10;
            int noiseCounter = 0;
            int emptyCounter = 0;
            int noiseStart = 0;
            //int[] emptyNoises = new int[] { 0, 1, -1, -65535, 65535, -65536, 65536, 131071 };
            int[] emptyNoises = new int[] { 0, 1, 255 };

            for (int i = startPoint; i < bufferInput.Length; i++) {
                bool isEmpty = false;
                for (int j = 0; j < emptyNoises.Length; j++) {
                    if (bufferInput[i] == emptyNoises[j]) {
                        emptyCounter++;
                        isEmpty = true;
                        break;
                    }
                }
                if (!isEmpty) {
                    if (emptyCounter >= emptyStartCont) {
                        emptyCounter = 0;
                        noiseStart = i;
                        noiseCounter = 1; //Börjar om noiseCounter
                    }
                    else {
                        noiseCounter++;
                        if (noiseCounter >= noiseStartCont) {
                            return noiseStart;
                        }
                    }
                }
            }
            return noiseStart;
        }

        private int BackRemoval(byte[] bufferInput) {
            int startPoint = bufferInput.Length / 2;
            int counterStart = 100;
            int emptyCounter = 0;
            int firstEmpty = -1;


            for (int i = startPoint; i < bufferInput.Length; i++) {
                if (bufferInput[i] == 0) {
                    if (firstEmpty == -1) {
                        firstEmpty = i;
                    }
                    emptyCounter++;
                    if (emptyCounter >= counterStart) {
                        break;
                    }
                }
                else if (firstEmpty != -1) {
                    firstEmpty = -1;
                    emptyCounter = 0;
                }
            }
            return firstEmpty;
        }
        #endregion
    }
}
