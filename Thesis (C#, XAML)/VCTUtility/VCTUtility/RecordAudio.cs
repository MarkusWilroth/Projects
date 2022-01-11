//using NAudio.Wave;
//using System;
//using System.IO;

//namespace VCTUtility {
//    public class RecordAudio {
//        #region NAudio 2.0.1
//        //public WaveIn waveSource = null;
//        //public WaveOut waveOut = null;

//        //private IWaveIn waveIn;
//        private WaveIn waveIn;
//        private WaveOut waveOut;
//        public WaveFileWriter writer = null;
//        private MemoryStream mStream;
//        private int sampleRate;
//        private int channel = 1;
//        private bool isCreated = false;

//        public RecordAudio(int sampleRate) {
//            this.sampleRate = sampleRate;
//        }

//        public void StartRec() {
//            CreateReader();
//            waveIn.StartRecording();
//        }

//        private void CreateReader() {
//            mStream = new MemoryStream();
//            waveIn = new WaveIn();
//            waveOut = new WaveOut();

//            waveIn.WaveFormat = new WaveFormat(sampleRate, channel);
//            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(WaveSource_DataAvailable);
//            waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(WaveSource_RecordingStopped);

//            writer = new WaveFileWriter(mStream, waveIn.WaveFormat);
//            isCreated = true;
//        }

//        public byte[] StopRecB() {
//            waveIn.StopRecording();
//            byte[] buffer = mStream.GetBuffer();
//            PlayAudio(buffer);
//            return buffer;
//        }

//        public double[] StopRecD() {
//            waveIn.StopRecording();
//            byte[] buffer = mStream.GetBuffer();
//            double[] dBuffer = ByteToDouble(buffer);
//            PlayAudio(buffer);
//            return dBuffer;
//        }

//        public void PlayAudio(byte[] buffer) {
//            if (!isCreated) {
//                CreateReader();
//            }

//            MemoryStream pStream = new MemoryStream(buffer);
//            WaveFileReader reader = new WaveFileReader(pStream);
//            waveOut.Init(reader);
//            waveOut.Play();

//        }

//        private void WaveSource_DataAvailable(object sender, WaveInEventArgs e) {
//            if (writer != null) {
//                writer.Write(e.Buffer, 0, e.BytesRecorded);
//                writer.Flush();
//            }
//        }

//        private void WaveSource_RecordingStopped(object sender, StoppedEventArgs e) {
//            if (waveIn != null) {
//                waveIn.Dispose();
//                waveIn = null;
//            }

//            if (writer != null) {
//                writer.Dispose();
//                writer = null;
//            }
//        }
//        private double[] ByteToDouble(byte[] buffer) {
//            double[] values = new double[buffer.Length / 8];

//            for (int i = 0; i < values.Length; i++) {
//                values[i] = BitConverter.ToInt32(buffer, i * 8);
//            }
//            return values;
//        }
//        #endregion

//        AsioOut asioOut;
//        private void CreateAsioDevice() {
//            if (asioOut == null) {
//                asioOut = new AsioOut();
//                asioOut.InitRecordAndPlayback(null, channel, sampleRate);
//                asioOut.AudioAvailable += OnAsioOutAudioAvailable;
//            }
//            mStream = new MemoryStream();
//            writer = new WaveFileWriter(mStream, new WaveFormat(sampleRate, channel));

//        }
//        private void Cleanup() {
//            if (asioOut != null) {
//                asioOut.Dispose();
//                asioOut = null;
//            }
//            if (writer != null) {
//                writer.Dispose();
//                writer = null;
//            }
//        }

//        void OnAsioOutAudioAvailable(object sender, AsioAudioAvailableEventArgs e) {
//            var samples = e.GetAsInterleavedSamples();
//            writer.WriteSamples(samples, 0, samples.Length);
//        }

//        public void StartRecAsio() {
//            CreateAsioDevice();
//            asioOut.Play();
//        }
//        public byte[] StopRecAsio() {
//            asioOut.Stop();
//            if (writer != null) {
//                writer.Dispose();
//                writer = null;
//            }
//            byte[] buffer = mStream.GetBuffer();
//            return buffer;
//        }
//    }
//}
