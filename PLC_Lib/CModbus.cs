using System;
using System.Collections.Generic;
using System.Text;

namespace PLC_Lib
{
    internal class CModbus
    {
        // Fields
        private CTxRx TxRx;

        // Methods
        public CModbus(CTxRx Tx)
        {
            this.TxRx = Tx;
        }

        public Result ReadRegisters(byte unitId, ushort function, ushort address, ushort quantity, short[] registers, int offset)
        {
            if ((function < 1) || (function > 0x7f))
            {
                return Result.FUNCTION;
            }
            if ((quantity < 1) || (quantity > 0x7d))
            {
                return Result.QUANTITY;
            }
            if ((quantity + offset) > registers.GetLength(0))
            {
                return Result.QUANTITY;
            }
            byte[] tXBuf = new byte[8];
            byte[] rXBuf = new byte[0x105];
            tXBuf[0] = unitId;
            tXBuf[1] = (byte)(function & 0xff);
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte(quantity);
            tXBuf[5] = ByteAccess.LoByte(quantity);
            int responseLength = 3 + (quantity * 2);
            Result result = this.TxRx.TxRx(tXBuf, 6, rXBuf, responseLength);
            if (result == Result.SUCCESS)
            {
                if ((tXBuf[0] != rXBuf[0]) || (tXBuf[1] != rXBuf[1]))
                {
                    return Result.RESPONSE;
                }
                if ((quantity * 2) != rXBuf[2])
                {
                    return Result.BYTECOUNT;
                }
                for (int i = 0; i < quantity; i++)
                {
                    registers[i + offset] = (short)((rXBuf[(2 * i) + 4] & 0xff) | ((rXBuf[(2 * i) + 3] & 0xff) << 8));
                }
            }
            return result;
        }

        public Result WriteSingleRegister(byte unitId, ushort address, short register)
        {
            byte[] tXBuf = new byte[8];
            byte[] rXBuf = new byte[8];
            tXBuf[0] = unitId;
            tXBuf[1] = 6;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte((ushort)register);
            tXBuf[5] = ByteAccess.LoByte((ushort)register);
            Result rESPONSE = this.TxRx.TxRx(tXBuf, 6, rXBuf, 6);
            if ((rESPONSE == Result.SUCCESS) && (tXBuf[0] != 0))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (tXBuf[i] != rXBuf[i])
                    {
                        rESPONSE = Result.RESPONSE;
                    }
                }
            }
            return rESPONSE;
        }
    }
}
