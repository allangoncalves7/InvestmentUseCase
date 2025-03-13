using System.ComponentModel;
using System.Reflection;

namespace InvestmentUseCase.Domain.Utils
{
    public static class Utils
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = (DescriptionAttribute)field
                .GetCustomAttribute(typeof(DescriptionAttribute));

            return attribute?.Description ?? value.ToString();
        }

    }
}
