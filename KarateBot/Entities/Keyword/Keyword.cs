using System;
using System.Collections.Generic;
using System.Text;

namespace KarateBot.Entities.Keyword
{
    public class Keyword : IDescription
    {
        public Gender Gender { get; set; }
        public string Adjective { get; set; }
        public string Noun { get; set; }

        public string Bonus { get; set; }

        public Keyword(string adjective, Gender gender, string noun)
        {
            Adjective = adjective;
            Gender = gender;
            Noun = noun;
        }

        public bool MatchAdjective(Keyword other)
            => Adjective == other.Adjective;

        public bool MatchNoun(Keyword other)
            => Noun == other.Noun;

        public override string ToString()
            => $"{Utils.CompleteNoun(Adjective, Gender)} {Noun} {Bonus}";
    }

    public class DualKeyword : IDescription
    {
        public Gender Gender { get; set; }
        public string Adjective { get; set; }
        public string Noun { get; set; }

        public string Bonus { get; set; }

        public DualKeyword(string adjective, Gender gender, string noun)
        {
            Adjective = adjective;
            Gender = gender;
            Noun = noun;
        }

        public bool MatchAdjective(Keyword other)
            => Adjective == other.Adjective;

        public bool MatchNoun(Keyword other)
            => Noun == other.Noun;

        public override string ToString()
            => $"{Utils.CompleteNoun(Adjective, Gender)} {Noun} {Bonus}";

    }
}
