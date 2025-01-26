using Microsoft.AspNetCore.Mvc;
using Moq;
using PruebaTecnica.Controllers;
using PruebaTecnica.Models.Services;
using PruebaTecnica.Models;
using PruebaTecnica.Models.DTOs;

public class UsersControllerTests
{
    private readonly Mock<UserService> _mockService; // Mock para simular el servicio de usuarios sin interactuar con la implementación real.
    private readonly UsersController _controller; // Controlador a probar.

    public UsersControllerTests()
    {
        // Configuración del mock y controlador antes de cada test.
        _mockService = new Mock<UserService>();
        _controller = new UsersController(_mockService.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult_WithAListOfUsers()
    {
        // Configuramos el mock para que retorne una lista predefinida de usuarios.
        var users = new List<User>
        {
            new User { Id = 1, Name = "Alice", UserName = "alice.test", Password = "test" },
            new User { Id = 2, Name = "Bob", UserName = "bob.test", Password = "test"  }
        };
        _mockService.Setup(service => service.GetUsers()).ReturnsAsync(users);

        // Ejecución del método del controlador.
        var result = await _controller.GetUsers();

        // Verificamos que el resultado sea un OkObjectResult y contenga una lista de usuarios.
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, returnValue.Count); // Validamos que se retornen exactamente 2 usuarios.
    }

    [Fact]
    public async Task GetUser_ReturnsOkResult_WhenUserExists()
    {
        // Configuramos el mock para que retorne un usuario específico si existe.
        var user = new User { Id = 1, Name = "Alice", UserName = "alice.test", Password = "test" };
        _mockService.Setup(service => service.GetUser(1)).ReturnsAsync(user);

        var result = await _controller.GetUser(1);

        // Verificamos que el resultado sea OkObjectResult y que los datos coincidan con el usuario esperado.
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user.Id, returnValue.Id);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Configuramos el mock para que retorne null cuando el usuario no exista.
        _mockService.Setup(service => service.GetUser(1)).ReturnsAsync((User)null);

        var result = await _controller.GetUser(1);

        // Validamos que se devuelva un resultado 404 NotFound.
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedAtActionResult()
    {
        // Configuramos el mock para que simule la creación de un usuario.
        var newUserDto = new CreateUserDTO
        {
            Name = "Alice",
            UserName = "alice.test",
            Password = "test"
        };

        _mockService.Setup(service => service.CreateUser(It.IsAny<CreateUserDTO>()))
            .ReturnsAsync(new User { Id = 1, Name = "Alice", UserName = "alice.test", Password = "test" });

        var result = await _controller.CreateUser(newUserDto);

        // Validamos que se devuelva un resultado 201 CreatedAtAction y que el usuario devuelto sea el esperado.
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<User>(createdResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal("Alice", returnValue.Name);
        Assert.Equal("alice.test", returnValue.UserName);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContentResult_WhenSuccessful()
    {
        // Configuramos el mock para simular una eliminación exitosa.
        _mockService.Setup(service => service.DeleteUser(1)).ReturnsAsync(true);

        var result = await _controller.DeleteUser(1);

        // Validamos que el resultado sea un 204 NoContent.
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Configuramos el mock para simular una eliminación fallida.
        _mockService.Setup(service => service.DeleteUser(1)).ReturnsAsync(false);

        var result = await _controller.DeleteUser(1);

        // Validamos que el resultado sea un 404 NotFound.
        Assert.IsType<NotFoundResult>(result);
    }
}
