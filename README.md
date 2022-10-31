# LazySerializer

![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg) ![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)

A small library to easily serialize or deserialize an object in an XML file.

## Installation

Use NuGet ([LazySerializer](https://www.nuget.org/packages/LazySerializer/)) !

Packet manager:
```sh
PM> NuGet\Install-Package LazySerializer -Version 1.1.0
```

.NET CLI:
```sh
> dotnet add package LazySerializer -Version 1.1.0
```

Paket CLI:
```sh
> paket add LazySerializer -Version 1.1.0
```


## Usage

Here is the object we will use for the serialization:

 ```C#
public class MySettings
{
    public string ConnectionName { get; set; }
    public bool Enabled { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public List<string> Paths;
}
```

We will save the object once to build the structure of the XML file. This will allow us to modify the object manually without worrying if the XML is valid or not.
We will remove this code later when the file is created.

 ```C#
// Build Settings
MySettings settings = new MySettings
{
    ConnectionName = "Production server",
    Enabled = true,
    Username = "admin",
    Password = "123456789",
    Server = "prod.server.com",
    Port = 8080,
    Paths = new List<string> {@"C:\Temp", @"\\quality.server.com\shared$"}
};

// Save
Serializer.Write("app.settings.xml", settings);
// or
settings.Serialize("app.settings.xml");
```

And.. ..here is the Settings file:

 ```XML
<?xml version="1.0" encoding="utf-8"?>
<MySettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Paths>
    <string>C:\Temp</string>
    <string>\\quality.server.com\shared$</string>
  </Paths>
  <ConnectionName>Production server</ConnectionName>
  <Enabled>true</Enabled>
  <Username>admin</Username>
  <Password>123456789</Password>
  <Server>prod.server.com</Server>
  <Port>8080</Port>
</MySettings>
```

Now to load the Settings we will use the  ``` Load ``` method:

 ```C#
MySettings mySettings = Serializer.Read<MySettings>("app.settings.xml");

// We can use the settings
// Server server = new Server(mySettings.Server, mySettings.Port);
// ...
```

## A simple serialization process

No secret, this library uses the following .NET methods to serialize and deserialize:

 Here is what it looks like if you want to copy/past:
 
 ### Serialize
 
 ```C#
 string path = "myfile.xml";
 YourObject obj = new YourObject();
 
 // ...
 
XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
using (StreamWriter streamWriter = new StreamWriter(path))
{
    xmlSerializer.Serialize(streamWriter, obj);
}
```
### Deserialize

 ```C#
string path = "myfile.xml";
YourObject obj;

XmlSerializer xmlSerializer = new XmlSerializer(typeof(YourObject));
using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
{
    YourObject = (YourObject)xmlSerializer.Deserialize(fileStream);
}
```

## Limitations

Based on the [MSDN](https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?redirectedfrom=MSDN&view=net-6.0), here is the limitation of the Serialization :

You must use:
* Public properties (read and write) & fields
* Public constructor (without args)
* No dictionnary
* All types to be known
* ...

License
----

MIT

![ForTheBadge built-with-love](http://ForTheBadge.com/images/badges/built-with-love.svg)
