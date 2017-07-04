using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SDRSharp.LimeSDR
{
    public partial class LimeSDRControllerDialog : Form
    {
        private readonly LimeSDRIO _owner;
        private bool _initialized;

        public LimeSDRControllerDialog(LimeSDRIO owner)
        {
            InitializeComponent();
            _owner = owner;
            InitSampleRates();
            
            _initialized = true;

        }

        private bool Initialized
        {
            get
            {
                return _initialized && _owner.Device != null;
            }
        }

        private void InitSampleRates()
        {

              for (int i = 1; i < 61; i++)
                samplerateComboBox.Items.Add(String.Format("{0} MSPS", i));
        }

        private void samplerateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Initialized)
            {
                return;
            }

            try
            {
                _owner.Device.SampleRate = (UInt32)((samplerateComboBox.SelectedIndex+1)* 1e6);
            }
            catch
            {
                _owner.Device.SampleRate = 2e6;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void LimeSDRControllerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void LimeSDRControllerDialog_Activated(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            if (_owner.Device.IsStreaming)
            {
                samplerateComboBox.Enabled = false;

                rx0.Enabled = false;
                rx1.Enabled = false;
                ant_h.Enabled = false;
                ant_l.Enabled = false;
                ant_w.Enabled = false;
            }
            else
            {
                samplerateComboBox.Enabled = true;

                rx0.Enabled = true;
                rx1.Enabled = true;
                ant_h.Enabled = true;
                ant_l.Enabled = true;
                ant_w.Enabled = true;
            }

            samplerateComboBox.SelectedIndex = (Int32)(_owner.Device.SampleRate / 1e6 - 1);

            if (_owner.Device.Channel == 0)
            {
                rx0.Checked = true;
            }
            else {
                rx1.Checked = true;
            }

            if (_owner.Device.Antenna == 1)
            {
                ant_h.Checked = true;
            }
            else if (_owner.Device.Antenna == 2)
            {
                ant_l.Checked = true;
            }
            else {
                ant_w.Checked = true;
            }


            gainBar.Value = (int)_owner.Device.Gain;
            gainDB.Text = gainBar.Value + " dB";

        }

        private void gainBar_Scroll(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.Gain = gainBar.Value;
            gainDB.Text = gainBar.Value + " dB";

        }

        private void rx0_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.Channel = 0;

        }

        private void rx1_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.Channel = 1;

        }

        private void ant_h_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.Antenna = 1;
        }

        private void ant_l_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.Antenna = 2;
        }

        private void ant_w_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.Antenna = 3;
        }
    }

    



}
