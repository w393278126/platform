namespace Xn.Platform.Domain.User
{
    public class UserMedalInfo
    {
        public int roomId { get; set; }
        public string domain { get; set; }
        public int fan { get; set; }
        public int level { get; set; }
        public string name { get; set; }
    }

    public class UserAndUserMedal
    {
        public UserInfo user { get; set; }
        public UserMedalInfo medal { get; set; }
    }
}