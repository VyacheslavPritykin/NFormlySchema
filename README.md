NFormlySchema for .NET
=

![build](https://github.com/VyacheslavPritykin/FormlySchema.Generation.CSharp/workflows/build/badge.svg)

NFormlySchema is a .NET library generate Formly Schema. 

Formly Schema is used by the [ngx-formly](https://github.com/ngx-formly/ngx-formly) project which is a dynamic (JSON powered) form library for Angular.

## Usage
```csharp
string formlySchema = FormlySchema.FromType<Foo>().ToJson();
```

validators

The documentation is a work in progress.