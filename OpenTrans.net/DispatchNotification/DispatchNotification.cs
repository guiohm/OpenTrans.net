/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenTrans.net
{
    public class DispatchNotification
    {
        public string NotificationId { get; set; }
        public DateTime? GenerationDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public DeliveryDate DeliveryDate { get; set; }
        public ShipmentPartiesReference ShipmentPartiesReference { get; set; }
        public List<Party> Parties { get; set; } = new List<Party>();

        public List<DispatchNotificationItem> NotificationItems { get; set; } = new List<DispatchNotificationItem>();


        /// <summary>
        /// Loads an DispatchNotification from the given stream.
        ///
        /// Make sure that the stream is open. After successful reading, the stream is left open, i.e.
        /// the caller of the library has to take care of the stream lifecycle.
        ///
        /// If the stream is not open or readable, an IllegalStreamException exception is raised.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static DispatchNotification Load(Stream stream)
        {
            return new DispatchNotificationReader().Load(stream);
        }


        /// <summary>
        /// Loads an DispatchNotification from the given file.
        /// If the file does not exist, a FileNotFoundException exception is raised.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DispatchNotification Load(string filename)
        {
            return new DispatchNotificationReader().Load(filename);
        }
    }
}
