using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDRSharp.Radio;
using System.Windows.Forms;

namespace SDRSharp.LimeSDR
{
    public class LimeSDRIO : IFrontendController, IIQStreamController, IDisposable, IFloatingConfigDialogProvider, ITunableSource, ISampleRateChangeSource
    {
        private long _frequency;
        private LimeSDRDevice _LimeDev = null;
        private readonly LimeSDRControllerDialog _gui;
        private SDRSharp.Radio.SamplesAvailableDelegate _callback;
        public event EventHandler SampleRateChanged;

        public LimeSDRIO()
        {
            _gui = new LimeSDRControllerDialog(this);
        }

        ~LimeSDRIO()
        {
            Dispose();
        }

        public LimeSDRDevice Device
        {
            get { return _LimeDev; }
        }

        public void Dispose()
        {
            Close();
            if (_gui != null)
            {
                _gui.Close();
                _gui.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        private unsafe void LimeDevice_SamplesAvailable(object sender, SamplesAvailableEventArgs e)
        {
            _callback(this, e.Buffer, e.Length);

        }

        public void Close()
        {

            if (_LimeDev == null)
                return;

            _LimeDev.SamplesAvailable -= LimeDevice_SamplesAvailable;
            _LimeDev.SampleRateChanged -= LimeSDRDevice_SampleRateChanged;
            _LimeDev.Dispose();
            _LimeDev = null;

        }

        private void LimeSDRDevice_SampleRateChanged(object sender, EventArgs e)
        {
            if (SampleRateChanged != null)
                SampleRateChanged(this, EventArgs.Empty);
        }

        public void Open()
        {

            _LimeDev = new LimeSDRDevice();
            _LimeDev.SamplesAvailable += LimeDevice_SamplesAvailable;
            _LimeDev.SampleRateChanged += LimeSDRDevice_SampleRateChanged;
        }


        public void Start(Radio.SamplesAvailableDelegate callback)
        {

            if (this._LimeDev == null)
                throw new ApplicationException("No device selected");


            _callback = callback;

            try
            {
                _LimeDev.Start();
            }
            catch
            {
                this.Open();
                _LimeDev.Start();
            }

        }



        public void Stop()
        {

            if (_LimeDev != null)
            {
                _LimeDev.Stop();
            }

        }

        public bool IsSoundCardBased
        {
            get
            {
                return false;
            }
        }

        public string SoundCardHint
        {
            get
            {
                return string.Empty;
            }
        }

        public void ShowSettingGUI(IWin32Window parent)
        {
            if (this._gui.IsDisposed)
                return;
            _gui.Show();
            _gui.Activate();

        }

        public void HideSettingGUI()
        {
            if (_gui.IsDisposed)
                return;
            _gui.Hide();
        }

        public long Frequency
        {
            get
            {
                if (this._LimeDev != null)
                {
                    _frequency = this._LimeDev.Frequency;
                }
                return _frequency;
            }
            set
            {
                if (this._LimeDev != null)
                {
                    this._LimeDev.Frequency = value;
                    this._frequency = value;
                }
                _frequency = value;

            }
        }

        public double Samplerate
        {

            get
            {
                if (_LimeDev != null)
                    return _LimeDev.SampleRate;
                else
                    return 0.0;
            }

        }

        public bool CanTune
        {
            get
            { return true; }
        }

        public long MaximumTunableFrequency
        {

            get { return 3800000000L; }
        }

        public long MinimumTunableFrequency
        {

            get
            { return 1000; }
        }
    }
}
