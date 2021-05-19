using System;
using System.IO;
using System.Xml;

namespace OpenTrans.net
{
    internal class DispatchNotificationReader : BaseReader<DispatchNotification>
    {
        public override DispatchNotification Load(Stream stream)
        {
            XmlDocument doc = _loadXml(stream);

            DispatchNotification retval = new DispatchNotification();




            XmlNode headerNode = doc.SelectSingleNode("//*[local-name()='DISPATCHNOTIFICATION_HEADER']", nsmgr);
            if (headerNode != null)
            {
                retval.GenerationDate = _nodeAsDateTime(headerNode, "./*[local-name()='CONTROL_INFO']/*[local-name()='GENERATION_DATE']", nsmgr);
                retval.NotificationDate = _nodeAsDateTime(headerNode, "./*[local-name()='DISPATCHNOTIFICATION_INFO']/*[local-name()='DISPATCHNOTIFICATION_DATE']", nsmgr);
                retval.NotificationId = XmlUtils.NodeAsString(headerNode, "./*[local-name()='DISPATCHNOTIFICATION_INFO']/*[local-name()='DISPATCHNOTIFICATION_ID']", nsmgr);

                XmlNode deliveryDateRefNode = headerNode.SelectSingleNode("./*[local-name()='DISPATCHNOTIFICATION_INFO']/*[local-name()='DELIVERY_DATE']", nsmgr);
                if (deliveryDateRefNode != null)
                {
                    retval.DeliveryDate = new DeliveryDate
                    {
                        StartDate = _nodeAsDateTime(deliveryDateRefNode, "./*[local-name()='DELIVERY_START_DATE']", nsmgr),
                        EndDate = _nodeAsDateTime(deliveryDateRefNode, "./*[local-name()='DELIVERY_END_DATE']", nsmgr),
                    };
                }

                XmlNodeList partyNodes = headerNode.SelectNodes(".//*[local-name()='PARTIES']/*[local-name()='PARTY']", nsmgr);
                foreach (XmlNode partyNode in partyNodes)
                {
                    Party p = _readParty(partyNode, nsmgr);
                    retval.Parties.Add(p);
                }

                XmlNode shipmentPartiesRefNode = headerNode.SelectSingleNode("./*[local-name()='DISPATCHNOTIFICATION_INFO']/*[local-name()='SHIPMENT_PARTIES_REFERENCE']", nsmgr);
                if (shipmentPartiesRefNode != null)
                {
                    retval.ShipmentPartiesReference = new ShipmentPartiesReference
                    {
                        DeliveryIdRef = XmlUtils.NodeAsString(shipmentPartiesRefNode, "./*[local-name()='DELIVERY_IDREF']", nsmgr),
                        FinalDeliveryIdRef = XmlUtils.NodeAsString(shipmentPartiesRefNode, "./*[local-name()='FINAL_DELIVERY_IDREF']", nsmgr),
                    };
                }
            }

            XmlNodeList itemNodes = doc.SelectNodes("//*[local-name()='DISPATCHNOTIFICATION_ITEM_LIST']/*[local-name()='DISPATCHNOTIFICATION_ITEM']", nsmgr);

            foreach (XmlNode itemNode in itemNodes)
            {
                var orderItem = _readItem(itemNode, nsmgr);

                retval.NotificationItems.Add(_readNotificationItem(orderItem, itemNode, nsmgr));
            }

            return retval;
        }

        private DispatchNotificationItem _readNotificationItem(OrderItem orderItem, XmlNode node, XmlNamespaceManager nsmgr)
        {
            var retval = new DispatchNotificationItem(orderItem);

            XmlNode orderRefNode = node.SelectSingleNode("./*[local-name()='ORDER_REFERENCE']", nsmgr);
            if (orderRefNode != null)
            {
                retval.OrderRefecence = new OrderReference()
                {
                    OrderId = XmlUtils.NodeAsString(orderRefNode, "./*[local-name()='ORDER_ID']", nsmgr),
                    LineItemId = XmlUtils.NodeAsInt(orderRefNode, "./*[local-name()='LINE_ITEM_ID']", nsmgr),
                };
            }

            XmlNode supplierOrderRefNode = node.SelectSingleNode("./*[local-name()='SUPPLIER_ORDER_REFERENCE']", nsmgr);
            if (supplierOrderRefNode != null)
            {
                retval.SupplierOrderReference = new SupplierOrderReference
                {
                    SupplierOrderId = XmlUtils.NodeAsString(supplierOrderRefNode, "./*[local-name()='SUPPLIER_ORDER_ID']", nsmgr),
                    SupplierOrderItemId = XmlUtils.NodeAsString(supplierOrderRefNode, "./*[local-name()='SUPPLIER_ORDER_ITEM_ID']", nsmgr),
                };
            }

            XmlNode shipmentPartiesRefNode = node.SelectSingleNode("./*[local-name()='SHIPMENT_PARTIES_REFERENCE']", nsmgr);
            if (shipmentPartiesRefNode != null)
            {
                retval.ShipmentPartiesReference = new ShipmentPartiesReference
                {
                    DeliveryIdRef = XmlUtils.NodeAsString(shipmentPartiesRefNode, "./*[local-name()='DELIVERY_IDREF']", nsmgr),
                };
            }

            XmlNode logisticDetailsNode = node.SelectSingleNode("./*[local-name()='LOGISTIC_DETAILS']/*[local-name()='PACKAGE_INFO']/*[local-name()='PACKAGE']", nsmgr);
            if (logisticDetailsNode != null)
            {
                retval.LogisticDetails = new LogisticDetails
                {
                    PackageId = XmlUtils.NodeAsString(logisticDetailsNode, "./*[local-name()='PACKAGE_ID']", nsmgr),
                    PackageOrderUnitQuantity = XmlUtils.NodeAsDecimal(logisticDetailsNode, "./*[local-name()='PACKAGE_ORDER_UNIT_QUANTITY']", nsmgr),
                };
            }
            return retval;
        }
    }
}
