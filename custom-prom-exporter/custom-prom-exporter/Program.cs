// See https://aka.ms/new-console-template for more information
using Prometheus;
using System.Reflection.Metadata.Ecma335;

Console.WriteLine("Hello, World!");
// Start kestrel server to publish /metrics endpoint
// https://github.com/prometheus-net/prometheus-net?tab=readme-ov-file#kestrel-stand-alone-server

var metricServer = new KestrelMetricServer(port: 1234, url: "/metrics");
metricServer.Start();

// Mock an API Call
//var result = new HttpClient().GetAsync("http://myapi.com");
// Example Result
// {
//   customerId: 56,
//   customerName: "BobsCafe"
// }
 
// Implement a custom counter metric
// https://github.com/prometheus-net/prometheus-net?tab=readme-ov-file#counters
Counter myCustomCounter = Metrics.CreateCounter("myapp_loops_total", "Number of loops this process has done");

// Implement a custom gauge metric
// https://github.com/prometheus-net/prometheus-net?tab=readme-ov-file#gauges
Gauge myCustomGauge = Metrics.CreateGauge("myapp_random_number", "Randomly defined number");

Gauge customer_info = Metrics.CreateGauge("customer_info", "Mapping of Customer ID to Customer Name", labelNames: new[] { "customer_id", "customer_name" });

customer_info.WithLabels("{customerId}", "{customerName").Set(1);

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
