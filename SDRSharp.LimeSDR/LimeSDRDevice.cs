using System;
using SDRSharp.Radio;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDRSharp.LimeSDR
{

    public class LimeSDRDevice
    {
        private const int DefaultSamplerate = 2000000;
        private const uint DefaultFrequency = 101700000;
        private const uint SampleTimeoutMs = 1000;

        private IntPtr _device = IntPtr.Zero;
        IntPtr _stream = IntPtr.Zero;
        private GCHandle _gcHandle;
        private bool _isStreaming;
        private Thread _sampleThread = null;
        private uint _channel = 0;
        private uint _ant = 2;

        private static uint _readLength = 5000;
        private UnsafeBuffer _iqBuffer;
        private unsafe Complex* _iqPtr;
        private UnsafeBuffer _samplesBuffer;
        private unsafe float* _samplesPtr;
        private readonly SamplesAvailableEventArgs _eventArgs = new SamplesAvailableEventArgs();

        private double _sampleRate = DefaultSamplerate;
        private double _centerFrequency = DefaultFrequency;
        private uint _gain = 40;

        public const bool LMS_CH_TX = true;
        public const bool LMS_CH_RX = false;

        [method: CompilerGenerated]
        [CompilerGenerated]
        public event SamplesAvailableDelegate SamplesAvailable;

        private unsafe void ReceiveSamples_sync()
        {
            while (_isStreaming)
            {

                if (_iqBuffer == null || _iqBuffer.Length != _readLength)
                {
                    _iqBuffer = UnsafeBuffer.Create((int)_readLength, sizeof(Complex));
                    _iqPtr = (Complex*)_iqBuffer;
                }

                if (_samplesBuffer == null || _samplesBuffer.Length != (2 * _readLength))
                {
                    _samplesBuffer = UnsafeBuffer.Create((int)(2 * _readLength), sizeof(float));
                    _samplesPtr = (float*)_samplesBuffer;
                }

                NativeMethods.LMS_RecvStream(_stream, _samplesPtr, _readLength, IntPtr.Zero, SampleTimeoutMs);

                var ptrIq = _iqPtr;

                for (int i = 0; i < _readLength; i++)
                {
                    ptrIq->Real = _samplesPtr[i * 2];
                    ptrIq->Imag = _samplesPtr[i * 2 + 1];
                    ptrIq++;
                }

                ComplexSamplesAvailable(_iqPtr, _iqBuffer.Length);


            }
        }

        public bool IsStreaming
        {
            get
            {
                return _isStreaming;
            }
        }

        public LimeSDRDevice()
        {

            if (NativeMethods.LMS_GetDeviceList(null) < 1)
                throw new ApplicationException("Cannot found LimeSDR device. Is the device locked somewhere?");

            _gcHandle = GCHandle.Alloc(this);

        }

        ~LimeSDRDevice()
        {
            Dispose();
        }

        private unsafe void ComplexSamplesAvailable(Complex* buffer, int length)
        {
            if (SamplesAvailable != null)
            {
                _eventArgs.Buffer = buffer;
                _eventArgs.Length = length;
                SamplesAvailable(this, _eventArgs);
            }
        }

        public void Stop()
        {
            if (!_isStreaming)
                return;

            _isStreaming = false;
            if (_sampleThread != null)
            {

                if (_sampleThread.ThreadState == ThreadState.Running)
                    _sampleThread.Join();
                _sampleThread = null;
            }

            NativeMethods.LMS_StopStream(_stream);

        }

        public void Dispose()
        {
            this.Stop();
            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
            GC.SuppressFinalize(this);

            NativeMethods.LMS_Close(_device);
            _device = IntPtr.Zero;
        }

        public unsafe void Start()
        {
            if (NativeMethods.LMS_Open(out _device, null, null) != 0)
            {
                throw new ApplicationException("Cannot open LimeSDR device. Is the device locked somewhere?");
            }

            NativeMethods.LMS_Init(_device);

            if (NativeMethods.LMS_EnableChannel(_device, LMS_CH_RX, _channel, true) != 0)
            {
                throw new ApplicationException(NativeMethods.limesdr_strerror());
            }


            if (NativeMethods.LMS_SetAntenna(_device, LMS_CH_RX, _channel, _ant) != 0)
            {
                throw new ApplicationException(NativeMethods.limesdr_strerror());
            }

            this.SampleRate = _sampleRate;
            this.Frequency = (long)_centerFrequency;


            if (NativeMethods.LMS_SetGaindB(_device, LMS_CH_RX, _channel, _gain) != 0)
            {
                throw new ApplicationException(NativeMethods.limesdr_strerror());
            }


            lms_stream_t streamId = new lms_stream_t();
            streamId.handle = 0;
            streamId.channel = _channel; //channel number
            streamId.fifoSize = 128 * 1024; //fifo size in samples
            streamId.throughputVsLatency = 1.0f; //optimize for max throughput
            streamId.isTx = false; //RX channel
            streamId.dataFmt = dataFmt.LMS_FMT_F32;
            _stream = Marshal.AllocHGlobal(Marshal.SizeOf(streamId));

            Marshal.StructureToPtr(streamId, _stream, false);
            if (NativeMethods.LMS_SetupStream(_device, _stream) != 0)
            {
                throw new ApplicationException(NativeMethods.limesdr_strerror());
            }

            if (NativeMethods.LMS_StartStream(_stream) != 0)
            {
                throw new ApplicationException(NativeMethods.limesdr_strerror());
            }

            _sampleThread = new Thread(ReceiveSamples_sync);
            _sampleThread.Name = "limesdr_samples_rx";
            _sampleThread.Priority = ThreadPriority.Highest;
            _isStreaming = true;
            _sampleThread.Start();

        }


        public long Frequency
        {

            get
            {
                if (_device != IntPtr.Zero)
                {
                    if (NativeMethods.LMS_GetLOFrequency(_device, LMS_CH_RX, _channel, ref _centerFrequency) != 0)
                    {
                        throw new ApplicationException(NativeMethods.limesdr_strerror());
                    }
                }
                return (long)_centerFrequency;
            }
            set
            {
                _centerFrequency = value;

                if (_device != IntPtr.Zero)
                {

                    if (NativeMethods.LMS_SetLOFrequency(_device, LMS_CH_RX, _channel, _centerFrequency) != 0)
                    {

                        throw new ApplicationException(NativeMethods.limesdr_strerror());
                    }


                    if (_centerFrequency <= 30e6)
                    {
                        if (_isStreaming)
                        {
                            NativeMethods.LMS_StopStream(_stream);
                            NativeMethods.LMS_StartStream(_stream);
                        }
                    }


                }

            }

        }

        public event EventHandler SampleRateChanged;

        public void OnSampleRateChanged()
        {
            if (SampleRateChanged != null)
                SampleRateChanged(this, EventArgs.Empty);
        }

        public uint Channel
        {
            get
            {

                return _channel;
            }
            set
            {

                _channel = value;

            }

        }

        public uint Antenna
        {
            get
            {

                return _ant;
            }
            set
            {

                _ant = value;

            }

        }

        public double SampleRate
        {

            get
            {

                return _sampleRate;
            }


            set
            {
                _sampleRate = value;



                if (_device != IntPtr.Zero)
                {

                    if (NativeMethods.LMS_SetSampleRate(_device, _sampleRate, 0) != 0)
                    {
                        throw new ApplicationException(NativeMethods.limesdr_strerror());
                    }

                    if (_isStreaming)
                    {
                        NativeMethods.LMS_StopStream(_stream);
                        NativeMethods.LMS_StartStream(_stream);
                    }

                }

                OnSampleRateChanged();

            }

        }

        public double Gain
        {

            get
            {

                return _gain;
            }

            set
            {
                _gain = (uint)value;
                if (_device != IntPtr.Zero)
                {
                    if (NativeMethods.LMS_SetGaindB(_device, LMS_CH_RX, _channel, _gain) != 0)
                    {
                        throw new ApplicationException(NativeMethods.limesdr_strerror());
                    }

                }
            }
        }

    }



    public sealed class SamplesAvailableEventArgs : EventArgs
    {
        public int Length { get; set; }
        public unsafe Complex* Buffer { get; set; }
    }

    public delegate void SamplesAvailableDelegate(object sender, SamplesAvailableEventArgs e);

}
