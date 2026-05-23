namespace Front.Client.Services;

    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public bool IsLogged => UserId > 0;
    }

