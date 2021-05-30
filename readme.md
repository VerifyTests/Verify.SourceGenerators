# <img src="/src/icon.png" height="30px"> Verify.AngleSharp

[![Build status](https://ci.appveyor.com/api/projects/status/ff4ms9mevndkui7l?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-SourceGenerators)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.SourceGenerators.svg)](https://www.nuget.org/packages/Verify.SourceGenerators/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of C# Source Generators.

<a href='https://dotnetfoundation.org' alt='Part of the .NET Foundation'><img src='https://raw.githubusercontent.com/VerifyTests/Verify/master/docs/dotNetFoundation.svg' height='30px'></a><br>
Part of the <a href='https://dotnetfoundation.org' alt=''>.NET Foundation</a>


## NuGet package

https://nuget.org/packages/Verify.SourceGenerators/


## Initialize

Call `VerifySourceGenerators.Enable()` once at assembly load time.



        //https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#unit-testing-of-generators
