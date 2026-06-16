namespace FrontTPAPIdeProyectoSoftware.Components.Servicies
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public bool IsLogged => UserId > 0;
    }
}
