using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Tekla.Extension;
using Tekla.Extension.Services;

namespace TeklaTest
{
    public class ConverterBenchmark
    {
        string[] BenchmarkInput;

        [GlobalSetup]
        public void Setup()
        {
            int InputMultiplier = 1000;
            string[] EnumStrings = new string[]
            {
            "B",
            "I",
            "L",
            "U",
            "RU",
            "RO",
            "M",
            "C",
            "T",
            "Z"
            };
            string[] result = EnumStrings;
            for (int i = 0; i < InputMultiplier; i++)
            {
                result = result.Concat(EnumStrings).ToArray();
            }
            BenchmarkInput = result;
        }

        [Benchmark]
        public void PartProfileSwitchConverterBM()
        {
            var converterTest = BenchmarkInput
                .Select(str => PartProfileSwitchConverter.GetProfileTypeFromString(str))
                .ToList();
            var stringBack = converterTest
                .Select(en => PartProfileSwitchConverter.GetStringValueFromProfileType(en))
                .ToList();
        }

        [Benchmark]
        public void PartProfileConverterBM()
        {
            var converterTest = BenchmarkInput
                .Select(str => PartProfileConverter.GetProfileTypeFromString(str))
                .ToList();
            var stringBack = converterTest
                .Select(en => PartProfileConverter.GetStringValueFromProfileType(en))
                .ToList();
        }

        [Benchmark]
        public void EnumStringConverterBM()
        {
            var GenConverter = new EnumStringConverter<ProfileType>();
            var converterTestGeneric = BenchmarkInput
                .Select(str => GenConverter.GetEnumFromString(str))
                .ToList();

            var stringBackGeneric = converterTestGeneric
                .Select(en => GenConverter.GetStringValueFromEnum(en))
                .ToList();
        }

        [Benchmark]
        public void StaticEnumConverterBM()
        {
            var converterTestGeneric = BenchmarkInput
                .Select(str => StaticEnumConverter.GetEnumFromString<ProfileType>(str))
                .ToList();

            var stringBackGeneric = converterTestGeneric
                .Select(en => StaticEnumConverter.GetStringValueFromEnum(en))
                .ToList();
        }

        [Benchmark]
        public void StaticEnumConverterWithCashingBM()
        {
            var converterTestGeneric = BenchmarkInput
                .Select(str => StaticEnumConverterWithCashing.GetEnumFromString<ProfileType>(str))
                .ToList();

            var stringBackGeneric = converterTestGeneric
                .Select(en => StaticEnumConverterWithCashing.GetStringValueFromEnum(en))
                .ToList();
        }
    }
}
