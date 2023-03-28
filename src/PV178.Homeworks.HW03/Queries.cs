using PV178.Homeworks.HW03.DataLoading.DataContext;
using PV178.Homeworks.HW03.DataLoading.Factory;
using PV178.Homeworks.HW03.Model;
using PV178.Homeworks.HW03.Model.Enums;
using System.Linq;
using System.Text;

namespace PV178.Homeworks.HW03
{
    public class Queries
    {
        private IDataContext? _dataContext;
        public IDataContext DataContext => _dataContext ??= new DataContextFactory().CreateDataContext();

        /// <summary>
        /// Ukážkové query na pripomenutie základnej LINQ syntaxe a operátorov. Výsledky nie je nutné vracať
        /// pomocou jedného príkazu, pri zložitejších queries je vhodné si vytvoriť pomocné premenné cez `var`.
        /// Toto query nie je súčasťou hodnotenia.
        /// </summary>
        /// <returns>The query result</returns>
        public int SampleQuery()
        {
            return DataContext.Countries
                .Where(a => a.Name?[0] >= 'A' && a.Name?[0] <= 'G')
                .Join(DataContext.SharkAttacks,
                    country => country.Id,
                    attack => attack.CountryId,
                    (country, attack) => new { country, attack }
                )
                .Join(DataContext.AttackedPeople,
                    ca => ca.attack.AttackedPersonId,
                    person => person.Id,
                    (ca, person) => new { ca, person }
                )
                .Where(p => p.person.Sex == Sex.Male)
                .Count(a => a.person.Age >= 15 && a.person.Age <= 40);
        }

        /// <summary>
        /// Úloha č. 1
        ///
        /// Vráťte zoznam, v ktorom je textová informácia o každom človeku,
        /// na ktorého v štáte Bahamas zaútočil žralok s latinským názvom začínajúcim sa 
        /// na písmeno I alebo N.
        /// 
        /// Túto informáciu uveďte v tvare:
        /// "{meno človeka} was attacked in Bahamas by {latinský názov žraloka}"
        /// </summary>
        /// <returns>The query result</returns>
        public List<string> InfoAboutPeopleThatNamesStartsWithCAndWasInBahamasQuery()
        {
            return DataContext.SharkSpecies.Where(specie => specie.LatinName?[0] == 'I' || specie.LatinName?[0] == 'N')
                .Join(DataContext.SharkAttacks,
                species => species.Id,
                attack => attack.SharkSpeciesId,
                (species, attack) => new { species, attack })
                .Join(DataContext.Countries.Where(country => country.Name!.Equals("Bahamas")),
                speciesAttack => speciesAttack.attack.CountryId,
                countries => countries.Id,
                (speciesAttack, countries) => new { speciesAttack, countries })
                .Join(DataContext.AttackedPeople,
                speciesAttackCountry => speciesAttackCountry.speciesAttack.attack.AttackedPersonId,
                person => person.Id,
                (speciesAttackCountry, person) => new { speciesAttackCountry, person })
                .Select(x => $"{x.person.Name} was attacked in Bahamas by {x.speciesAttackCountry.speciesAttack.species.LatinName}")
                .ToList();
        }

        /// <summary>
        /// Úloha č. 2
        ///
        /// Firma by chcela expandovať do krajín s nedemokratickou formou vlády – monarchie alebo teritória.
        /// Pre účely propagačnej kampane by chcela ukázať, že žraloky v týchto krajinách na ľudí neútočia
        /// s úmyslom zabiť, ale chcú sa s nimi iba hrať.
        /// 
        /// Vráťte súhrnný počet žraločích utokov, pri ktorých nebolo preukázané, že skončili fatálne.
        /// 
        /// Požadovany súčet vykonajte iba pre krajiny s vládnou formou typu 'Monarchy' alebo 'Territory'.
        /// </summary>
        /// <returns>The query result</returns>
        public int FortunateSharkAttacksSumWithinMonarchyOrTerritoryQuery()
        {
            return DataContext.Countries
                .Where(country => country.GovernmentForm == GovernmentForm.Monarchy || country.GovernmentForm == GovernmentForm.Territory)
                .Join(DataContext.SharkAttacks.Where(attack => attack.AttackSeverenity != AttackSeverenity.Fatal),
                country => country.Id,
                attack => attack.CountryId,
                (country, attack) => new { country, attack })
                .Count();
        }

