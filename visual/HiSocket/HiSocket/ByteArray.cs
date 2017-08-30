﻿//****************************************************************************
// Description:
// Author: hiramtan@qq.com
//***************************************************************************

using System;
using System.Collections.Generic;

namespace HiSocket
{
    internal class ByteArray : IByteArray
    {
        private List<byte> _bytes = new List<byte>();
        private readonly object _locker = new object();
        public int Length
        {
            get { return _bytes.Count; }
        }

        public byte[] Read(int length)
        {
            lock (_locker)
            {
                if (length > this._bytes.Count)
                {
                    throw new Exception("length>_bytes's length");
                }
                byte[] bytes = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    bytes[i] = this._bytes[i];
                }
                this._bytes.RemoveRange(0, length);
                return bytes;
            }
        }

        public void Write(byte[] bytes, int length)
        {
            lock (_locker)
            {
                if (length > bytes.Length)
                {
                    throw new Exception("length>_bytes's length");
                }
                for (int i = 0; i < length; i++)
                {
                    this._bytes.Add(bytes[i]);
                }
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _bytes.Clear();
            }
        }
    }
}


