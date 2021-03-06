﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.Operations.DataFlow.ParameterValidationAnalysis
{
    using ParameterValidationAnalysisData = IDictionary<AbstractLocation, ParameterValidationAbstractValue>;

    internal partial class ParameterValidationAnalysis : ForwardDataFlowAnalysis<ParameterValidationAnalysisData, ParameterValidationBlockAnalysisResult, ParameterValidationAbstractValue>
    {
        /// <summary>
        /// Abstract value domain for <see cref="ParameterValidationAnalysis"/> to merge and compare <see cref="ParameterValidationAbstractValue"/> values.
        /// </summary>
        private class ParameterValidationAbstractValueDomain : AbstractValueDomain<ParameterValidationAbstractValue>
        {
            public static ParameterValidationAbstractValueDomain Default = new ParameterValidationAbstractValueDomain();

            private ParameterValidationAbstractValueDomain() { }

            public override ParameterValidationAbstractValue Bottom => ParameterValidationAbstractValue.NotApplicable;

            public override ParameterValidationAbstractValue UnknownOrMayBeValue => ParameterValidationAbstractValue.MayBeValidated;

            public override int Compare(ParameterValidationAbstractValue oldValue, ParameterValidationAbstractValue newValue)
            {
                return Comparer<ParameterValidationAbstractValue>.Default.Compare(oldValue, newValue);
            }

            public override ParameterValidationAbstractValue Merge(ParameterValidationAbstractValue value1, ParameterValidationAbstractValue value2)
            {
                if (value1 == value2)
                {
                    return value1;
                }
                else if (value1 == ParameterValidationAbstractValue.NotApplicable ||
                    value2 == ParameterValidationAbstractValue.NotApplicable)
                {
                    return ParameterValidationAbstractValue.NotApplicable;
                }

                return ParameterValidationAbstractValue.MayBeValidated;
            }
        }
    }
}