        /// <summary>
        /// Úloha č. 3
        ///
        /// Marketingovému oddeleniu dochádzajú nápady ako pomenovávať nové produkty.
        /// 
        /// Inšpirovať sa chcú prezývkami žralokov, ktorí majú na svedomí najviac
        /// útokov v krajinách na kontinente 'South America'. Pre pochopenie potrebujú 
        /// tieto informácie vo formáte slovníku:
        /// 
        /// (názov krajiny) -> (prezývka žraloka s najviac útokmi v danej krajine)
        /// </summary>
        /// <returns>The query result</returns>
        public Dictionary<string, string> MostProlificNicknamesInCountriesQuery()
        {
            return DataContext.Countries
                .Where(country => country.Continent != null && country.Continent.Equals("South America"))
                .Join(DataContext.SharkAttacks,
                country => country.Id,
                attack => attack.CountryId,
                (country, attack) => new { country, attack })
                .Join(DataContext.SharkSpecies.Where(specie => specie.AlsoKnownAs != null && !specie.AlsoKnownAs.Equals("")),
                countryAttack => countryAttack.attack.SharkSpeciesId,
                species => species.Id,
                (countryAttack, species) => new { countryAttack, species })
                .GroupBy(countryAttackSpecies => countryAttackSpecies.countryAttack.country.Name)
                .Select(countryAttackSpecies => new {
                    countryAttackSpecies.Key, Value = countryAttackSpecies
                        .GroupBy(
                        countryAttackSpecies => countryAttackSpecies.species.AlsoKnownAs,
                        countryAttackSpecies => countryAttackSpecies.species.Id,
                        (key, g) => new { Key = key, Count = g.Count() })
                        .OrderByDescending(specie => specie.Count).First().Key 
                    })
                .ToDictionary(attack => attack.Key!, attack => attack.Value!);
        }

        /// <summary>
        /// Úloha č. 4
        ///
        /// Firma chce začať kompenzačnú kampaň a potrebuje k tomu dáta.
        ///
        /// Preto zistite, ktoré žraloky útočia najviac na mužov. 
        /// Vráťte ID prvých troch žralokov, zoradených zostupne podľa počtu útokov na mužoch.
        /// </summary>
        /// <returns>The query result</returns>
        public List<int> ThreeSharksOrderedByNumberOfAttacksOnMenQuery()
        {
            return DataContext.SharkAttacks
                .Join(DataContext.AttackedPeople.Where(person => person.Sex == Sex.Male),
                attack => attack.AttackedPersonId,
                person => person.Id,
                (attack, person) => new { attack, person })
                .GroupBy(
                attackPerson => attackPerson.attack.SharkSpeciesId,
                attackPerson => attackPerson.attack,
                (key, g) => new { Key = key, Count = g.Count() })
                .OrderByDescending(attack => attack.Count)
                .Select(attack => attack.Key)
                .Take(3)
                .ToList();
        }

