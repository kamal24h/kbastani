
namespace Service
{
    public class ProfileService
    {
        public ProfileService() { }

        public ProfileService(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
        }

    }
}
