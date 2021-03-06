﻿using System.Linq;

namespace SHAutomation.Core.AutomationElements
{
    public partial class SHAutomationElement
    {
        public ISHAutomationElement FindFirstDescendantNameStartsWith(string name, int timeout = 20000, bool waitUntilExists = true)
        {
            var elements = FindAllDescendantsNameStartsWith(name, timeout, waitUntilExists);
            return elements.Any() ? elements.First() : null;
        }
    }
}
