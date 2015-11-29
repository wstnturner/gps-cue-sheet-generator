http://sqlite.phxsoftware.com/

you will get an error trying to run under VS10 because the SQLite library was built for .Net 2.0.x in VS10 uses .Net 4.x. In order to fix this, you must add

```
useLegacyV2RuntimeActivationPolicy="true"
```

to the 

&lt;startup&gt;

 tag in the application's .exe.config file (Pathfinder.exe.config in our case), like so:

```
<?xml version="1.0"?>
<configuration>
<startup useLegacyV2RuntimeActivationPolicy="true"><supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/></startup></configuration>
```

And as it turns out, using SQLite for the geocoding cache mechanism is not particularly fast. I'm sure it could be further optimized, but right now it is not nearly as fast as the red/black tree cache.