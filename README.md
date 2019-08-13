# StandardMapper

StandardMapper is a simple objects/models mapper you can use to tranfer the value of the properties of an object/model into another object/model. The mapping process is based
on the properties names, that are must be the same between the two objects/models to map.

**Language/Technologies**

StandardMapper is written in C#. It is based on NETStandard 2.0.3, then you can use it with different platforms. (https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

**Usage**

```cs
using StandardMapper;
..
..
// SAMPLE OBJECTS
Class1 object1 = new Class1 { Name = "John", Surname = "Doe" };

public class Class2 { 
  public string Name { get; set; }
  public string Surname { get; set; }
}

Mapper simpleMapper = new Mapper();
simpleMapper.OutMap(object1, out Class2 object2);  // result: object2.Name = "John", object2.Surname = "Doe";

// OBJECTS WITH ENUMERABLES, LISTS, ECC...
Class3 object3 = new Class3 { Name = "John", Friends = new string[] { "Michael", "Mary" }};

public class Class4 {
  public string Name { get; set; }
  public List<string> Friends { get; set; }
}

Mapper listMapper = new Mapper();
listMapper.OutMap(object3, out Class4 object4);  // result: object4.Name = "John", object4.Friends = new List<string> { "Michael", "Mary" }

// EXCLUDE PROPERTIES FROM MAPPING
Class5 object5 = new Class5 { Name = "John", Surname = "Doe", Age = 33 };

public class Class6 { 
  public string Name { get; set; }
  public string Surname { get; set; }
  public int Age { get; set; }
}

Mapper exceptionsMapper = new Mapper();
exceptionsMapper.OutMap(object5, out Class6 object6, new string[] { "Age" });     // result: object6.Name = "John", object6.Surname = "Doe", object6.Age = 0;
..
..
..
```

You can also use "Map" method, without "out" parameter:

```cs
Class1 object1 = new Class1 { Name = "John", Surname = "Doe" };

public class Class2 { 
  public string Name { get; set; }
  public string Surname { get; set; }
}

Class2 object2 = new Class2();

Mapper simpleMapper = new Mapper();
simpleMapper.Map(object1, object2);  // result: object2.Name = "John", object2.Surname = "Doe";
```

**Limitations**

StandardMapper doesn't currently support the mapping of dictionaries. This feature will added to the project in the next future. 

**Installation**

StandardMapper is on NuGet (https://www.nuget.org/packages/StandardMapper).

From .NET Core command line:

```Shell
    dotnet add package StandardMapper
```

From Visual Studio package manager console:

```PowerShell
    PM> Install-Package StandardMapper
```

Alternatively, you can use the equivalent command on Visual Studio UI.

**License**

StandardMapper is an open source project released under **MIT** license.