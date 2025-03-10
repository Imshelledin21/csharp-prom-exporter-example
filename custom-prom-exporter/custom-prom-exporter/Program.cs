﻿// See https://aka.ms/new-console-template for more information
using Prometheus;
using System.Reflection.Metadata.Ecma335;

Console.WriteLine("Hello, World!");
// Start kestrel server to publish /metrics endpoint
// https://github.com/prometheus-net/prometheus-net?tab=readme-ov-file#kestrel-stand-alone-server

var metricServer = new KestrelMetricServer(port: 1234, url: "/metrics");
metricServer.Start();


// Implement a custom counter metric
// https://github.com/prometheus-net/prometheus-net?tab=readme-ov-file#counters
Counter myCustomCounter = Metrics.CreateCounter("myapp_loops_total", "Number of loops this process has done");

// Implement a custom gauge metric
// https://github.com/prometheus-net/prometheus-net?tab=readme-ov-file#gauges
Gauge myCustomGauge = Metrics.CreateGauge("myapp_random_number", "Randomly defined number");

Console.WriteLine("Entering Loop");
while (true)
{
    Random random = new Random();
    // Increment Counter Metric
    myCustomCounter.Inc();

    // Change Gauge Metric
    myCustomGauge.Set(random.Next(0, 100));

    Console.WriteLine("Metrics Updated");
    System.Threading.Thread.Sleep(60000);
}
