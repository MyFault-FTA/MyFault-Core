# MyFault-Core
.NET Fault Tracking &amp; Analytics

Dead-simple fault tracking system designed for .Net applications. 
Quickly log exceptions, errors and fail-states that can easily be traced back to
a single line of code. Aggregate instances and avoid searching through long error logs.
Filter, analyize and report on code faults with an easy to use REST API.

**Getting Started**

```csharp
//Easy to configure
MyFaultConfig.CurrentDefaults.WithMsSqlData("Data Source=APPDB;Integrated Security=True;Database=MYFAULT");
            
try
{
    throw new ApplicationException("New Exception");
}
catch (Exception exception)
{
    var state = new {Message = "Somethign went wrong", Type="Error"};
    
    MyFaultHandler.Current.New().AsException(exception)
        .Having("TestKey", "TestVal") // Store key-value pairs
        .Having(() => state) // Store objects (ViewModels, Dtos, anything)
          // Store images, trace logs, uploaded files, anything that might help
        .Having("TraceLog", "C:\\TraceLog.log", "applications/text")
    .Handle();
}
```
