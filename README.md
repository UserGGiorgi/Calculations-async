# Calculations (Async)

An advanced level task for practicing asynchronous programming for CPU bound operations.

In this task you
- will learn the basics of asynchronous programming;
- will get acquainted with [`Task`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-6.0) and [`Task<TResult`>](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1?view=net-6.0) classes from [System.Threading.Tasks](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks?view=net-6.0) namespace;
- will learn how to use `async/await` key words in asynchronous programming. 

Before starting the task learn how to use `Task`/`Task<TResult>` classes and `async/await` key words in C#.

Estimated time to complete the task: 6h.


## Task Description

 - Read the articles
    - [Asynchronous programming with async and await](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/).
    - [Your Ultimate async / await Tutorial in C#](https://www.codingame.com/playgrounds/4240/your-ultimate-async-await-tutorial-in-c/introduction). 
    - [Async and Await](https://blog.stephencleary.com/2012/02/async-and-await.html).
- Implement a `CalculateSum` method that calculate the sum from `1` to `n` synchronously.
- Implement a `CalculateSumAsync` method that calculate the sum from `1` to `n` asynchronously.
- Complete console application witch calculate asynchronously sum from `1` to `n`. The value of `n` is set by the user from the console. The user has the right to change the boundary `n` in the course of calculations, which should lead to a restart of the calculation, this should not crash the application. After receiving the result, be able to continue calculations without leaving the console.
 - Compare synchronous and asynchronous implementations of the methods. Make inferences about usage preference.
