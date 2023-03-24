using PV178.Homeworks.HW03;

Console.WriteLine("Welcome to HW03, you can use this file as a playground for manually testing your solution.");

Queries queries = new Queries();

var expected = new Dictionary<string, double>
            {
                { "Asia", 48.91 },
                { "North America", 43.58 },
                { "Australia", 47.32 },
                { "Africa", 44.68 },
                { "Europe", 49.44 },
                { "Central America", 50.8 },
                { "South America", 46.19 },
                { "Oceania", 45.14 }
            };
var actual = queries.SwimmerAttacksSharkAverageSpeedQuery();

for (int i = 0; i < expected.Count; i++)
{
    Console.WriteLine($"Expected: {expected.ElementAt(i).Key} - {expected.ElementAt(i).Value}");
    Console.WriteLine($"Actual: {actual.ElementAt(i).Key} - {actual.ElementAt(i).Value}");
}


var expected2 = new List<string>
            {
                "Dave Hamilton-Brown & Ant Rowan",
                "Kayak: Occupant Kelly Janse van Rensburg"
            };
var actual2 = queries.NonFatalAttemptOfZambeziSharkOnPeopleBetweenDAndKQuery();

Console.WriteLine(actual2.Count);

for (int i = 0; i < expected2.Count; i++)
{
    Console.WriteLine($"Expected: {expected2.ElementAt(i)}");
    Console.WriteLine($"Actual: {actual2.ElementAt(i)}");
}