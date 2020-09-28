using KarateBot.Entities;
using KarateBot.Entities.Keyword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarateBot
{
    public static class Utils
    {
        private static Random Random { get; set; } = new Random();

        public static T GetRandom<T>(this IList<T> list)
            => list[Random.Next(0, list.Count)];

        public static bool GetRandomBool()
            => Random.NextDouble() < .5;

        public static void AddOrUpdate<T>(this List<T> list, Predicate<T> predicate, T value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    //Update
                    list[i] = value;
                    return;
                }
            }

            //Add
            list.Add(value);
        }

        public static void RemoveFirst<T>(this List<T> list, Predicate<T> predicate, out bool success)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    success = true;
                    return;
                }
            }

            success = false;
        }

        public static int IndistingFromEnd<T>(this List<T> list, Func<T, bool> predicate)
        {
            int result = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!predicate(list[i]))
                    break;

                result++;
            }

            return result;
        }

        public static string InsertIfNotEmpty(string text, string insert)
            => InsertIfNotEmpty(text, insert, insert);

        public static string InsertIfNotEmpty(string text, string before, string after)
        {
            if (text == string.Empty)
                return string.Empty;

            return $"{before}{text}{after}";
        }

        public static int GetDamage(int min, int max, out bool isCrit)
        {
            isCrit = Random.NextDouble() < .1;

            return Random.Next(min, max + 1) * (isCrit ? 2 : 1);
        }

        public static int Round(double value)
        {
            if (value >= .5)
                return (int)(value + 1);

            return (int)value;
        }

        public static void ScrollText(StringBuilder builder)
        {
            string s = builder.ToString();

            if (s.Split('\n').Length < 25)
                return;

            builder.Remove(0, s.IndexOf('\n') + 1);
        }

        public static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        public static string CompleteVerb(string str, Gender gender)
        {
            return str + gender switch
            {
                Gender.MASCULINE => String.Empty,
                Gender.FEMININE => 'a',
                Gender.NEUTER => 'o',
                _ => ""
            };
        }

        public static string CompleteNoun(string str, Gender gender)
        {
            return str + (gender, str[^1]) switch
            {
                (Gender.MASCULINE, 'k') => 'i',
                (Gender.MASCULINE, 'g') => 'i',
                (Gender.MASCULINE, _) => 'y',
                (Gender.FEMININE, _) => 'a',
                (Gender.NEUTER, 'k') => "ie",
                (Gender.NEUTER, 'g') => "ie",
                (Gender.NEUTER, _) => "e",
                _ => String.Empty
            };
        }

    }
}
