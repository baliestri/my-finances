await WebApplication
  .CreateBuilder(args)
  .RegisterComponents()
  .RegisterPipelines()
  .RunAsync();