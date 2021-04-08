using System;

namespace SafeEncrypt
{
    public struct MenuOption
    {
        private readonly string name;
        private readonly int code;
        private readonly Func<string, string> hashedAction;

        public MenuOption(string name, int code, Func<string, string> hashedAction) : this()
        {
            this.name = name;
            this.code = code;
            this.hashedAction = hashedAction;
        }

        public string Name => name;
        public int Code => code;
        public Func<string, string> HashedAction => hashedAction;
    }
}