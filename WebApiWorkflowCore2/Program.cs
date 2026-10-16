using Scalar.AspNetCore;
using System.Reflection;
using WebApiWorkflowCore2.Workflows;
using WorkflowCore.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddWorkflow();

// Register all steps in the assembly
var steps = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetInterfaces().Contains(typeof(IStepBody)) && t.IsClass);

foreach (var step in steps)
{
    builder.Services.AddTransient(step);
}

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapGet("start-hello-workflow", async (IWorkflowHost host) => {
    string workflowId = await host.StartWorkflow(nameof(HelloWorkflow));
    return Results.Ok($"Workflow id: {workflowId}");
});


// Uruchominie hosta workflows

var workflowHost = app.Services.GetService<IWorkflowHost>();

// zarejestrowac wszystkie workflowy
workflowHost?.RegisterWorkflow<HelloWorkflow>();

workflowHost?.Start();  // startujemy host

app.Run();
