using System.Collections.Generic;
using System.Linq;

namespace SentenceGenerator
{
    public class SentenceComposer
    {
        #region OriginalInterviewAlgorithm

        public IEnumerable<string> ComposeIterative(Dictionary<string, string> words, List<string> template)
        {
            var grouping = words.GroupBy(x => x.Value).ToDictionary(i => i.Key, i => i.Select(k => k.Key).ToList());
            var groupingCount = grouping.ToDictionary(i => i.Key, i => i.Value.Count);
            var totalPossibleSentences = groupingCount.Aggregate(1, (acc, val) => acc * val.Value);

            var result = new List<string>();
            var solution = new List<int>(new int[template.Count]);

            for (var i = 0; i < totalPossibleSentences; i++)
            {
                result.Add(GetSentence(grouping, template, solution));

                var j = template.Count - 1;
                while (j >= 0 && solution[j] == groupingCount[template[j]] - 1)
                {
                    solution[j] = 0;
                    j--;
                }

                if (j < 0) break;

                solution[j] = solution[j] + 1;
            }

            return result;
        }

        private static string GetSentence(IReadOnlyDictionary<string, List<string>> words, IList<string> template,
            IList<int> indexes)
            => string.Join(" ",
                   template.Select(templatePart =>
                           words[templatePart][indexes[template.IndexOf(templatePart)]])
                       .ToList()) + ".";

        #endregion

        #region MyAlgo

        public IEnumerable<string> Compose(Dictionary<string, string> words, List<string> template)
        {
            return
                words
                    .GroupBy(x => x.Value)
                    .Aggregate(new [] { new KeyValuePair<string, string>[]{}},
                        (acc, seq) => acc.SelectMany(j => seq, (l, r) => l.Concat(new[] {r}).ToArray()).ToArray())
                        .Select(i => string.Join(" ", template.Select(j => i.Single(k => k.Value == j).Key)) + ".")
                        .ToList();
        }

        #endregion
    }
}
