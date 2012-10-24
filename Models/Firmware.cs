﻿/*
 * Copyright 2011, Rowe Technology Inc. 
 * All rights reserved.
 * http://www.rowetechinc.com
 * https://github.com/rowetechinc
 * 
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 * 
 *  1. Redistributions of source code must retain the above copyright notice, this list of
 *      conditions and the following disclaimer.
 *      
 *  2. Redistributions in binary form must reproduce the above copyright notice, this list
 *      of conditions and the following disclaimer in the documentation and/or other materials
 *      provided with the distribution.
 *      
 *  THIS SOFTWARE IS PROVIDED BY Rowe Technology Inc. ''AS IS'' AND ANY EXPRESS OR IMPLIED 
 *  WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 *  FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> OR
 *  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 *  CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 *  SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
 *  ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 *  ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *  
 * The views and conclusions contained in the software and documentation are those of the
 * authors and should not be interpreted as representing official policies, either expressed
 * or implied, of Rowe Technology Inc.
 * 
 * HISTORY
 * -----------------------------------------------------------------
 * Date            Initials    Version    Comments
 * -----------------------------------------------------------------
 * 01/20/2012      RC          1.14       Initial coding
 * 01/26/2012      RC          1.14       Changed constructor to take a byte instead of string for subsystem.
 * 07/20/2012      Rc          2.12       Added FirmwareVersionList() to get a list of all possible firmware major, minor and revision values.
 * 10/09/2012      RC          2.15       Changed SubsystemIndex to SubsystemCode.
 *
 */

using System;
using System.Collections.Generic;
namespace RTI
{
    /// <summary>
    /// A class to describe the system firmware version.
    /// This will include the hardware sub-system, the 
    /// major and minor firmware version and the 
    /// firmware revision.
    /// </summary>
    public class Firmware
    {

        #region Variables

        /// <summary>
        /// Number of bytes in the Firmware.
        /// </summary>
        public const int NUM_BYTES = 4;

        /// <summary>
        /// Location of the Hardware sub-system.
        /// </summary>
        private const int SUBSYSTEM_START = 3;

        /// <summary>
        /// Location of the Major Firmware version.
        /// </summary>
        private const int MAJOR_START = 2;

        /// <summary>
        /// Location of the Minor Firmware version.
        /// </summary>
        private const int MINOR_START = 1;

        /// <summary>
        /// Location of the Firmware revsion.
        /// </summary>
        private const int REVISION_START = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Subsystem Code.  This represent the
        /// system type.
        /// </summary>
        public byte SubsystemCode { get; set; }

        /// <summary>
        /// Major Firmware version.
        /// This is an unsigned int between 0 and 255.
        /// </summary>
        public UInt16 FirmwareMajor { get; set; }

        /// <summary>
        /// Minor Firmware version.
        /// This is an unsigned int between 0 and 255.
        /// </summary>
        public UInt16 FirmwareMinor { get; set; }

        /// <summary>
        /// Firmware revision.
        /// This is an unsigned int between 0 and 255.
        /// </summary>
        public UInt16 FirmwareRevision { get; set; }

        #endregion


        /// <summary>
        /// Set the values to nothing.
        /// </summary>
        public Firmware()
        {
            FirmwareMajor = 0;
            FirmwareMinor = 0;
            FirmwareRevision = 0;
            SubsystemCode = Subsystem.EMPTY_CODE;
        }

        /// <summary>
        /// Set the Firmware based off 
        /// the byte array given.
        /// </summary>
        /// <param name="firmware">Firmware data.</param>
        public Firmware(byte[] firmware)
        {
            Decode(firmware);
        }

        /// <summary>
        /// Constructor that takes all the values.
        /// </summary>
        /// <param name="subSystem">Sub-system of ensemble.</param>
        /// <param name="major">Firmware major version.</param>
        /// <param name="minor">Firmware minor version.</param>
        /// <param name="revision">Firmware revision.</param>
        public Firmware(byte subSystem, UInt16 major, UInt16 minor, UInt16 revision)
        {
            FirmwareMajor = major;
            FirmwareMinor = minor;
            FirmwareRevision = revision;
            SubsystemCode = subSystem;
        }

        /// <summary>
        /// Decode the firmware into its 
        /// parts.  There will be 4 bytes.
        /// Most significant[3]: Hardware Sub-system code.
        /// [2]: Major Firmware Version.
        /// [1]: Minor Firmware Version.
        /// Least significant[0]: Firmware Revision
        /// </summary>
        /// <param name="firmware">Firmware data.</param>
        private void Decode(byte[] firmware)
        {
            FirmwareMajor = Convert.ToUInt16(firmware[MAJOR_START]);
            FirmwareMinor = Convert.ToUInt16(firmware[MINOR_START]);
            FirmwareRevision = Convert.ToUInt16(firmware[REVISION_START]);
            SubsystemCode = firmware[SUBSYSTEM_START];
        }

        /// <summary>
        /// Convert the values back to a byte array.
        /// </summary>
        /// <returns>Byte array of the firmware.</returns>
        public byte[] Encode()
        {
            byte[] result = new byte[NUM_BYTES];
            result[MAJOR_START] = (byte)FirmwareMajor;
            result[MINOR_START] = (byte)FirmwareMinor;
            result[REVISION_START] = (byte)FirmwareRevision;
            result[SUBSYSTEM_START] = (byte)SubsystemCode;

            return result;
        }

        /// <summary>
        /// Print out the firmware version.
        /// Format: Major.Minor.Revision.
        /// </summary>
        /// <returns>String of the version number.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2} - {3}", FirmwareMajor, FirmwareMinor, FirmwareRevision, (Convert.ToChar(SubsystemCode)).ToString());
        }

        /// <summary>
        /// Create a list of all the possible major, minor and revision values.
        /// </summary>
        /// <returns>List of all possible values.</returns>
        public static List<ushort> FirmwareVersionList()
        {
            List<ushort> list = new List<ushort>();

            // Populate the list
            for (ushort x = 0; x < 255; x++) 
            { 
                list.Add(x); 
            }

            return list;
        }
    }
}

