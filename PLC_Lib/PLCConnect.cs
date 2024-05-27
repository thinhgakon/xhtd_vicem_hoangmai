using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace PLC_Lib
{
    public class PLCConnect
    {
        // Fields
        private int _ConnectTimeout;
        private Mode _Mode;
        private int _ResponseTimeout;
        private string Error;
        //System
        private TcpClient client;
        private UdpClient udpClient;
        //Customer
        private CModbus Modbus;
        private Result Res;
        private CTxRx TxRx;

        // Methods
        public PLCConnect()
        {
            this._ResponseTimeout = 0x3e8;
            this._ConnectTimeout = 0x3e8;
            this.Error = "";
            this.Modbus = new CModbus(this.TxRx = new CTxRx());
            this.TxRx.Mode = Mode.TCP_IP;
            this.TxRx.connected = false;
        }

        public void Close()
        {
            if (this.client != null)
            {
                this.client.Close();
            }
            if (this.udpClient != null)
            {
                this.udpClient.Close();
            }
            if (this.TxRx != null)
            {
                this.TxRx.connected = false;
            }
        }

        public Result Connect(string ipAddress, int port)
        {
            this.Close();
            switch (this._Mode)
            {
                case Mode.TCP_IP:
                case Mode.RTU_OVER_TCP_IP:
                case Mode.ASCII_OVER_TCP_IP:
                    this.client = new TcpClient();
                    this.TxRx.SetClient(this.client);
                    this.TxRx.Timeout = this._ResponseTimeout;
                    this.client.SendTimeout = 0x7d0;
                    this.client.ReceiveTimeout = this._ResponseTimeout;
                    try
                    {
                        IAsyncResult asyncResult = this.client.BeginConnect(ipAddress, port, null, null);
                        WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                        if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds((double)this._ConnectTimeout), false))
                        {
                            Result result2;
                            this.client.Close();
                            this.Res = result2 = Result.CONNECT_TIMEOUT;
                            return result2;
                        }
                        this.client.EndConnect(asyncResult);
                        asyncWaitHandle.Close();
                    }
                    catch (Exception exception)
                    {
                        this.Error = exception.Message;
                        return (this.Res = Result.CONNECT_ERROR);
                    }
                    this.TxRx.Mode = this._Mode;
                    this.TxRx.connected = true;
                    break;

                case Mode.UDP_IP:
                    {
                        this.udpClient = new UdpClient();
                        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                        try
                        {
                            this.udpClient.Connect(endPoint);
                        }
                        catch (Exception exception2)
                        {
                            this.Error = exception2.Message;
                            return (this.Res = Result.CONNECT_ERROR);
                        }
                        this.TxRx.SetClient(this.udpClient);
                        this.TxRx.Timeout = this._ResponseTimeout;
                        this.udpClient.Client.SendTimeout = 0x7d0;
                        this.udpClient.Client.ReceiveTimeout = this._ResponseTimeout;
                        this.TxRx.Mode = this._Mode;
                        this.TxRx.connected = true;
                        break;
                    }
            }
            return (this.Res = Result.SUCCESS);
        }

        public string GetLastErrorString()
        {
            switch (this.Res)
            {
                case Result.SUCCESS:
                    return "Success";

                case Result.ILLEGAL_FUNCTION:
                    return "Illegal function.";

                case Result.ILLEGAL_DATA_ADDRESS:
                    return "Illegal data address.";

                case Result.ILLEGAL_DATA_VALUE:
                    return "Illegal data value.";

                case Result.SLAVE_DEVICE_FAILURE:
                    return "Slave device failure.";

                case Result.ACKNOWLEDGE:
                    return "Acknowledge.";

                case Result.SLAVE_DEVICE_BUSY:
                    return "Slave device busy.";

                case Result.NEGATIVE_ACKNOWLEDGE:
                    return "Negative acknowledge.";

                case Result.MEMORY_PARITY_ERROR:
                    return "Memory parity error.";

                case Result.CONNECT_ERROR:
                    return this.Error;

                case Result.CONNECT_TIMEOUT:
                    return "Could not connect within the specified time";

                case Result.WRITE:
                    return ("Write error. " + this.TxRx.GetErrorMessage());

                case Result.READ:
                    return ("Read error. " + this.TxRx.GetErrorMessage());

                case Result.RESPONSE_TIMEOUT:
                    return "Response timeout.";

                case Result.ISCLOSED:
                    return "Connection is closed.";

                case Result.CRC:
                    return "CRC Error.";

                case Result.RESPONSE:
                    return "Not the expected response received.";

                case Result.BYTECOUNT:
                    return "Byte count error.";

                case Result.QUANTITY:
                    return "Quantity is out of range.";

                case Result.FUNCTION:
                    return "Modbus function code out of range. 1 - 127.";

                case Result.DEMO_TIMEOUT:
                    return "Demo mode expired. Restart your application to continue.";
            }
            return ("Unknown Error - " + this.Res.ToString());
        }

        private Result ReadHoldingRegisters(byte unitId, ushort address, ushort quantity, short[] registers)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, 3, address, quantity, registers, 0));
        }
        /// <summary>
        /// Ham nay dung de kiem tra trang thai cac cong dau vao cua PLC.
        /// </summary>
        /// <param name="PortsValue">Mang luu tru gia tri cua tung cong.</param>
        /// <returns>Ket qua cua viec goi ham.</returns>
        public Result CheckInputPorts(bool[] PortsValue)
        {
            if (PortsValue.Length < 0 || PortsValue.Length > 24)
            {
                return Result.QUANTITY;
            }
            Int16[] Registers = new Int16[110];
            Result ReadResult;
            ReadResult = this.ReadHoldingRegisters(1, 0, 110, Registers);
            if (ReadResult == Result.SUCCESS)
            {
                Int16 Reg_9 = Registers[9];
                for (int i = 0; i < PortsValue.Length; i++)
                {
                    PortsValue[i] = (bool)(((Reg_9 >> i) & 1) == 1);
                }
            }
            return ReadResult;
        }

        public Result CheckOutputPorts(bool[] PortsValue)
        {
            if (PortsValue.Length < 0 || PortsValue.Length > 50)
            {
                return Result.QUANTITY;
            }
            Int16[] Registers = new Int16[110];
            Result ReadResult;
            ReadResult = this.ReadHoldingRegisters(1, 0, 110, Registers);
            if (ReadResult == Result.SUCCESS)
            {
                Int16 Reg_109 = Registers[109];
                for (int i = 0; i < PortsValue.Length; i++)
                {
                    PortsValue[i] = (bool)(((Reg_109 >> i) & 1) == 1);
                }
            }
            return ReadResult;
        }

        private Result WriteSingleRegister(byte unitId, ushort address, short register)
        {
            return (this.Res = this.Modbus.WriteSingleRegister(unitId, address, register));
        }

        public Result ShuttleOutputPort(byte Q)
        {
            if (Q < 0 || Q > 15)
            {
                return Result.QUANTITY;
            }
            Int16[] Registers = new Int16[110];
            Result ActResult;
            ActResult = this.ReadHoldingRegisters(1, 0, 110, Registers);
            if (ActResult == Result.SUCCESS)
            {
                Int16 Reg_109 = Registers[109];
                if (((Reg_109 >> Q) & 1) == 0)
                {
                    Reg_109 = (Int16)(Reg_109 | (1 << Q));
                }
                else
                {
                    Reg_109 = (Int16)(Reg_109 & (~(1 << Q)));
                }

                ActResult = this.WriteSingleRegister(1, 109, Reg_109);
            }
            return ActResult;
        }

        /// <summary>
        ///Properties
        ///["Max time to wait for connection 100 - 30000ms.")]
        /// </summary>
        public int ConnectTimeout
        {
            get
            {
                return this._ConnectTimeout;
            }
            set
            {
                if ((value >= 100) && (value <= 0x7530))
                {
                    this._ConnectTimeout = value;
                }
            }
        }

        /// <summary>
        /// Select which protocol mode to use.
        /// </summary>
        public Mode Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                this._Mode = value;
            }
        }
        /// <summary>
        /// Max time to wait for response 100 - 30000ms.
        /// </summary>
        public int ResponseTimeout
        {
            get
            {
                return this._ResponseTimeout;
            }
            set
            {
                if ((value >= 100) && (value <= 0x7530))
                {
                    this._ResponseTimeout = value;
                }
            }
        }
    }
}
