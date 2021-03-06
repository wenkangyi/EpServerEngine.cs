﻿/*! 
@file PacketSerializer.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/epserverengine.cs>
@date April 01, 2014
@brief PacketSerializer Interface
@version 2.0

@section LICENSE

The MIT License (MIT)

Copyright (c) 2014 Woong Gyu La <juhgiyo@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

@section DESCRIPTION

A PacketSerializer Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using EpLibrary.cs;

namespace EpServerEngine.cs
{
    
    /// <summary>
    /// Packet Serializer
    /// </summary>
    /// <typeparam name="PacketStruct">packet class type</typeparam>
    public sealed class PacketSerializer<PacketStruct> where PacketStruct : class
    {

        /// <summary>
        /// stream
        /// </summary>
        private MemoryStream m_stream = null;
        
        /// <summary>
        /// lock
        /// </summary>
        private Object m_packetContainerLock = new Object();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="packet">packet class object</param>
        public PacketSerializer(PacketStruct packet=null)
        {
            
            m_stream = new MemoryStream();
            SilverlightSerializer.Serialize(packet,m_stream);      
        }

        /// <summary>
        /// Default  constructor
        /// </summary>
        /// <param name="rawData">serialized packet</param>
        public PacketSerializer(byte[] rawData)
        {
            m_stream = new MemoryStream(rawData);
        }

        /// <summary>
        /// Default  constructor
        /// </summary>
        /// <param name="rawData">serialized packet</param>
        /// <param name="offset">rawData offset</param>
        /// <param name="count">rawData byte size</param>
        public PacketSerializer(byte[] rawData,int offset,int count)
        {
            m_stream = new MemoryStream(rawData, offset, count);
        }

        /// <summary>
        /// Default copy constructor
        /// </summary>
        /// <param name="orig">the object to copy from</param>
        public PacketSerializer(PacketSerializer<PacketStruct> orig)
        {
            m_stream=orig.m_stream;
        }


        /// <summary>
        /// Get packet class object
        /// </summary>
        /// <returns>packet class object</returns>
        public PacketStruct GetPacket()
        {
            m_stream.Seek(0, SeekOrigin.Begin);
            return (PacketStruct)SilverlightSerializer.Deserialize(m_stream);
        }

        /// <summary>
        /// Get serialized packet
        /// </summary>
        /// <returns>serialized packet</returns>
        public byte[] GetPacketRaw()
        {
            //return m_stream.ToArray();
            return m_stream.GetBuffer();

        }
        /// <summary>
        /// Return size of packet in byte
        /// </summary>
        /// <returns>size of packet in byte</returns>
        public long GetPacketByteSize()
        {
            return m_stream.Length;   
        }

        /// <summary>
        /// Set packet class object
        /// </summary>
        /// <param name="packet">packet class object</param>
        public void SetPacket(PacketStruct packet)
        {
            m_stream = new MemoryStream();
            SilverlightSerializer.Serialize(packet, m_stream);      
        }

        /// <summary>
        /// Set serialize packet
        /// </summary>
        /// <param name="rawData">serialize packet</param>
        public void SetPacket(byte[] rawData)
        {
            m_stream = new MemoryStream(rawData);

        }

        /// <summary>
        /// Set serialize packet
        /// </summary>
        /// <param name="rawData">serialize packet</param>
        /// <param name="offset">rawData offset</param>
        /// <param name="count">rawData byte size</param>
        public void SetPacket(byte[] rawData, int offset,int count)
        {
            m_stream = new MemoryStream(rawData, offset, count);

        }

    }
}
