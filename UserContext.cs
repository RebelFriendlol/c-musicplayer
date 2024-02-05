using System;

namespace AlbertoPlayer
{
    public class UserContext
    {
        private static UserContext _instance;

        // Właściwość przechowująca nazwę użytkownika
        public string Username { get; private set; }

        // Prywatny konstruktor, aby uniemożliwić bezpośrednie utworzenie instancji
        private UserContext() { }

        // Metoda do ustawiania nazwy użytkownika
        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Nazwa użytkownika nie może być pusta ani null.");
            }

            Username = username;
        }

        // Metoda do uzyskania instancji UserContext
        public static UserContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserContext();
                }
                return _instance;
            }
        }
    }
}