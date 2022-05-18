using Db;

namespace protobuf
{
    public class LoginRequest
    {
        public string UserName;
        public string Uid;
    }
    public class LoginResponse
    {
        public User UserInfo { get; set; }
    }
}