using System;
using System.Diagnostics;
using System.Collections.Generic;

int correctAnswers = 0;
int incorrectAnswers = 0;
int noAnswers = 0;

Console.WriteLine("Welcome to the multiplication task game!\n");
Console.WriteLine("You will be given 20 tasks to calculate the multiplication of two numbers between 1 and 9.");
Console.WriteLine("You have 7 seconds to answer each task. If you don't answer within this time, it will be considered incorrect.\n");

var tasks = new List<(int, int, int?, int, string)>();
var random = new Random();

for (int i = 1; i <= 20; i++)
{
    int num1 = random.Next(2, 10);
    int num2 = random.Next(2, 10);
    int correctResult = num1 * num2;

    Console.WriteLine($"Task {i}: What is {num1} x {num2}?");

    var stopwatch = new Stopwatch();
    stopwatch.Start();
    string userInput = null;
    
    while (stopwatch.Elapsed.TotalSeconds <= 30 && string.IsNullOrEmpty(userInput))
    {
        if (Console.KeyAvailable)
        {
            userInput = Console.ReadLine();
            break;
        }
    }
    stopwatch.Stop();

    if (string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("Time's up! Moving to the next task.");
        tasks.Add((num1, num2, null, correctResult, "No Answer"));
        noAnswers++;
        continue;
    }

    try
    {
        int userAnswer = int.Parse(userInput);

        if (userAnswer == correctResult)
        {
            Console.WriteLine("Correct!");
            tasks.Add((num1, num2, userAnswer, correctResult, "Correct"));
            correctAnswers++;
        }
        else
        {
            Console.WriteLine($"Incorrect. The correct answer was {correctResult}.");
            tasks.Add((num1, num2, userAnswer, correctResult, "Incorrect"));
            incorrectAnswers++;
        }
    }
    catch
    {
        Console.WriteLine("Invalid input detected. Moving to the next task.");
        tasks.Add((num1, num2, null, correctResult, "No Answer"));
        noAnswers++;
    }
}

// Summary
Console.WriteLine("\nGame Over! Here's the summary:");
Console.WriteLine($"Total Tasks: 20");
Console.WriteLine($"Correct Answers: {correctAnswers}");
Console.WriteLine($"Incorrect Answers: {incorrectAnswers}");
Console.WriteLine($"No Answers: {noAnswers}\n");

Console.WriteLine("Detailed Report:");
for (int i = 0; i < tasks.Count; i++)
{
    var task = tasks[i];
    Console.WriteLine($"Task {i + 1}: {task.Item1} x {task.Item2} = {task.Item4}, Your Answer: {task.Item3}, Status: {task.Item5}");
}
