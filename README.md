Benchmark results:

BenchmarkDotNet v0.13.6, Windows 11 (10.0.22621.1992/22H2/2022Update/SunValley2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
  [Host] : .NET Framework 4.8.1 (4.8.9167.0), X86 LegacyJIT

Job=MediumRun  Toolchain=InProcessNoEmitToolchain  IterationCount=15
LaunchCount=1  WarmupCount=10

|                           Method |         Mean |       Error |      StdDev |
|--------------------------------- |-------------:|------------:|------------:|
|     PartProfileSwitchConverterBM |     198.9 us |     1.89 us |     1.76 us |
|           PartProfileConverterBM |     358.1 us |     3.64 us |     3.23 us |
|            EnumStringConverterBM |     600.4 us |     7.79 us |     7.29 us |
|            StaticEnumConverterBM | 103,318.7 us | 1,528.06 us | 1,429.35 us |
| StaticEnumConverterWithCashingBM |     813.6 us |    11.44 us |    10.70 us |
