namespace UserService.Application.UseCases.Commands;

public class CreateHandler
{
    public async Task<string> HandleAsync()
    {
        await Task.Delay(2000);
        
        return "Hello World!";
    }
}