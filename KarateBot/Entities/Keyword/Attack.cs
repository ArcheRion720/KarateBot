using KarateBot.Services;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarateBot.Entities.Keyword
{
    public class Attack : Keyword
    {
        public AttackEffects Effects { get; set; }
        public int Damage { get; set; }
        
        public Attack(string adj, Gender gender, string noun) : base(adj, gender, noun) { }

        public override string ToString()
        {
            var temp = Effects;
            var effects = new List<string>();

            RemoveFlags(AttackEffects.Synergy | AttackEffects.Antagonism, ref temp);

            AddEffect(AttackEffects.Professional,   "Profesjonalista",  effects,    ref temp);
            AddEffect(AttackEffects.Critical,       "Krytyczny",        effects,    ref temp);
            AddEffect(AttackEffects.Synergy,        "Synergia",         effects,    ref temp);
            AddEffect(AttackEffects.Antagonism,     "Antagonizm",       effects,    ref temp);
            AddEffect(AttackEffects.Combination,    "Kombinacja",       effects,    ref temp);

            return $"{Utils.CompleteNoun(Adjective, Gender)} {Noun} - {Damage} obrażeń (**{String.Join(", ", effects)}**)";
        }

        private static void RemoveFlags(AttackEffects flags, ref AttackEffects data)
        {
            if(data.HasFlag(flags))
                data &= ~flags;
        }

        private static void AddEffect(AttackEffects effect, string bonus, List<string> list, ref AttackEffects data)
        {
            if (data.HasFlag(effect))
            {
                data &= ~effect;
                list.Add(bonus);
            }
        }
    }

    [Flags]
    public enum AttackEffects
    {
        Critical        = 1 << 0,
        Synergy         = 1 << 1,
        Antagonism      = 1 << 2,
        Specialist      = 1 << 3,
        Combination      = 1 << 4,


        Professional    = Synergy | Specialist
    }
}
