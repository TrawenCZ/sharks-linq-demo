using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PV178.Homeworks.HW03.Tests
{
    [TestClass]
    public class QueriesTest
    {
        private Queries? _queries;
        public Queries Queries => _queries ??= new Queries();

        [TestMethod]
        public void SampleQuery_ReturnsCorrectResult()
        {
            var expected = 624;
            var actual = Queries.SampleQuery();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void InfoAboutPeopleThatNamesStartsWithCAndWasInBahamasQuery_ReturnsExpected()
        {
            var expected = new List<string>
            {
                "male was attacked in Bahamas by Isurus oxyrinchus",
                "Bruce Johnson, rescuer was attacked in Bahamas by Isurus oxyrinchus",
                "Philip Sweeting was attacked in Bahamas by Isurus oxyrinchus",
                "Krishna Thompson was attacked in Bahamas by Isurus oxyrinchus",
                "Kevin King was attacked in Bahamas by Isurus oxyrinchus",
                "John Petty was attacked in Bahamas by Notorynchus cepedianus",
                "male was attacked in Bahamas by Isurus oxyrinchus",
                "Henry Kreckman was attacked in Bahamas by Notorynchus cepedianus",
                "Kerry Anderson was attacked in Bahamas by Notorynchus cepedianus",
                "Wolfgang Leander was attacked in Bahamas by Negaprion brevirostris",
                "Peter Albury was attacked in Bahamas by Notorynchus cepedianus",
                "Bruce Cease was attacked in Bahamas by Isurus oxyrinchus",
                "Robert Marx was attacked in Bahamas by Notorynchus cepedianus",
                "Michael Beach was attacked in Bahamas by Notorynchus cepedianus"
            };
            var actual = Queries.InfoAboutPeopleThatNamesStartsWithCAndWasInBahamasQuery();

            var expectedSorted = expected.OrderBy(s => s);
            var actualSorted = actual.OrderBy(s => s);

            Assert.IsTrue(expectedSorted.SequenceEqual(actualSorted));
        }

        [TestMethod]
        public void FortunateSharkAttacksSumWithinMonarchyOrTerritoryQuery_ReturnsExpected()
        {
            var actual = Queries.FortunateSharkAttacksSumWithinMonarchyOrTerritoryQuery();
            const int expected = 1157;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MostProlificNicknamesInCountriesQuery_ReturnsExpected()
        {
            var expected = new Dictionary<string, string>
            {
                { "Venezuela", "Broadnose sevengill shark" },
                { "Brazil", "White death" },
                { "Chile", "White death" },
                { "Ecuador", "White death" },
                { "Uruguay", "Shovelnose shark" },
                { "Guyana", "Raggedtooth shark" }
            };
            var actual = Queries.MostProlificNicknamesInCountriesQuery();

            Assert.IsTrue(SimpleDictionaryEquals(expected, actual));
        }

        [TestMethod]
        public void ThreeSharksOrderedByNumberOfAttacksOnMenQuery_ReturnsExpected()
        {
            var expected = new List<int>
            {
                1,
                11,
                17
            };
            var actual = Queries.ThreeSharksOrderedByNumberOfAttacksOnMenQuery();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void SwimmerAttacksSharkAverageSpeedQuery_ReturnsExpected()
        {
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
            var actual = Queries.SwimmerAttacksSharkAverageSpeedQuery();

            Assert.IsTrue(SimpleDictionaryEquals(expected, actual, DoublePrecisionEquals));
        }

        [TestMethod]
        public void NonFatalAttemptOfZambeziSharkOnPeopleBetweenDAndKQuery_ReturnsExpected()
        {
            var expected = new List<string>
            {
                "Dave Hamilton-Brown & Ant Rowan",
                "Kayak: Occupant Kelly Janse van Rensburg"
            };
            var actual = Queries.NonFatalAttemptOfZambeziSharkOnPeopleBetweenDAndKQuery();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void LightestSharksInSouthAmericaQuery_ReturnsExpected()
        {
            var expected = new Dictionary<string, List<int>>
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
            var actual = Queries.LightestSharksInSouthAmericaQuery();
            var actualDictionary = actual.ToDictionary(
                tuple => tuple.Item1,
                tuple => tuple.Item2.Select(species => species.Id).ToList()
            );

            Assert.IsTrue(ComplexDictionaryEquals(expected, actualDictionary, n => n));
        }

        [TestMethod]
        public void FiftySixMaxSpeedAndAgeQuery_ReturnsExpected()
        {
            var actual = Queries.FiftySixMaxSpeedAndAgeQuery();

            AssertBoolEqualsTrue(actual);
        }

        [TestMethod]
        public void InfoAboutPeopleAndCountriesOnBorRAndFatalAttacksQuery_ReturnsExpected()
        {
            var expected = new List<string>
            {
                "Alessandro Gomes de Souza was attacked in Brazil by Ginglymostoma cirratum",
                "Alexandre Rassiga was attacked in Reunion by Lamna ditropis",
                "Aylson Gadelha was attacked in Brazil by Carcharhinus brachyurus",
                "Boulabhaï Ishmael was attacked in Reunion by Notorynchus cepedianus",
                "Bruna Silva Gobbi was attacked in Brazil by Isurus oxyrinchus"
            };
            var actual = Queries.InfoAboutPeopleAndCountriesOnBorRAndFatalAttacksQuery();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void InfoAboutFinesInEuropeQuery_ReturnsExpected()
        {
            var expected = new List<string>
            {
                "Italy: 17900 EUR",
                "Croatia: 8900 HRK",
                "Greece: 6750 EUR",
                "France: 3150 EUR",
                "Ireland: 300 EUR"
            };
            var actual = Queries.InfoAboutFinesInEuropeQuery();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void FiveSharkNamesWithMostFatalitiesQuery_ReturnsExpected()
        {
            var expected = new Dictionary<string, int>
            {
                { "White shark", 283 },
                { "Hammerhead shark", 148 },
                { "Sevengill shark", 131 },
                { "Bronze whaler shark", 129 },
                { "Wobbegong shark", 126 },
            };
            var actual = Queries.FiveSharkNamesWithMostFatalitiesQuery();

            Assert.IsTrue(SimpleDictionaryEquals(expected, actual));
        }

        [TestMethod]
        public void StatisticsAboutGovernmentsQuery_ReturnsExpected()
        {
            var expected =
                "Republic: 59,9%, Monarchy: 18,6%, Territory: 15,8%, AutonomousRegion: 2,0%, ParliamentaryDemocracy: 1,6%, AdministrativeRegion: 0,8%, OverseasCommunity: 0,8%, Federation: 0,4%";
            var actual = Queries.StatisticsAboutGovernmentsQuery().Replace('.', ',');

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TigerSharkAttackZipQuery_ReturnsExpected()
        {
            var expected = new List<string>
            {
                "Jane Derry was tiggered in Unknown country",
                "Franz Mayer was tiggered in Cuba",
                "male was tiggered in India",
                "Madelaine Dalton was tiggered in Unknown country",
                "Michael Heidenreich was tiggered in Unknown country",
                "Lowell Lutz was tiggered in Unknown country"
            };
            var actual = Queries.TigerSharkAttackZipQuery();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void LongestVsShortestSharkQuery_ReturnsExpected()
        {
            var expected = "4,1% vs 8,0%";
            var actual = Queries.LongestVsShortestSharkQuery().Replace('.', ',');

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SafeCountriesQuery_ReturnsExpected()
        {
            const int expected = 167;
            var actual = Queries.SafeCountriesQuery();

            Assert.AreEqual(expected, actual);
        }

        #region TestHelperEqualityMethods

        private static bool DoublePrecisionEquals(double double1, double double2)
        {
            var difference = Math.Abs(double1 * .00001);

            return Math.Abs(double1 - double2) <= difference;
        }

        private static bool CheckTwoCollections<T1, T2>(IDictionary<T1, T2>? first, IDictionary<T1, T2>? second)
        {
            return first != null && second != null && first.Count == second.Count;
        }

        private static bool SimpleDictionaryEquals<T1, T2>(IDictionary<T1, T2> first, IDictionary<T1, T2> second,
            Func<T2, T2, bool> equalityCheck)
        {
            return CheckTwoCollections(first, second) &&
                   first.All(keyValuePair => second.ContainsKey(keyValuePair.Key) &&
                                             equalityCheck(keyValuePair.Value, second[keyValuePair.Key]));
        }

        private static bool SimpleDictionaryEquals<T1, T2>(IDictionary<T1, T2> first, IDictionary<T1, T2> second)
        {
            return SimpleDictionaryEquals(first, second, (a, b) => a != null && a.Equals(b));
        }

        private static bool ComplexDictionaryEquals<T1, T2, TOrderKey>(
            IDictionary<T1, List<T2>> first,
            IDictionary<T1, List<T2>> second,
            Func<T2, TOrderKey> valuesSortOrder)
        {
            return CheckTwoCollections(first, second) &&
                   first.All(keyValuePair =>
                   {
                       var (key, value) = keyValuePair;
                       return second.ContainsKey(key) &&
                              value
                                  .OrderBy(valuesSortOrder)
                                  .SequenceEqual(
                                      second[key].OrderBy(valuesSortOrder)
                                  );
                   });
        }

        private static bool ComplexDictionaryEquals<T1, T2, T3>(IDictionary<T1, IDictionary<T2, List<T3>>> first,
            IDictionary<T1, IDictionary<T2, List<T3>>> second) where T3 : class
        {
            if (!CheckTwoCollections(first, second))
            {
                return false;
            }

            for (var i = 0; i < first.Count; i++)
            {
                var firstInnerDictionary = first.ElementAt(i).Value;
                if (!second.Select(item => item.Key).Contains(first.ElementAt(i).Key))
                {
                    return false;
                }

                var secondInnerDictionary = second.First(item => item.Key.Equals(first.ElementAt(i).Key)).Value;
                if (firstInnerDictionary.Count != secondInnerDictionary.Count)
                {
                    return false;
                }

                for (var j = 0; j < firstInnerDictionary.Count; j++)
                {
                    var firstInnerList = firstInnerDictionary.ElementAt(j).Value;
                    if (!second.Select(item => item.Key).Contains(first.ElementAt(j).Key))
                    {
                        return false;
                    }

                    var secondInnerList = secondInnerDictionary
                        .First(item => item.Key.Equals(firstInnerDictionary.ElementAt(j).Key))
                        .Value;
                    if (firstInnerList.Count != secondInnerList.Count)
                    {
                        return false;
                    }

                    if (firstInnerList.Where((t, k) => !secondInnerList.Contains(firstInnerList.ElementAt(k))).Any())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void AssertBoolEqualsTrue(bool res)
        {
            Assert.AreEqual(true, res, "Actual result and the expected one are not equal.");
        }

        #endregion
    }
}
