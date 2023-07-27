using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Extension;
using Tekla.Extension.Services;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace TeklaTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setting up an antivirus friendly config.
            var config = DefaultConfig.Instance
                .AddJob(Job
                     .MediumRun
                     .WithLaunchCount(1)
                     .WithToolchain(InProcessNoEmitToolchain.Instance));
            var summary = BenchmarkRunner.Run<ConverterBenchmark>(config);
        }
    }
}