        /// <summary>
        /// Úloha č. 5
        ///
        /// Oslovila nás medzinárodná plavecká organizácia. Chce svojich plavcov motivovať možnosťou
        /// úteku pred útokom žraloka.
        ///
        /// Potrebuje preto informácie o priemernej rýchlosti žralokov, ktorí
        /// útočili na plávajúcich ľudí (informácie o aktivite počas útoku obsahuje "Swimming" alebo "swimming").
        /// 
        /// Pozor, dáta požadajú oddeliť podľa jednotlivých kontinentov. Ignorujte útoky takých druhov žralokov,
        /// u ktorých nie je známa maximálná rýchlosť. Priemerné rýchlosti budú zaokrúhlené na dve desatinné miesta.
        /// </summary>
        /// <returns>The query result</returns>
        public Dictionary<string, double> SwimmerAttacksSharkAverageSpeedQuery()
        {
            return DataContext.SharkAttacks.Where(attack => attack.Activity != null && (attack.Activity.Contains("Swimming") || attack.Activity.Contains("swimming")))
                .Join(DataContext.SharkSpecies.Where(specie => specie.TopSpeed != null),
                attack => attack.SharkSpeciesId,
                specie => specie.Id,
                (attack, specie) => new { attack, specie })
                .Join(DataContext.Countries,
                attackSpecie => attackSpecie.attack.CountryId,
                country => country.Id,
                (attackSpecie, country) => new { attackSpecie, country })
                .GroupBy(
                attackSpecieCountry => attackSpecieCountry.country.Continent,
                attackSpecieCountry => attackSpecieCountry.attackSpecie.specie.TopSpeed,
                (key, g) => new { Key = key, Average = g.Average() })
                .ToDictionary(attack => attack.Key!, attack => Math.Round(attack.Average!.Value, 2));
        }

        /// <summary>
        /// Úloha č. 6
        ///
        /// Zistite všetky nefatálne (AttackSeverenity.NonFatal) útoky spôsobené pri člnkovaní 
        /// (AttackType.Boating), ktoré mal na vine žralok s prezývkou "Zambesi shark".
        /// Do výsledku počítajte iba útoky z obdobia po 3. 3. 1960 (vrátane) a ľudí,
        /// ktorých meno začína na písmeno z intervalu <D, K> (tiež vrátane).
        /// 
        /// Výsledný zoznam mien zoraďte abecedne.
        /// </summary>
        /// <returns>The query result</returns>
        public List<string> NonFatalAttemptOfZambeziSharkOnPeopleBetweenDAndKQuery()
        {
            return DataContext.SharkAttacks.Where(attack => attack.Type == AttackType.Boating && attack.AttackSeverenity == AttackSeverenity.NonFatal && attack.DateTime != null && new DateTime(1960, 3, 3).CompareTo(attack.DateTime!) <= 0)
                .Join(DataContext.SharkSpecies.Where(specie => specie.AlsoKnownAs != null && specie.AlsoKnownAs.Equals("Zambesi shark")),
                attack => attack.SharkSpeciesId,
                specie => specie.Id,
                (attack, specie) => new { attack, specie })
                .Join(DataContext.AttackedPeople.Where(person => person.Name?[0] >= 'D' && person.Name?[0] <= 'K'),
                attackSpecie => attackSpecie.attack.AttackedPersonId,
                person => person.Id,
                (attackSpecie, person) => person.Name!)
                .OrderBy(name => name)
                .ToList();
        }

