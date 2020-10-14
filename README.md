Formly Schema Generation for .NET
=

![build](https://github.com/VyacheslavPritykin/FormlySchema.Generation.CSharp/workflows/build/badge.svg)

FormlySchema.Generation.CSharp is a .NET library to generate Formly Schema out of .NET types. 

Formly Schema is used by the [ngx-formly](https://github.com/ngx-formly/ngx-formly) project which is a dynamic (JSON powered) form library for Angular.

## Usage
```csharp
string formlySchema = Formly.Generate<Foo>().ToJson();
```

The documentation is a work in progress.