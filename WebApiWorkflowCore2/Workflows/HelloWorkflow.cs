using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebApiWorkflowCore2.Workflows;



public class FirstStep(ILogger<FirstStep> logger) : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        logger.LogInformation("Executing first step");
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

public class HelloWorkflow : IWorkflow
{
    public string Id => nameof(HelloWorkflow);

    public int Version => 1;

    public void Build(IWorkflowBuilder<object> builder)
    {
        builder
            .StartWith<FirstStep>()
            .Delay(d => TimeSpan.FromSeconds(10))
            .Then<SecondStep>()
            .Delay(d => TimeSpan.FromSeconds(10))
            .Then<ThirdStep>();
    }
}
