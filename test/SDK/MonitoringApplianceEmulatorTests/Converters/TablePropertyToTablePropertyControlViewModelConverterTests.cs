﻿//-----------------------------------------------------------------------
// <copyright file="TablePropertyToTablePropertyControlViewModelConverterTests.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MonitoringApplianceEmulatorTests.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.Azure.Monitoring.SmartDetectors.MonitoringApplianceEmulator.Converters;
    using Microsoft.Azure.Monitoring.SmartDetectors.MonitoringApplianceEmulator.ViewModels;
    using Microsoft.Azure.Monitoring.SmartDetectors.RuntimeEnvironment.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TablePropertyToTablePropertyControlViewModelConverterTests
    {
        [TestMethod]
        public void WhenConvertingTablePropertyThenResultIsTablePropertyControlViewModel()
        {
            var columns = new List<TableColumn>()
            {
                new TableColumn(nameof(TestTableAlertPropertyValue.FirstName), "First Name"),
            };

            var rows = new List<TestTableAlertPropertyValue>()
            {
                new TestTableAlertPropertyValue() { FirstName = "Edinson", LastName = "Cavani", Goals = 4.67 },
            };

            var tableAlertProperty = new TableAlertProperty<TestTableAlertPropertyValue>("propertyName", "displayName", 5, true, columns, rows);
            var converter = new TablePropertyToTablePropertyControlViewModelConverter();

            object result = converter.Convert(tableAlertProperty, typeof(TablePropertyControlViewModel<TestTableAlertPropertyValue>), null, new CultureInfo("en-us"));

            Assert.IsInstanceOfType(result, typeof(TablePropertyControlViewModel<TestTableAlertPropertyValue>));
        }

        [TestMethod]
        public void WhenConvertingIntegerThenExceptionIsThrown()
        {
            var converter = new TablePropertyToTablePropertyControlViewModelConverter();

            Exception thrownException = null;
            try
            {
                converter.Convert(12, typeof(TablePropertyControlViewModel<string>), null, new CultureInfo("en-us"));
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            Assert.IsNotNull(thrownException);
        }

        [TestMethod]
        public void WhenConvertingNullThenNullIsReturned()
        {
            var converter = new TablePropertyToTablePropertyControlViewModelConverter();

            object result = converter.Convert(null, typeof(TablePropertyControlViewModel<string>), null, new CultureInfo("en-us"));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void WhenConvertingDisconnectedObjectThenSameObjectIsReturned()
        {
            var converter = new TablePropertyToTablePropertyControlViewModelConverter();

            DisconnectedItem disconnectedItem = new DisconnectedItem();

            object result = converter.Convert(disconnectedItem, typeof(TablePropertyControlViewModel<string>), null, new CultureInfo("en-us"));

            Assert.AreEqual(disconnectedItem, result, "The conversion result should be the converted disconnected item object");
        }
    }
}