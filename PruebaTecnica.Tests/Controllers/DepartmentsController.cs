using Microsoft.AspNetCore.Mvc;
using Moq;
using PruebaTecnica.Controllers;
using PruebaTecnica.Models.Services;
using PruebaTecnica.Models;

public class DepartmentsControllerTests
{
    private readonly Mock<DepartmentService> _mockService; // Se usa un mock para simular el servicio de departamentos.
    private readonly DepartmentsController _controller; // Instancia del controlador que se va a probar.

    public DepartmentsControllerTests()
    {
        // Inicialización del mock y del controlador con la inyección del mock.
        _mockService = new Mock<DepartmentService>();
        _controller = new DepartmentsController(_mockService.Object);
    }

    [Fact]
    public async Task GetDepartments_ReturnsOkResult_WithAListOfDepartments()
    {
        // Arrange: Se configura el mock para devolver una lista predefinida de departamentos.
        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "IT" }
        };
        _mockService.Setup(service => service.GetDepartments()).ReturnsAsync(departments);

        // Act: Llamada al método GetDepartments del controlador.
        var result = await _controller.GetDepartments();

        // Assert: Se verifica que el resultado sea un OkObjectResult y que contenga una lista de departamentos.
        var okResult = Assert.IsType<OkObjectResult>(result); // El resultado debe ser un 200 OK.
        var returnValue = Assert.IsType<List<Department>>(okResult.Value); // El valor devuelto debe ser una lista de departamentos.
        Assert.Equal(2, returnValue.Count); // Validamos que la lista contenga exactamente dos elementos.
    }

    [Fact]
    public async Task GetDepartment_ReturnsOkResult_WhenDepartmentExists()
    {
        // Arrange: Configuramos el mock para que retorne un departamento específico si existe.
        var department = new Department { Id = 1, Name = "HR" };
        _mockService.Setup(service => service.GetDepartment(1)).ReturnsAsync(department);

        // Act: Llamada al método GetDepartment del controlador con un ID existente.
        var result = await _controller.GetDepartment(1);

        // Assert: Verificamos que el resultado sea un OkObjectResult y que el departamento retornado sea el esperado.
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Department>(okResult.Value);
        Assert.Equal(department.Id, returnValue.Id); // Verificamos que el ID devuelto coincida con el esperado.
    }

    [Fact]
    public async Task GetDepartment_ReturnsNotFound_WhenDepartmentDoesNotExist()
    {
        // Arrange: Configuramos el mock para que retorne null cuando no se encuentre el departamento.
        _mockService.Setup(service => service.GetDepartment(1)).ReturnsAsync((Department)null);

        // Act: Llamada al método GetDepartment con un ID inexistente.
        var result = await _controller.GetDepartment(1);

        // Assert: Validamos que el resultado sea un 404 NotFound.
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateDepartment_ReturnsCreatedAtActionResult()
    {
        // Arrange: Configuramos el mock para simular la creación de un departamento.
        var newDepartment = new Department { Id = 1, Name = "HR" };
        _mockService.Setup(service => service.CreateDepartment(It.IsAny<Department>())).ReturnsAsync(newDepartment);

        // Act: Llamada al método CreateDepartment con un nuevo departamento.
        var result = await _controller.CreateDepartment(newDepartment);

        // Assert: Verificamos que el resultado sea un CreatedAtActionResult y que el departamento devuelto sea el esperado.
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Department>(createdResult.Value);
        Assert.Equal(newDepartment.Id, returnValue.Id); // Verificamos que el ID coincida.
    }

    [Fact]
    public async Task DeleteDepartment_ReturnsNoContentResult_WhenSuccessful()
    {
        // Arrange: Configuramos el mock para simular una eliminación exitosa.
        _mockService.Setup(service => service.DeleteDepartment(1)).ReturnsAsync(true);

        // Act: Llamada al método DeleteDepartment con un ID existente.
        var result = await _controller.DeleteDepartment(1);

        // Assert: Verificamos que el resultado sea un NoContentResult (204 No Content).
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteDepartment_ReturnsNotFound_WhenDepartmentDoesNotExist()
    {
        // Arrange: Configuramos el mock para simular un intento fallido de eliminación.
        _mockService.Setup(service => service.DeleteDepartment(1)).ReturnsAsync(false);

        // Act: Llamada al método DeleteDepartment con un ID inexistente.
        var result = await _controller.DeleteDepartment(1);

        // Assert: Verificamos que el resultado sea un NotFoundResult (404 Not Found).
        Assert.IsType<NotFoundResult>(result);
    }
}