        /// <summary>
        /// Úloha č. 7
        ///
        /// Zistilo sa, že desať najľahších žralokov sa správalo veľmi podozrivo počas útokov v štátoch Južnej Ameriky.
        /// 
        /// Vráťte preto zoznam dvojíc, kde pre každý štát z Južnej Ameriky bude uvedený zoznam žralokov,
        /// ktorí v tom štáte útočili. V tomto zozname môžu figurovať len vyššie spomínaných desať najľahších žralokov.
        /// 
        /// Pokiaľ v nejakom štáte neútočil žiaden z najľahších žralokov, zoznam žralokov u takého štátu bude prázdny.
        /// </summary>
        /// <returns>The query result</returns>
        public List<Tuple<string, List<SharkSpecies>>> LightestSharksInSouthAmericaQuery()
        {
            /*
            DataContext.SharkAttacks
                .Join(DataContext.SharkSpecies.OrderBy(specie => specie.Weight).Take(10),
                attack => attack.SharkSpeciesId,
                specie => specie.Id,
                (attack, specie) => new { attack, specie })
                .Join(DataContext.Countries.Where(country => country.Continent != null && country.Continent.Equals("South America") && country.Name != null),
                attack => attack.attack.CountryId,
                country => country.Id,
                (attack, country) => new { attack, country })
                .GroupBy(
                attack => attack.country.Name,
                attack => attack.attack.specie,
                (key, g) => new Tuple<string, List<SharkSpecies>>(key!, g.Count() == 0 ? g.ToList() : g.GroupBy(s => s.Id).Select(g => g.First()).ToList()))
                .ToList();

            DataContext.SharkAttacks
            .Join(DataContext.SharkSpecies.OrderBy(specie => specie.Weight).Take(10),
            attack => attack.SharkSpeciesId,
            specie => specie.Id,
            (attack, specie) => new { attack, specie })
            .GroupJoin(DataContext.Countries.Where(country => country.Continent != null && country.Continent.Equals("South America") && country.Name != null),
            attack => attack.attack.CountryId,
            country => country.Id,
            (attack, country) => new { attack, country })
            .SelectMany(
            x => x.attack.DefaultIfEmpty(),
            (attack, country) => new { attack, country })
            .GroupBy(
            attack => attack.country.Name,
            attack => attack == null ? null : attack.attack.attack.specie,
            (key, g) => new Tuple<string, List<SharkSpecies>>(key!, g.ElementAt(0) == null ? new List<SharkSpecies>() : g.GroupBy(s => s.Id).Select(g => g.First()).ToList()))
            .ToList();

            DataContext.SharkAttacks
            .Join(DataContext.SharkSpecies.OrderBy(specie => specie.Weight).Take(10),
            attack => attack.SharkSpeciesId,
            specie => specie.Id,
            (attack, specie) => new { attack, specie })
            */
            return DataContext.Countries.Where(country => country.Continent != null && country.Continent.Equals("South America") && country.Name != null)
                .GroupJoin(DataContext.SharkAttacks.Join(DataContext.SharkSpecies.OrderBy(specie => specie.Weight).Take(10),
                    attack => attack.SharkSpeciesId,
                    specie => specie.Id,
                    (attack, specie) => new { attack, specie }),
                country => country.Id,
                attackSpecie => attackSpecie.attack.CountryId,
                (country, attackSpecie) => new { country, attackSpecie })
                .SelectMany(
                x => x.attackSpecie.DefaultIfEmpty(),
                (country, attackSpecie) => new { country.country, Specie = (attackSpecie == null ? null : attackSpecie.specie) })
                .GroupBy(
                countrySpecie => countrySpecie.country.Name,
                countrySpecie => countrySpecie.Specie,
                (key, g) => new Tuple<string, List<SharkSpecies>>(key!, g.ElementAt(0) == null ? new List<SharkSpecies>() : g.Where(specie => specie != null).GroupBy(specie => specie!.Id).Select(g => g.First()).ToList()!))
                .ToList();
        }

        /// <summary>
        /// Úloha č. 8
        ///
        /// Napísať hocijaký LINQ dotaz musí byť pre Vás už triviálne. Riaditeľ firmy vás preto chce
        /// využiť na testovanie svojich šialených hypotéz.
        /// 
        /// Zistite, či každý žralok, ktorý má maximálnu rýchlosť aspoň 56 km/h zaútočil aspoň raz na
        /// človeka, ktorý mal viac ako 56 rokov. Výsledok reprezentujte ako pravdivostnú hodnotu.
        /// </summary>
        /// <returns>The query result</returns>
        public bool FiftySixMaxSpeedAndAgeQuery()
        {
            return !DataContext.SharkSpecies.Where(specie => specie.TopSpeed >= 56)
                .Join(DataContext.SharkAttacks,
                specie => specie.Id,
                attack => attack.SharkSpeciesId,
                (specie, attack) => attack
                ).GroupBy(
                attack => attack.SharkSpeciesId,
                attack => attack.AttackedPersonId,
                (specieId, personId) => new { specieId, personId })
                .Select(specieIdPersonId => specieIdPersonId.personId
                    .Join(DataContext.AttackedPeople.Where(person => person.Age > 56),
                    personId => personId,
                    person => person.Id,
                    (personId, person) => personId))
                .ToList().Any(personId => personId.Count() == 0);
        }

