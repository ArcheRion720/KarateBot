using System;
using System.Collections.Generic;
using System.Text;

namespace KarateBot.Entities.Keyword
{
    public interface IDescription
    {
        string ToString();
    }

    public class Description : IDescription
    {
        private readonly string Text;

        public Description(string text)
            => Text = text;

        public override string ToString()
            => Text;
    }
}
