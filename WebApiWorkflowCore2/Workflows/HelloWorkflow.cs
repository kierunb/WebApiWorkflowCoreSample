using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebApiWorkflowCore2.Workflows;



public class FirstStep(ILogger<FirstStep> logger) : StepBody
{
    
    public string FirstName { get; set; } = string.Empty;

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        logger.LogInformation($"Executing first step for user {FirstName}");
        return ExecutionResult.Next(); // zawolanie kolejnego stepa
    }
}

public class SecondStep(ILogger<SecondStep> logger) : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        logger.LogInformation("Executing second step");
        return ExecutionResult.Next(); // zawolanie kolejnego stepa
    }
}

public class ThirdStep(ILogger<ThirdStep> logger) : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        logger.LogInformation("Executing third step");
        return ExecutionResult.Next(); // zawolanie kolejnego stepa
    }
}

public record class HelloWorkflowState
{
    public string Parametr { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}


public class HelloWorkflow : IWorkflow<HelloWorkflowState>
{
    public string Id => nameof(HelloWorkflow);

    public int Version => 1;

    public void Build(IWorkflowBuilder<HelloWorkflowState> builder)
    {
        builder
            .StartWith<FirstStep>()
                .Input(x => x.FirstName, y => y.UserName)   
            .Delay(d => TimeSpan.FromSeconds(10))
            .Then<SecondStep>()
            .Delay(d => TimeSpan.FromSeconds(10))
            .Delay(d => TimeSpan.FromSeconds(10))
            .Then<ThirdStep>();
    }
}
