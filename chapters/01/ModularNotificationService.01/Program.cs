using ModularNotificationService._01.UseCases;

var registerUserUseCase = new RegisterUser();

try
{
    var input = new RegisterUserInput("John Doe", "john@doe.com");
    await registerUserUseCase.ExecuteAsync(input);

    Console.WriteLine("[Bootstrap] Proceso de registro finalizado correctamente.");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"[Bootstrap] Error en el sistema: {ex.Message}");
}