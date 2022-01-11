using System;
using System.IO;
using NAudio.Wave;

namespace ANN_Test {
    public class RecordAudio {

        public static RecordAudio recAudio;

        private WaveIn waveIn;
        private WaveOut waveOut;
        public WaveFileWriter writer = null;
        private MemoryStream mStream;
        private int sampleRate;
        private int channel = 1;
        private bool isCreated = false;

        public RecordAudio(int sampleRate) {
            this.sampleRate = sampleRate;
        }

        public void StartRec() {
            CreateReader();
            waveIn.StartRecording();
        }

        private void CreateReader() {
            mStream = new MemoryStream();
            waveIn = new WaveIn();
            waveOut = new WaveOut();

            waveIn.WaveFormat = new WaveFormat(sampleRate, channel);
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(WaveSource_DataAvailable);
            waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(WaveSource_RecordingStopped);

            writer = new WaveFileWriter(mStream, waveIn.WaveFormat);
        }

        public byte[] StopRecB() {
            waveIn.StopRecording();
            byte[] buffer = mStream.GetBuffer();
            PlayAudio(buffer);
            return buffer;
        }

        public double[] StopRecD() {
            waveIn.StopRecording();
            byte[] buffer = mStream.GetBuffer();
            double[] dBuffer = ByteToDouble(buffer);
            
            return dBuffer;
        }

        public void Dispose() {
            if (waveOut != null) {
                waveOut.Dispose();
                waveOut = null;
            }
            if (waveIn != null) {
                waveIn.Dispose();
                waveIn = null;
            }
            if (writer != null) {
                writer.Dispose();
                writer = null;
            }
        }

        public void PlayAudio(byte[] buffer) {
            if (waveOut == null) {
                CreateReader();
            }

            MemoryStream pStream = new MemoryStream(buffer);
            WaveFileReader reader = new WaveFileReader(pStream);
            waveOut.Init(reader);
            waveOut.Play();
            //waveOut.Dispose();
            waveOut = null;
        }

        private void WaveSource_DataAvailable(object sender, WaveInEventArgs e) {
            if (writer != null) {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                writer.Flush();
            }
        }

        private void WaveSource_RecordingStopped(object sender, StoppedEventArgs e) {
            if (waveIn != null) {
                waveIn.Dispose();
                waveIn = null;
            }

            if (writer != null) {
                //writer.Flush();
                //writer.Close();
                writer.Dispose();
                writer = null;
            }
            if (waveOut != null) {
                waveOut.Dispose();
                waveOut = null;
            }
        }
        private double[] ByteToDouble(byte[] buffer) {
            double[] values = new double[buffer.Length / 8];

            for (int i = 0; i < values.Length; i++) {
                values[i] = BitConverter.ToInt32(buffer, i * 8);
            }
            return values;
        }
    }
}
