using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SentenceGenerator
{
    public class SentenceComposer
    {
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
        
        public IEnumerable<string> Compose(Dictionary<string, string> words, List<string> template)
        {
            var grouping = words.GroupBy(x => x.Value).ToDictionary(i => i.Key, i => i.Select(k => k.Key).ToArray());
            var groupingCount = grouping.ToDictionary(i => i.Key, i => i.Value.Length);
            var totalPossibleSentences = groupingCount.Aggregate(1, (acc, val) => acc * val.Value);

            var result = new List<string>();

            var a = new int[totalPossibleSentences][];
            for (var i = 0; i < a.Length; i++)
            {
                a[i] = new int[template.Count];
            }

            for (var i = 0; i < totalPossibleSentences; i++)
            {
                for (var j = 0; j < template.Count; j++)
                {
                    a[i][j] = j;
                }
            }


            //for (int i = 0, j = 0, k = 0; i < totalPossibleSentences; i++, k++)
            //{
            //    var lst = new List<string>();
            //    var count = groupingCount[template[j]];
            //    if (k == count)
            //    {
            //        k = 0;
            //        j++;
            //    }

            //    foreach (var _ in template)
            //    {
            //        lst.Add(grouping[template[j]][k]);
            //    }

            //    result.Add(string.Join(" ", lst) + ".");
            //}

            return result;
        }
    }
}
