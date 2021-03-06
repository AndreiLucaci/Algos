﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SentenceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new Dictionary<string, string>
            {
                ["Ana"] = "personal-noun",
                ["drink"] = "verb",
                ["break"] = "verb",
                ["John"] = "personal-noun",
                ["bottle"] = "noun",
                ["Emma"] = "personal-noun"
            };

            var template = new List<string> {"personal-noun", "verb", "noun"};

            var sentenceComposer = new SentenceComposer();

            var result1 = sentenceComposer.Compose(words, template).OrderBy(i => i).ToList();
            var result2 = sentenceComposer.ComposeIterative(words, template).OrderBy(i => i).ToList();

            Console.WriteLine("First result");
            foreach (var sentence in result1)
            {
                Console.WriteLine(sentence);
            }

            Console.WriteLine("Second result");
            foreach (var sentence in result2)
            {
                Console.WriteLine(sentence);
            }

            Console.WriteLine("Are equal? {0}", result1.SequenceEqual(result2));
        }
    }
}
