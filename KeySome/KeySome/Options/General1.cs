﻿using System.ComponentModel;
using System.Runtime.InteropServices;

namespace KeySome
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.General1Options), "KeySome", "General1", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class General1Options : BaseOptionPage<General1> { }
    }

    public class General1 : BaseOptionModel<General1>
    {
        [Category("General")]
        [DisplayName("My Option")]
        [Description("An informative description.")]
        [DefaultValue(true)]
        public bool MyOption { get; set; } = true;
    }
}
