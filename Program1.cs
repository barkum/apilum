using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using q3;

var serviceProvider = new ServiceCollection().AddDbContext<SchoolContext>(options => options.UseInMemoryDatabase("SchoolDB")).BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SchoolContext>();

    context.Database.EnsureCreated();

    var studentManager = new StudentManager(context);

    studentManager.AddStudent(new Student {Id = 5, Name = "test", Grade = 23});

    var students = studentManager.GetAllStudents();


    foreach (var student in students)

    {
        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Grade: {student.Grade}");
    }
}