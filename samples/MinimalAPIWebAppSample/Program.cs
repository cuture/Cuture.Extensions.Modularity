using MinimalAPIWebAppSample;

using SampleModule2;

using SampleModule4;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;

// *** 设置IConfiguration，以使 `services.GetConfiguration` 能够正常工作 ***
services.SetConfiguration(builder.Configuration);

//添加IApplicationBuilder的访问器，以便可以在模块中进行访问
services.AddObjectAccessor<IApplicationBuilder>();

var module3Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleModule3.dll");
var module5Directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

//加载模块
services.LoadModule<MinimalAPISampleModule>()
        .AddModuleFile(module3Path) //从文件加载
        .AddModuleDirectory(source =>
        {
            source.SearchDepth = 5;    //设置文件夹搜索深度
        }, module5Directory)  //从文件夹加载
        .AutoBindModuleOptions();    //自动使用 IConfiguration 绑定标记了 AutoRegisterServicesInAssemblyAttribute 的模块中继承了 IOptions<TOptions> 的类

await services.ModuleLoadCompleteAsync();   //完成加载

var app = builder.Build();

//为IApplicationBuilder的访问器设置值，以便可以在模块中进行访问
app.Services.SetObjectAccessorValue<IApplicationBuilder>(app);

//将IApplicationBuilder作为键app，传递到初始化上下文
app.Services.InitializationModules("app", app);

//初始化模块
await app.InitializationModulesAsync();

//移除对象访问器的值，使其不再引用IApplicationBuilder
app.Services.RemoveObjectAccessorValue<IApplicationBuilder>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/test", (HttpContext httpContext, IHelloable helloable, IAccessCounter accessCounter, IRequestRandomProvider randomProvider) =>
{
    accessCounter.Add();
    return new
    {
        hello = helloable.SayHello(),
        accessCount = accessCounter.Count,
        scopedRandom1 = randomProvider.Random(),
        scopedRandom2 = randomProvider.Random(),
        random1 = httpContext.RequestServices.GetRequiredService<IRandomProvider>().Random(),
        random2 = httpContext.RequestServices.GetRequiredService<IRandomProvider>().Random(),
    };
})
.WithName("Test");

app.Run();
