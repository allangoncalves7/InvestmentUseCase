using Newtonsoft.Json;

namespace InvestmentUseCase.Domain.Utils
{
    public static class JsonUtils
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
