Video-demo of the application can be found here: https://www.loom.com/share/7b0b7eb2e3294a0c9f3204554a1c6893

MY LOCAL ENVIRONMENT:

OS: Windows 10 Version 22H2 (OS Build 19045.4780)

IDE: Microsoft Visual Studio Community 2022 (64-bit) - Current
Version 17.11.0

Docker: Version 27.1.1, build 6312585

DB: PostgreSQL 16.4, compiled by Visual C++ build 1940, 64-bit

HOW TO RUN THE APPLICATION:

NOTE!
Next on the list is the implication that it is running on the windows operating system!

1. Restore orders and users DB's using backup from SolutionItems folder. 
2. Change the connection string to one that would be relevant to your database connection.
3. Run docker application.
4. Open Windows PowerShell and navigate to the folder where docker-compose.yml file is located (it was added in SolutionItems folder).
5. execute the following command -> "docker-compose up --build" to build and run the application


Technical notes:

1. I use Clean Architecture approach to design current application (greetings from Robert C. Martin (Uncle Bob))
 -> https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html
Clean Architecture provides a robust framework for building software systems that are maintainable, scalable, and adaptable. By emphasizing separation of concerns, independence from frameworks, and testability, it allows to create high-quality applications that can evolve with changing business needs.

2. As a communication technology I use RabbitMQ  -> https://www.rabbitmq.com

3. ORM (Entity Framework) -> https://learn.microsoft.com/en-us/aspnet/entity-framework

4. I use Command Query Responsibility Segregation (CQRS) approach to deal with the app requests. CQRS offers a powerful architectural pattern that enhances scalability, performance, and maintainability in complex applications. By separating read and write responsibilities, it allows for optimized data handling, improved security, and better alignment with evolving business needs.

5. To deal with the mapping from/to  Model-Dto I use AutoMapper package -> https://automapper.org

6. To reduce dependencies beetween objects I use MediatR pattern and it's simple implication ->  https://www.nuget.org/packages/MediatR

7. To validate the models I used the package -> https://www.nuget.org/packages/FluentValidation


p.s.
I understand that it was possible to add postgres sql image in docker-compose, but I was too lazy to do it) 