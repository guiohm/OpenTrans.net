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
