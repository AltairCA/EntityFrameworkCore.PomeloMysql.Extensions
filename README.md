# AltairCA.EntityFrameworkCore.PomeloMysql.Extensions
[![Build Status](https://jenkins.altairsl.us/buildStatus/icon?job=Pomelo+Mysql+Encrypt%2FPublish)](https://jenkins.altairsl.us/view/Nugets/job/Pomelo%20Mysql%20Encrypt/job/Publish/)

AltairCA.EntityFrameworkCore.PomeloMysql.Extensions is a Pomelo Mysql Extension that supports native MySQL AES encryption. Meaning this will support search query on encrypted columns. Well, this is good if you have GDPR compliance requirement.

### Note
`If you use this make sure your application to the MySQL service is use a encrypted connection because this will transmit the RAW PASSWORD over the network`. You can enforce it in the connection string. example -
```json
{
  "DefaultConnection": "server=127.0.0.1;database=youdb;user=root;password=;persistsecurityinfo=True;port=3306;SslMode=Required;CharSet=utf8mb4;"
}
```
take a look at `SslMode=Required`

`AltairCA.EntityFrameworkCore.PomeloMysql.Extensions` targets `netstandard2.0`. The package has following dependencies

```xml
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="3.1.1" />
```

When you choose the version choose with the `.Net Core` Version for example if `.Net Core` version is 3.1 then choose `AltairCA.EntityFrameworkCore.PomeloMysql.Extensions` version 3.1.x

## setup

### NuGet install:

`Install-Package AltairCA.EntityFrameworkCore.PomeloMysql.Extensions`

### DbContext

```c#
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseEncryptionFunctions("HelloWorld1");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseEncryptAttribute("HelloWorld1");
        }
```

Replace `HelloWorld` with your password

### Example of use

#### `MysqlEncrypt` Annotation Use
```c#
    public class TestModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MysqlEncrypt] #Use MysqlEncrypt attribute to denote the property must be encrypt in database
        public string Name { get; set; }
    }
```
#### How to use it in search query
```c#
var searchTest = _dbContext.TModels.Where(x => x.Name.MySqlDecrypt().Contains("test")).ToList();
```

Above Linq will convert to a Native MySql Query that will decrypt the column before it do a search.

You can find a example that I have used in the `WebApplication` Project `HomeController.cs`


Just added a comment here !
