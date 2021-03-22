

namespace Battleship.Util
{
    public interface IAuthUtil
    {
        public string GenerateJSONWebToken(int id);
    }
}