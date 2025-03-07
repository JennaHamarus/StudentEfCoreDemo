using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEfCoreDemo.Controllers;
using StudentEfCoreDemo.Data;
using StudentEfCoreDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class StudentsControllerTests
{
    private StudentContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<StudentContext>()
            .UseInMemoryDatabase(databaseName: "StudentTestD")
            .Options;
        return new StudentContext(options);
    }

    [Fact]
    public async Task GetStudents_ReturnsEmptyList_WhenNoStudents()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new StudentsController(context);

        // Act
        var result = await controller.GetStudents();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Student>>>(result);
        var students = Assert.IsType<List<Student>>(actionResult.Value);
        Assert.Empty(students);
    }

    [Fact]
    public async Task GetStudent_ReturnsNotFound_WhenStudentDoesNotExist()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new StudentsController(context);

        // Act
        var result = await controller.GetStudent(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
   
}
