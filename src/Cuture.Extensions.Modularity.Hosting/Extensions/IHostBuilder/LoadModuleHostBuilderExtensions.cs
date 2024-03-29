﻿using Cuture.Extensions.Modularity;
using Cuture.Extensions.Modularity.Hosting;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

/// <summary>
///
/// </summary>
public static class LoadModuleHostBuilderExtensions
{
    #region Load

    /// <inheritdoc cref="LoadModuleServiceCollectionExtensions.LoadModule{TModule}(IServiceCollection, Action{ModuleLoadOptions}?)"/>
    public static IHostBuilder LoadModule<TModule>(this IHostBuilder hostBuilder, Action<ModuleLoadOptions>? optionAction = null) where TModule : IAppModule
    {
        return hostBuilder.InternalAddModuleSource(new TypeModuleSource(typeof(TModule)), optionAction);
    }

    /// <inheritdoc cref="LoadModuleServiceCollectionExtensions.LoadModule(IServiceCollection, IModuleSource, Action{ModuleLoadOptions}?)"/>
    public static IHostBuilder LoadModule(this IHostBuilder hostBuilder, IModuleSource moduleSource, Action<ModuleLoadOptions>? optionAction = null)
    {
        return hostBuilder.InternalAddModuleSource(moduleSource, optionAction);
    }

    #endregion Load

    #region Directory

    /// <inheritdoc cref="LoadModuleDirectory(IHostBuilder, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleDirectory(this IHostBuilder hostBuilder, params string[] directories)
    {
        return hostBuilder.LoadModuleDirectory(null, null, directories);
    }

    /// <inheritdoc cref="LoadModuleDirectory(IHostBuilder, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleDirectory(this IHostBuilder hostBuilder, Action<ModuleLoadOptions>? optionAction, params string[] directories)
    {
        return hostBuilder.LoadModuleDirectory(null, optionAction, directories);
    }

    /// <inheritdoc cref="LoadModuleDirectory(IHostBuilder, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleDirectory(this IHostBuilder hostBuilder, Action<DirectoryModuleSource>? sourceOptionAction, params string[] directories)
    {
        return hostBuilder.LoadModuleDirectory(sourceOptionAction, null, directories);
    }

    /// <inheritdoc cref="LoadModuleServiceCollectionExtensions.LoadModuleDirectory(IServiceCollection, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleDirectory(this IHostBuilder hostBuilder, Action<DirectoryModuleSource>? sourceOptionAction, Action<ModuleLoadOptions>? optionAction, params string[] directories)
    {
        var source = new DirectoryModuleSource(directories);

        sourceOptionAction?.Invoke(source);

        return hostBuilder.InternalAddModuleSource(source, optionAction);
    }

    #endregion Directory

    #region File

    /// <inheritdoc cref="LoadModuleFile(IHostBuilder, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleFile(this IHostBuilder hostBuilder, params string[] files)
    {
        return hostBuilder.LoadModuleFile(null, null, files);
    }

    /// <inheritdoc cref="LoadModuleFile(IHostBuilder, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleFile(this IHostBuilder hostBuilder, Action<ModuleLoadOptions>? optionAction, params string[] files)
    {
        return hostBuilder.LoadModuleFile(null, optionAction, files);
    }

    /// <inheritdoc cref="LoadModuleFile(IHostBuilder, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleFile(this IHostBuilder hostBuilder, Action<FileModuleSource>? sourceOptionAction, params string[] files)
    {
        return hostBuilder.LoadModuleFile(sourceOptionAction, null, files);
    }

    /// <inheritdoc cref="LoadModuleServiceCollectionExtensions.LoadModuleFile(IServiceCollection, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
    public static IHostBuilder LoadModuleFile(this IHostBuilder hostBuilder, Action<FileModuleSource>? sourceOptionAction, Action<ModuleLoadOptions>? optionAction, params string[] files)
    {
        var source = new FileModuleSource(files);

        sourceOptionAction?.Invoke(source);

        return hostBuilder.InternalAddModuleSource(source, optionAction);
    }

    #endregion File
}
