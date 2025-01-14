using System;
using System.Diagnostics;
using System.Collections.Generic;

int correctAnswers = 0;
int incorrectAnswers = 0;
int noAnswers = 0;
int timeIntervalSeconds = 30;

int totalAnswers;

Console.WriteLine("Welcome to the multiplication task game!\n");
Console.WriteLine("How many tasks do you want: ");
string input = Console.ReadLine();
if (!int.TryParse(input, out totalAnswers))
{
    Console.WriteLine("Invalid input. Please enter a valid number.");
    return;
}

Console.WriteLine("How many seconds for each task: ");
input = Console.ReadLine();
if (!int.TryParse(input, out timeIntervalSeconds))
{
    Console.WriteLine("Invalid input. Please enter a valid number.");
    return;
}

Stopwatch gameStopwatch = new Stopwatch();
gameStopwatch.Start();

var tasks = new List<(int, int, int?, int, string)>();
var random = new Random();

for (int i = 1; i <= totalAnswers; i++)
{

    int correctResult = random.Next(2, 10);
    int num1 = random.Next(2, 10);
    int num2 = correctResult * num1;

    Console.WriteLine($"Task {i}: What is {num2} / {num1}?");

    var stopwatch = new Stopwatch();
    stopwatch.Start();
    string userInput = null;
    
    while (stopwatch.Elapsed.TotalSeconds <= timeIntervalSeconds && string.IsNullOrEmpty(userInput))
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

gameStopwatch.Stop();

// Summary
Console.WriteLine("\nGame Over! Here's the summary:");
Console.WriteLine($"Total Tasks: {totalAnswers}");
Console.WriteLine($"Correct Answers: {correctAnswers}");
Console.WriteLine($"Incorrect Answers: {incorrectAnswers}");
Console.WriteLine($"No Answers: {noAnswers}\n");
Console.WriteLine($"Total Time Taken: {gameStopwatch.Elapsed.TotalSeconds} seconds");

Console.WriteLine("Detailed Report:");
for (int i = 0; i < tasks.Count; i++)
{
    var task = tasks[i];
    Console.WriteLine($"Task {i + 1}: {task.Item1} x {task.Item2} = {task.Item4}, Your Answer: {task.Item3}, Status: {task.Item5}");
}
