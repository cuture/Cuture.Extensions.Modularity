# Cuture.Extensions.Modularity
## 1. Intro
围绕 `Microsoft.Extensions.DependencyInjection.Abstractions` 为核心的`.Net`模块化开发库.

## 2. Features
- 支持异步的模块配置方法；
- 支持基于特性标注的服务自动注入（默认不支持基于继承的服务自动注入）；
- 基本和[Abp](https://github.com/abpframework/abp)的模块实现方法相同；
- 可拓展的模块加载源，已实现基于`Type`、`Assembly`、`File`、`Directory`的模块加载；
- `IOptions<TOptions>`自动绑定；
- 可集成其它模块系统的模块（如Abp），详见示例代码；
- 主项目只依赖`Microsoft.Extensions.DependencyInjection.Abstractions`，Hosting项目额外依赖`Hosting.Abstractions`、`Configuration.Binder`、`Options`；
- Mermaid生成工具，方便查看模块依赖关系；

### Nuget包列表
|Package|Description|
|:------|:-----:|
|Cuture.Extensions.Modularity|模块化的核心库|
|Cuture.Extensions.Modularity.Hosting|对Host的模块化支持库，用于在通用主机中加载模块|

## 3. 如何使用

### 3.1 安装`Nuget`包

```PowerShell
Install-Package Cuture.Extensions.Modularity
```

### 3.2 实现一个模块

```C#
[DependsOn(
    typeof(DependsSampleModule1),
    typeof(DependsSampleModule2)
    )]  //定义依赖的模块
[AutoRegisterServicesInAssembly]
public class SampleModule : AppModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //进行模块需要的配置
    }
}
```
- `[DependsOn]`：声明此模块依赖的模块，加载顺序与声明顺序相同;（可选）
- `[AutoRegisterServicesInAssembly]`：有此特性的模块将会自动将所在程序集中使用ExportServices标记的导出服务注入到DI容器;（可选）
- 继承`AppModule`：标准的模块只需要继承`IAppModule`即可。如果需要单独的配置，则可以单独实现`IPreConfigureServices`、`IConfigureServices`、`IPostConfigureServices`、`IOnPreApplicationInitialization`、`IOnApplicationInitialization`、`IOnPostApplicationInitialization`、`IOnApplicationShutdown`以在对应的时机进行配置，或直接继承`AppModule`并重写对应的方法；所有方法都有异步版本`IPreConfigureServicesAsync`、`IConfigureServicesAsync`、`IPostConfigureServicesAsync`、`IOnPreApplicationInitializationAsync`、`IOnApplicationInitializationAsync`、`IOnPostApplicationInitializationAsync`、`IOnApplicationShutdownAsync`，或直接继承`AsyncAppModule`；

### 3.3 直接通过DI容器使用模块

##### 可能需要安装`Microsoft.Extensions.DependencyInjection`包，视主项目而定

```C#
var services = new ServiceCollection();

//加载模块
services.LoadModule<SampleModule>() //直接从类型加载
        .LoadModuleFile(modulePath) //从文件加载
        .LoadModuleDirectory(source =>
        {
            source.SearchDepth = 5;    //设置文件夹搜索深度
        }, moduleDirectory)  //从文件夹加载
        .ModuleLoadComplete()   //必须调用此方法，以确认模块加载完成

using (var serviceProvider = services.BuildServiceProvider())
{
    //必须调用此方法，以初始化模块
    serviceProvider.InitializationModulesWithOutHostLifetime();

    //这里使用初始化了模块的serviceProvider

    //关闭模块
    serviceProvider.ShutdownModules();
}
```
Note:
- 直接使用DI容器时，不会在控制台等关闭时，自动调用模块结束方法（使用Host时会），需要手动调用；

### 3.4 通过通用主机使用模块 (Console、Asp .net Core...)

#### 1. 安装Hosting拓展库
```PowerShell
Install-Package Cuture.Extensions.Modularity.Hosting
```

#### 2. 在主机构建时配置模块加载
```C#
Host.CreateDefaultBuilder(args)
    .LoadModule<SampleModule>() //直接从类型加载
    .LoadModuleFile(modulePath) //从文件加载
    .LoadModuleDirectory(source =>
    {
        source.SearchDepth = 5;    //设置文件夹搜索深度
    }, moduleDirectory)  //从文件夹加载
    .UseConsoleLifetime()
    .InitializationModules()    //必须调用此方法，以初始化模块
    .Run();
```

 - 也可以通过配置主机DI容器的方法来使用

更多细节详见示例代码；

### 3.5 服务导出

- 需要为类型`所在程序集`的`模块`标记特性`[AutoRegisterServicesInAssembly]`

```C#
[ExportServices(ServiceLifetime.Singleton, AddDIMode.Replace, typeof(IHello))]
public class Hello : IHello
{
    public string SayHello()
    {
        return "Hello";
    }
}
```
示例代码会自动将`Hello`类型以`单例`模式注册为`IHello`服务，并在注入时使用`Replace`方法。

参数：
- ServiceLifetime：注册的生命周期类型；
- AddDIMode：注册的方法，包括`Add`、`TryAdd`、`Replace`；

除示例的`ExportServices`外，还有`ExportSingletonServices`、`ExportScopedServices`、`ExportTransientServices`等多个拓展特性的重载实现；

## `IOptions<TOptions>`自动绑定
 - 自动查找模块中继承了`IOptions<TOptions>`的类；
 - 使用其完整名称为路径，在`IConfiguration`查找节点，并绑定值；
 - 如 `A` 类命名空间为 `B.C.D.E.F` ，则`IConfiguration`查找路径为 `B:C:D:E:F:A`；
 - Note: 构建过程中必须有可访问的`IConfiguration` !!! 详见示例项目；

示例配置代码:

 - 在构建中调用 `AutoBindModuleOptions()` 方法即可；
```C#
Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(builder => builder.AddJsonFile("appsettings.Development.json"))
    .LoadModule<HostSampleModule>()
    .AutoBindModuleOptions()    //自动使用 IConfiguration 绑定模块中继承了 IOptions<TOptions> 的类
    .UseConsoleLifetime()
    .InitializationModules()
    .Run();
```

## 集成其它模块系统的模块

参考示例项目`OtherModuleSystemAdaptSample`

## 其它

### 获取依赖的`Mermaid`字符串

```C#
var abpModuleDescriptors = AppModuleDependencyUtil.FindAllDependedModuleDescriptors(typeof(XXXModule));
var mermaidString = abpModuleDescriptors.ToMermaidString();
```
然后将`mermaidString`复制到支持`mermaid`的编辑器就能查看模块依赖关系了。