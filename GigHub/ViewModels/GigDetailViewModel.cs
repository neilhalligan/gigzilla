using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigDetailViewModel
    {
        public Gig Gig { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool UserIsAttending { get; set; }
    }
}