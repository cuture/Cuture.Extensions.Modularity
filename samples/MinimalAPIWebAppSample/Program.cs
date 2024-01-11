using Cuture.Extensions.Modularity;

using MinimalAPIWebAppSample;

using SampleModule2;

using SampleModule4;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;

// *** ����IConfiguration����ʹ `services.GetConfiguration` �ܹ��������� ***
services.SetConfiguration(builder.Configuration);

//����IApplicationBuilder�ķ��������Ա������ģ���н��з���
services.AddObjectAccessor<IApplicationBuilder>();

var module3Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleModule3.dll");
var module5Directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

//����ģ��(���� WebApplicationBuilder �������ԣ�����ʹ�� `builder.Host` ����ģ�����)
services.LoadModule<MinimalAPISampleModule>()
        .AddModuleFile(module3Path) //���ļ�����
        .AddModuleDirectory(source =>
        {
            source.SearchDepth = 5;    //�����ļ����������
        }, module5Directory)  //���ļ��м���
        .AutoBindModuleOptions();    //�Զ�ʹ�� IConfiguration �󶨱���� AutoRegisterServicesInAssemblyAttribute ��ģ���м̳��� IOptions<TOptions> ����

await services.ModuleLoadCompleteAsync();   //��ɼ���

var app = builder.Build();

//ΪIApplicationBuilder�ķ���������ֵ���Ա������ģ���н��з���
app.Services.SetObjectAccessorValue<IApplicationBuilder>(app);

//��IApplicationBuilder��Ϊ��app�����ݵ���ʼ��������
app.Services.InitializationModules("app", app);

//��ʼ��ģ��
await app.InitializationModulesAsync();

//�Ƴ������������ֵ��ʹ�䲻������IApplicationBuilder
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
