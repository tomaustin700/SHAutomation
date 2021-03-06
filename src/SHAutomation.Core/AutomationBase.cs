﻿using System;
using System.Drawing;
using SHAutomation.Core.AutomationElements;
using SHAutomation.Core.Conditions;
using SHAutomation.Core.Definitions;
using SHAutomation.Core.EventHandlers;
using SHAutomation.Core.Identifiers;
using SHAutomation.Core.Overlay;

namespace SHAutomation.Core
{
    /// <summary>
    /// Base class for the native automation object.
    /// </summary>
    public abstract class AutomationBase : IDisposable
    {
        protected AutomationBase(IPropertyLibrary propertyLibrary, IEventLibrary eventLibrary, IPatternLibrary patternLibrary, ITextAttributeLibrary textAttributeLibrary)
        {
            PropertyLibrary = propertyLibrary;
            EventLibrary = eventLibrary;
            PatternLibrary = patternLibrary;
            ConditionFactory = new ConditionFactory(propertyLibrary);


#if NETSTANDARD
            OverlayManager = new NullOverlayManager();
#else
            OverlayManager = new WinFormsOverlayManager();

#endif        
            TextAttributeLibrary = textAttributeLibrary;


            // Make sure all pattern ids are initialized
            var unused = PatternLibrary.AllForCurrentFramework;
        }

        /// <summary>
        /// Provides a library with the existing <see cref="PropertyId"/>s.
        /// </summary>
        public IPropertyLibrary PropertyLibrary { get; }

        /// <summary>
        /// Provides a library with the existing <see cref="EventId"/>s.
        /// </summary>
        public IEventLibrary EventLibrary { get; }

        /// <summary>
        /// Provides a library with the existing <see cref="PatternId"/>s.
        /// </summary>
        public IPatternLibrary PatternLibrary { get; }
        /// <summary>
        /// Provides a library with the existing <see cref="TextAttributeId"/>s.
        /// </summary>
        public ITextAttributeLibrary TextAttributeLibrary { get; }

        /// <summary>
        /// Provides a factory to create conditions for searching.
        /// </summary>
        public ConditionFactory ConditionFactory { get; }

        /// <summary>
        /// Provides a manager for displaying overlays.
        /// </summary>
        public IOverlayManager OverlayManager { get; }

        /// <summary>
        /// Provides a factory to create <see cref="ITreeWalker"/>s.
        /// </summary>
        public abstract ITreeWalkerFactory TreeWalkerFactory { get; }

        /// <summary>
        /// The <see cref="AutomationType"/> of the automation implementation.
        /// </summary>
        public abstract AutomationType AutomationType { get; }

        /// <summary>
        /// Object which represents the "Not Supported" value.
        /// </summary>
        public abstract object NotSupportedValue { get; }

        /// <summary>
        /// Specifies the length of time that UI Automation will wait for a provider to respond to a client request for information about an automation element.
        /// </summary>
        public abstract TimeSpan TransactionTimeout { get; set; }

        /// <summary>
        /// Specifies the length of time that UI Automation will wait for a provider to respond to a client request for an automation element.
        /// </summary>
        public abstract TimeSpan ConnectionTimeout { get; set; }

        public abstract ConnectionRecoveryBehaviorOption ConnectionRecoveryBehavior { get; set; }

        /// <summary>
        /// Gets or sets whether an accessible technology client receives all events, or a subset where duplicate events are detected and filtered.
        /// </summary>
        public abstract CoalesceEventsOption CoalesceEvents { get; set; }

        /// <summary>
        /// Gets the desktop (root) element.
        /// </summary>
        public abstract SHAutomationElement GetDesktop();

        /// <summary>
        /// Creates an <see cref="AutomationElement" /> from a given point.
        /// </summary>
        public abstract SHAutomationElement FromPoint(Point point);

        /// <summary>
        /// Creates an <see cref="AutomationElement" /> from a given windows handle (HWND).
        /// </summary>
        public abstract SHAutomationElement FromHandle(IntPtr hwnd);

        /// <summary>
        /// Gets the currently focused element as an <see cref="AutomationElement"/>.
        /// </summary>
        /// <returns></returns>
        public abstract SHAutomationElement FocusedElement();

        /// <summary>
        /// Registers for a focus changed event.
        /// </summary>
        public abstract FocusChangedEventHandlerBase RegisterFocusChangedEvent(Action<SHAutomationElement> action);

        /// <summary>
        /// Unregisters the given focus changed event handler.
        /// </summary>
        public abstract void UnregisterFocusChangedEvent(FocusChangedEventHandlerBase eventHandler);

        /// <summary>
        /// Removes all registered event handlers.
        /// </summary>
        public abstract void UnregisterAllEvents();

        /// <summary>
        /// Compares two automation elements for equality.
        /// </summary>
        public abstract bool Compare(SHAutomationElement element1,SHAutomationElement element2);

        /// <summary>
        /// Cleans up the resources.
        /// </summary>
        public void Dispose()
        {
            UnregisterAllEvents();
        }
    }
}
