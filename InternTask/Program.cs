using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InternTask
{
    public class Program
    {
        public static void Main()
        {
            var pathToInputFile = Console.ReadLine();
            var maxYearsPlayed = int.Parse(Console.ReadLine());
            var minPlayerRating = int.Parse(Console.ReadLine());
            var pathToOutputFile = Console.ReadLine();

            var inputString = File.ReadAllText(pathToInputFile);

            var allPlayersParsed = JsonConvert.DeserializeObject<List<Player>>(inputString);

            var lastQualifyingYear = DateTime.UtcNow.Year - maxYearsPlayed;

            var qualifiedPlayers = allPlayersParsed.Where(p => p.PlayingSince >= lastQualifyingYear && p.Rating >= minPlayerRating).Select(x => new
            {
                x.Name,
                x.Rating
            }).OrderByDescending(x => x.Rating).ToList();

            var output = new StringBuilder();
            output.AppendLine("Name,Rating");

            qualifiedPlayers.ForEach(p => output.AppendLine($"{p.Name}, {p.Rating}"));

            File.WriteAllText(pathToOutputFile, output.ToString().TrimEnd());
        }
    }
}