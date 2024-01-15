using PlayersMVCApplication.BusinessLayer.Interfaces;

namespace PlayersMVCApplication.BusinessLayer.BusinessLogics
{
    public class BusinessLogics : IBusinessLogics
    {
        public Task<int> GetRandomNumber()
        {
            Random random = new();
            int getNumber = random.Next(000000000, 999999999);
            return Task.FromResult(getNumber);
        }
    }
}
