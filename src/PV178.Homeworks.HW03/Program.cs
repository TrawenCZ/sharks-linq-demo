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

var expected3 = new Dictionary<string, List<int>>
            {
                { "Argentina", new List<int> { 15 } },
                { "Bolivia", new List<int>() },
                { "Brazil", new List<int> { 17, 14, 4, 12, 13, 2, 15, 7 } },
                { "Chile", new List<int> { 3, 4, 7 } },
                { "Ecuador", new List<int> { 17, 13 } },
                { "Falkland Islands", new List<int>() },
                { "French Guiana", new List<int>() },
                { "Guyana", new List<int> { 4 } },
                { "Colombia", new List<int>() },
                { "Paraguay", new List<int>() },
                { "Peru", new List<int>() },
                { "Suriname", new List<int>() },
                { "Uruguay", new List<int> { 13 } },
                { "Venezuela", new List<int> { 17, 3, 15 } }
            };
var actual3 = queries.LightestSharksInSouthAmericaQuery();

for (int i = 0; i < expected3.Count; i++)
{
    Console.WriteLine($"Expected: {expected3.ElementAt(i).Key} -");
    foreach (var item in expected3.ElementAt(i).Value)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine($"Actual: {actual3.ElementAt(i).Item1} - ");
    foreach (var item in actual3.ElementAt(i).Item2)
    {
        Console.WriteLine(item);
    }
}

var expected4 = new List<string>
            {
                "Alessandro Gomes de Souza was attacked in Brazil by Ginglymostoma cirratum",
                "Alexandre Rassiga was attacked in Reunion by Lamna ditropis",
                "Aylson Gadelha was attacked in Brazil by Carcharhinus brachyurus",
                "Boulabhaï Ishmael was attacked in Reunion by Notorynchus cepedianus",
                "Bruna Silva Gobbi was attacked in Brazil by Isurus oxyrinchus"
            };
var actual4 = queries.InfoAboutPeopleAndCountriesOnBorRAndFatalAttacksQuery();

for (int i = 0; i < expected4.Count; i++)
{
    Console.WriteLine($"Expected: {expected4.ElementAt(i)}");
    Console.WriteLine($"Actual: {actual4.ElementAt(i)}");
}