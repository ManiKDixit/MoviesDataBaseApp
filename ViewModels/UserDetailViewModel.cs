namespace MoviesDataBaseApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? WatchList { get; set; }
        public string? Favorites { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
