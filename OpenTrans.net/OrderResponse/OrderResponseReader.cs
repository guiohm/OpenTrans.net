﻿/*
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
using System.IO;
using System.Xml;

namespace OpenTrans.net
{
    internal class OrderResponseReader : BaseReader<OrderResponse>
    {
        public override OrderResponse Load(Stream stream)
        {
            XmlDocument doc = _loadXml(stream);

            OrderResponse retval = new OrderResponse();

            XmlNode headerNode = doc.SelectSingleNode("//*[local-name()='ORDERRESPONSE_HEADER']", nsmgr);
            if (headerNode != null)
            {
                DateTime? generationDate = _nodeAsDateTime(headerNode, "./*[local-name()='CONTROL_INFO']/*[local-name()='GENERATION_DATE']", nsmgr);
                retval.OrderId = XmlUtils.NodeAsString(headerNode, "./*[local-name()='ORDERRESPONSE_INFO']/*[local-name()='ORDER_ID']", nsmgr);
                retval.SupplierOrderId = XmlUtils.NodeAsString(headerNode, "./*[local-name()='ORDERRESPONSE_INFO']/*[local-name()='SUPPLIER_ORDER_ID']", nsmgr);
                retval.OrderDate = _nodeAsDateTime(headerNode, "./*[local-name()='ORDERRESPONSE_INFO']/*[local-name()='ORDER_DATE']", nsmgr);
                retval.OrderChangeSequenceId = XmlUtils.NodeAsInt(headerNode, "./*[local-name()='ORDERRESPONSE_INFO']/*[local-name()='ORDERCHANGE_SEQUENCE_ID']", nsmgr);

                XmlNodeList partyNodes = headerNode.SelectNodes(".//*[local-name()='PARTIES']/*[local-name()='PARTY']", nsmgr);
                foreach (XmlNode partyNode in partyNodes)
                {
                    Party p = _readParty(partyNode, nsmgr);
                    retval.Parties.Add(p);
                }
            }

            XmlNodeList itemNodes = doc.SelectNodes("//*[local-name()='ORDERRESPONSE_ITEM_LIST']/*[local-name()='ORDERRESPONSE_ITEM']", nsmgr);
            foreach (XmlNode itemNode in itemNodes)
            {
                retval.OrderItems.Add(_readItem(itemNode, nsmgr));
            }


            return retval;
        }
    }
}