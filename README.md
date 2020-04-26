# IpRateLimiter.AspNetCore.AltairCA
[![Build Status](https://jenkins.altairsl.us/buildStatus/icon?job=IPRateLimit%2FPublish)](https://jenkins.altairsl.us/job/IPRateLimit/job/Publish/)

IpRateLimiter.AspNetCore.AltairCA is an request limiting solution by looking at the client ip address. 

**Inspired by `AspNetCoreRateLimit` [repo link](https://github.com/stefanprodan/AspNetCoreRateLimit)**


`IpRateLimiter.AspNetCore.AltairCA` targets `netstandard2.0`. The package has following dependencies

```xml
<PackageReference Include="Microsoft.AspNetCore.Mvc.Abstractions" Version="1.1.3" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Core" Version="1.1.3" />
<PackageReference Include="Microsoft.Extensions.Caching.Abstractions" Version="1.1.0" />
<PackageReference Include="Microsoft.Extensions.Configuration" Version="2.2.0" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="2.2.0" />
<PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="2.2.0" />
<PackageReference Include="Newtonsoft.Json" Version="12.0.2" />
```

## setup

### NuGet install:

`Install-Package IpRateLimiter.AspNetCore.AltairCA`

### Startup.cs

```c#
public void ConfigureServices(IServiceCollection services)
{
  services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
  services.AddMemoryCache();
  services.AddHttpContextAccessor();
  services.AddScoped<IIpRateLimitStorageProvider, MemoryCacheProvider>();
  services.AddIpRateLimiter(options =>
  {
    options.GlobalRateLimit = 10;
    options.GlobalSpan = TimeSpan.FromMinutes(30);
    options.ExcludeList = new List<string>
    {
      "127.0.0.1", "192.168.0.0/24"
     };
     });      
 }
```

Default options for IpRateLimit

```c#
public class IpRateLimitOptions
    {
        public string RealIpHeader { get; set; }
        public List<string> ExcludeList { get; set; }
        public int GlobalRateLimit { get; set; } = 1000;
        public TimeSpan GlobalSpan { get; set; } = TimeSpan.FromMinutes(30);
        public int StatusCode { get; set; } = 429;
        public object LimitReachedResponse = new {detail = "Quota exceeded. Maximum allowed: {0} per {1}. Please try again in {2} second(s)." };
        public string CachePrefix { get; set; } = "AltairCA";

    }
```
`{0}` is max limit, `{1}` is period in seconds, `{2}` when the quota get resets in seconds


### Using it in a controller

```c#
    [Route("api/[controller]")]
    [ApiController]
    [IpRateLimitHttp]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [IpRateLimitHttp(10*60,2,"group1" )]
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        [IpRateLimitHttp(10*60,2,"group1" )]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
```

You can apply the filter attribute at the top of the controller class. It will apply the rule for all of the endpoints that defined in the controller or you can put the attribute at the endpoint level. If you put the attribute at the class level and the endpoint level it will work as a `AND` operator.