        /// <summary>
        /// Úloha č. 9
        ///
        /// Ohromili ste svojim výkonom natoľko, že si od Vás objednali rovno textové výpisy.
        /// Samozrejme, že sa to dá zvladnúť pomocou LINQ.
        /// 
        /// Chcú, aby ste pre všetky fatálne útoky v štátoch začínajúcich na písmeno 'B' alebo 'R' urobili výpis v podobe: 
        /// "{Meno obete} was attacked in {názov štátu} by {latinský názov žraloka}"
        /// 
        /// Záznam, u ktorého obeť nemá meno
        /// (údaj neexistuje, nejde o vlastné meno začínajúce na veľké písmeno, či údaj začína číslovkou)
        /// do výsledku nezaraďujte. Získané pole zoraďte abecedne a vraťte prvých 5 viet.
        /// </summary>
        /// <returns>The query result</returns>
        public List<string> InfoAboutPeopleAndCountriesOnBorRAndFatalAttacksQuery()
        {
            return DataContext.Countries.Where(country => country.Name?[0] == 'B' || country.Name?[0] == 'R')
                .Join(DataContext.SharkAttacks.Where(attack => attack.AttackSeverenity == AttackSeverenity.Fatal),
                country => country.Id,
                attack => attack.CountryId,
                (country, attack) => new { country, attack }
                )
                .Join(DataContext.SharkSpecies,
                countryAttack => countryAttack.attack.SharkSpeciesId,
                specie => specie.Id,
                (countryAttack, specie) => new { countryAttack, specie }
                )
                .Join(DataContext.AttackedPeople.Where(person => person.Name != null && person.Name[0] >= 'A' && person.Name[0] <= 'Z'),
                countryAttackSpecie => countryAttackSpecie.countryAttack.attack.AttackedPersonId,
                person => person.Id,
                (countryAttackSpecie, person) => new { countryAttackSpecie, person }
                )
                .Select(countryAttackSpeciePerson => $"{countryAttackSpeciePerson.person.Name} was attacked in {countryAttackSpeciePerson.countryAttackSpecie.countryAttack.country.Name} by {countryAttackSpeciePerson.countryAttackSpecie.specie.LatinName}")
                .OrderBy(str => str)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Úloha č. 10
        ///
        /// Nedávno vyšiel zákon, že každá krajina Európy začínajúca na písmeno A až L (vrátane)
        /// musí zaplatiť pokutu 250 jednotiek svojej meny za každý žraločí útok na ich území.
        /// Pokiaľ bol tento útok smrteľný, musia dokonca zaplatiť 300 jednotiek. Ak sa nezachovali
        /// údaje o tom, či bol daný útok smrteľný alebo nie, nemusia platiť nič.
        /// Áno, tento zákon je spravodlivý...
        /// 
        /// Vráťte informácie o výške pokuty európskych krajín začínajúcich na A až L (vrátane).
        /// Tieto informácie zoraďte zostupne podľa počtu peňazí, ktoré musia tieto krajiny zaplatiť.
        /// Vo finále vráťte 5 záznamov s najvyššou hodnotou pokuty.
        /// 
        /// V nasledujúcej sekcii môžete vidieť príklad výstupu v prípade, keby na Slovensku boli 2 smrteľné útoky,
        /// v Česku jeden fatálny + jeden nefatálny a v Maďarsku žiadny:
        /// <code>
        /// Slovakia: 600 EUR
        /// Czech Republic: 550 CZK
        /// Hungary: 0 HUF
        /// </code>
        /// 
        /// </summary>
        /// <returns>The query result</returns>
        public List<string> InfoAboutFinesInEuropeQuery()
        {
            return DataContext.Countries.Where(country => country.Continent != null && country.Continent.Equals("Europe") && country.Name?[0] >= 'A' && country.Name?[0] <= 'L')
                .GroupJoin(DataContext.SharkAttacks.Where(attack => attack.AttackSeverenity == AttackSeverenity.Fatal || attack.AttackSeverenity == AttackSeverenity.NonFatal),
                country => country.Id,
                attack => attack.CountryId,
                (country, attack) => new { country, attack })
                .SelectMany(
                countryAttack => countryAttack.attack.DefaultIfEmpty(),
                (country, attack) => new { country.country, attack })
                .GroupBy(
                countryAttack => new { countryAttack.country.Name, countryAttack.country.CurrencyCode },
                countryAttack => countryAttack.attack == null ? null : countryAttack.attack.AttackSeverenity,
                (key, g) => new { key, Sum = g.Aggregate(0, (sum, next) => sum + (next == null ? 0 : (next == AttackSeverenity.Fatal ? 300 : 250)), sum => sum) })
                .OrderByDescending(countryAttackSeverenity => countryAttackSeverenity.Sum)
                .Take(5)
                .Select(countryAttackSeverenity => $"{countryAttackSeverenity.key.Name}: {countryAttackSeverenity.Sum} {countryAttackSeverenity.key.CurrencyCode}")
                .ToList();
        }

        /// <summary>
        /// Úloha č. 11
        ///
        /// Organizácia spojených žraločích národov výhlásila súťaž: 5 druhov žralokov, 
        /// ktoré sú najviac agresívne získa hodnotné ceny.
        /// 
        /// Nájdite 5 žraločích druhov, ktoré majú na svedomí najviac ľudských životov,
        /// druhy zoraďte podľa počtu obetí.
        ///
        /// Výsledok vráťte vo forme slovníku, kde
        /// kľúčom je meno žraločieho druhu a
        /// hodnotou je súhrnný počet obetí spôsobený daným druhom žraloka.
        /// </summary>
        /// <returns>The query result</returns>
        public Dictionary<string, int> FiveSharkNamesWithMostFatalitiesQuery()
        {
            return DataContext.SharkAttacks.Where(attack => attack.AttackSeverenity == AttackSeverenity.Fatal)
                .Join(DataContext.SharkSpecies.Where(specie => specie.Name != null),
                attack => attack.SharkSpeciesId,
                specie => specie.Id,
                (attack, specie) => new { attack, specie })
                .GroupBy(
                attackSpecie => attackSpecie.specie.Name!,
                attackSpecie => attackSpecie.attack.Id,
                (key, g) => new { key, Sum = g.Count() })
                .OrderByDescending(specieNameAttackId => specieNameAttackId.Sum)
                .Take(5)
                .ToDictionary(specieNameAttackId => specieNameAttackId.key, specieNameAttackId => specieNameAttackId.Sum);
        }

        /// <summary>
        /// Úloha č. 12
        ///
        /// Riaditeľ firmy chce si chce podmaňiť čo najviac krajín na svete. Chce preto zistiť,
        /// na aký druh vlády sa má zamerať, aby prebral čo najviac krajín.
        /// 
        /// Preto od Vás chce, aby ste mu pomohli zistiť, aké percentuálne zastúpenie majú jednotlivé typy vlád.
        /// Požaduje to ako jeden string:
        /// "{1. typ vlády}: {jej percentuálne zastúpenie}%, {2. typ vlády}: {jej percentuálne zastúpenie}%, ...".
        /// 
        /// Výstup je potrebný mať zoradený od najväčších percent po najmenšie,
        /// pričom percentá riaditeľ vyžaduje zaokrúhľovať na jedno desatinné miesto.
        /// Pre zlúčenie musíte podľa jeho slov použiť metódu `Aggregate`.
        /// </summary>
        /// <returns>The query result</returns>
        public string StatisticsAboutGovernmentsQuery()
        {
            return DataContext.Countries
                .GroupBy(
                country => country.GovernmentForm,
                country => country.Id,
                (key, g) => new { key, Count = g.Count() })
                .OrderByDescending(countryGovernmentAndId => countryGovernmentAndId.Count)
                .Aggregate(
                new StringBuilder(),
                (output, next) => output.Append($"{next.key}: {Math.Round((double) next.Count * 100 / DataContext.Countries.Count(), 1).ToString("0.0")}%, "),
                output => output.Remove(output.Length -2, 2).ToString());
        }

        /// <summary>
        /// Úloha č. 13
        ///
        /// Firma zistila, že výrobky s tigrovaným vzorom sú veľmi populárne. Chce to preto aplikovať
        /// na svoju doménu.
        ///
        /// Nájdite informácie o ľudoch, ktorí boli obeťami útoku žraloka s menom "Tiger shark"
        /// a útok sa odohral v roku 2001.
        /// Výpis majte vo formáte:
        /// "{meno obete} was tiggered in {krajina, kde sa útok odohral}".
        /// V prípade, že chýba informácia o krajine útoku, uveďte namiesto názvu krajiny reťazec "Unknown country".
        /// V prípade, že informácie o obete vôbec neexistuje, útok ignorujte.
        ///
        /// Ale pozor! Váš nový nadriadený má panický strach z operácie `Join` alebo `GroupJoin`.
        /// Informácie musíte zistiť bez spojenia hocijakých dvoch tabuliek. Skúste sa napríklad zamyslieť,
        /// či by vám pomohla metóda `Zip`.
        /// </summary>
        /// <returns>The query result</returns>
        public List<string> TigerSharkAttackZipQuery()
        {   /*
            var tigerSharkSpecieId = DataContext.SharkSpecies.Where(specie => specie.Name != null && specie.Name.Equals("Tiger shark")).Select(specie => specie.Id).Single();
            var tigerSharkAttacksIn2001 = DataContext.SharkAttacks.Where(attack => attack.SharkSpeciesId == tigerSharkSpecieId && attack.AttackedPersonId != null && new DateTime(2001, 1, 1).CompareTo(attack.DateTime) <= 0 && new DateTime(2001, 12, 31).CompareTo(attack.DateTime) >= 0);
            var tigerSharkAttacksIn2001CountryNames = DataContext.Countries.Where(country => tigerSharkAttacksIn2001.Select(attack => attack.CountryId).Contains(country.Id)).Select(country => new { country.Id, country.Name });
            var tigerSharkAttacksIn2001PersonNames = DataContext.AttackedPeople.Where(person => person.Name != null && tigerSharkAttacksIn2001.Select(attack => attack.AttackedPersonId).Contains(person.Id)).Select(person => new { person.Id, person.Name});
            return tigerSharkAttacksIn2001.Select(
                 attack => new { attack, countryName = tigerSharkAttacksIn2001CountryNames.Where(country => country.Id == attack.CountryId).Select(country => country.Name).FirstOrDefault(), personName = tigerSharkAttacksIn2001PersonNames.Where(person => person.Id == attack.AttackedPersonId).Select(person => person.Name).FirstOrDefault() })
                .Where(attack => attack.personName != null)
                .Select(attack => $"{attack.personName} was tiggered in {attack.countryName ?? "Unknown country"}")
                .ToList();

            /*
            Console.WriteLine(tigerSharkAttacksIn2001.Where(attack => attack.CountryId == null).Count());
            var tigerSharkAttacksIn2001CountryIdsAndNames = DataContext.Countries.Where(country => tigerSharkAttacksIn2001.Select(attack => attack.CountryId).Contains(country.Id)).Select(country => new { country.Id, country.Name });
            var tigerSharkAttacksIn2001PersonIdsAndNames = DataContext.AttackedPeople.Where(person => person.Name != null && tigerSharkAttacksIn2001.Select(attack => attack.AttackedPersonId).Contains(person.Id)).Select(person => new { person.Id, person.Name }).OrderBy(person => person.Id);
            */

            var tigerSharkSpecieId = DataContext.SharkSpecies
                .Where(specie => specie.Name != null && specie.Name.Equals("Tiger shark"))
                .Select(specie => specie.Id)
                .Single();
            var tigerSharkAttacksIn2001 = DataContext.SharkAttacks
                .Where(attack => attack.SharkSpeciesId == tigerSharkSpecieId && attack.AttackedPersonId != null && attack.DateTime.HasValue && attack.DateTime.Value.Year == 2001)
                .OrderBy(attack => attack.AttackedPersonId);
            return DataContext.AttackedPeople
                .Where(person => person.Name != null && tigerSharkAttacksIn2001.Select(attack => attack.AttackedPersonId).Contains(person.Id))
                .OrderBy(person => person.Id)
                .Select(person => person.Name)
                .Zip(tigerSharkAttacksIn2001.Select(attack => DataContext.Countries.Where(country => attack.CountryId == country.Id).Select(country => country.Name).FirstOrDefault("Unknown country")),
                (personName, countryName) => $"{personName} was tiggered in {countryName}")
                .ToList();
        }

        /// <summary>
        /// Úloha č. 14
        ///
        /// Vedúci oddelenia prišiel s ďalšou zaujímavou hypotézou. Myslí si, že veľkosť žraloka nejako 
        /// súvisí s jeho apetítom na ľudí.
        ///
        /// Zistite pre neho údaj, koľko percent útokov má na svedomí najväčší a koľko najmenší žralok.
        /// Veľkosť v tomto prípade uvažujeme podľa dĺžky.
        /// 
        /// Výstup vytvorte vo formáte: "{percentuálne zastúpenie najväčšieho}% vs {percentuálne zastúpenie najmenšieho}%"
        /// Percentuálne zastúpenie zaokrúhlite na jedno desatinné miesto.
        /// </summary>
        /// <returns>The query result</returns>
        public string LongestVsShortestSharkQuery()
        {
            var sharksByLength = DataContext.SharkSpecies
                .OrderBy(specie => specie.Length).ToList();
            var shortestAndLongestShark = new { Shortest = sharksByLength.First().Id, Longest = sharksByLength.Last().Id };
            return DataContext.SharkAttacks.Where(attack => attack.SharkSpeciesId == shortestAndLongestShark.Shortest || attack.SharkSpeciesId == shortestAndLongestShark.Longest)
                .GroupBy(
                attack => attack.SharkSpeciesId,
                attack => attack.Id,
                (key, g) => new { Key = (key == shortestAndLongestShark.Shortest ? "shortest" : "longest"), Count = g.Count() })
                .OrderBy(x => x.Key)
                .Aggregate(new StringBuilder(), (output, next) => output.Append($"{Math.Round((double) next.Count * 100 / DataContext.SharkAttacks.Count(), 1).ToString("0.0")}% vs "), output => output.Remove(output.Length - 4, 4).ToString());
        }

        /// <summary>
        /// Úloha č. 15
        ///
        /// Na koniec vašej kariéry Vám chceme všetci poďakovať a pripomenúť Vám vašu mlčanlivosť.
        /// 
        /// Ako výstup požadujeme počet krajín, v ktorých žralok nespôsobil smrť (útok nebol fatálny).
        /// Berte do úvahy aj tie krajiny, kde žralok vôbec neútočil.
        /// </summary>
        /// <returns>The query result</returns>
        public int SafeCountriesQuery()
        {
            return DataContext.Countries.Count() - 
                DataContext.SharkAttacks.Where(attack => attack.AttackSeverenity == AttackSeverenity.Fatal && attack.CountryId != null)
                .GroupBy(attackCountry => attackCountry.CountryId).Count();
        }
    }
}
