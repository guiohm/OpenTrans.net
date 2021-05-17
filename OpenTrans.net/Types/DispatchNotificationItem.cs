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
using System.Collections.Generic;

namespace OpenTrans.net
{
    /// <summary>
    /// A item line contains dispatch information about exactly one item. Any number of item lines can be used, although at least one item line must be used.
    /// </summary>
    public class DispatchNotificationItem : OrderItem
    {
        public OrderReference OrderRefecence { get; set; }
        public SupplierOrderReference SupplierOrderReference { get; set; }
        public ShipmentPartiesReference ShipmentPartiesReference { get; set; }
        public LogisticDetails LogisticDetails { get; set; }
    }

    public class LogisticDetails
    {
        public string PackageId { get; set; }
        public decimal? PackageOrderUnitQuantity { get; set; }
    }
    public class OrderReference
    {
        public string OrderId { get; set; }
        public int? LineItemId { get; set; }
    }

    public class SupplierOrderReference
    {
        public string SupplierOrderId { get; set; }
        public string SupplierOrderItemId { get; set; }
    }
}
