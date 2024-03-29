﻿// see: https://source.dot.net/#Microsoft.Extensions.Options.ConfigurationExtensions/NamedConfigureFromConfigurationOptions.cs
#pragma warning disable IDE0079
#pragma warning disable IDE0044

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Options;

/// <summary>
/// Configures an option instance by using <see cref="ConfigurationBinder.Bind(IConfiguration, object)"/> against an <see cref="IConfiguration"/>.
/// </summary>
/// <typeparam name="TOptions">The type of options to bind.</typeparam>
internal class NamedConfigureFromConfigurationOptions<TOptions> : ConfigureNamedOptions<TOptions>
    where TOptions : class
{
    /// <summary>
    /// Constructor that takes the <see cref="IConfiguration"/> instance to bind against.
    /// </summary>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="config">The <see cref="IConfiguration"/> instance.</param>
    public NamedConfigureFromConfigurationOptions(string name, IConfiguration config)
        : this(name, config, _ => { })
    { }

    /// <summary>
    /// Constructor that takes the <see cref="IConfiguration"/> instance to bind against.
    /// </summary>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="config">The <see cref="IConfiguration"/> instance.</param>
    /// <param name="configureBinder">Used to configure the <see cref="BinderOptions"/>.</param>
    public NamedConfigureFromConfigurationOptions(string name, IConfiguration config, Action<BinderOptions> configureBinder)
        : base(name, options => config.Bind(options, configureBinder))
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
    }
}
