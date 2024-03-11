using Newtonsoft.Json;

namespace CoreSession_0.ExtensionMethods
{
    public static class SessionExtension
    {
        //Bir metodun Extension method olabilmesi icin ilk parametresini cok özel alması gerekir...Bu ilk parametre verilirken this keyword'u ile baslanmalıdır...Ve entegre edilmek istedigi tip secilmelidir ki o tip icerisinde o metot yer alsın...Sonra diger parametreler normal bir şekilde verilir...


        public static void SetObject(this ISession session, string key, object value)
        {
            string serializedObject = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedObject);
        }

        public static T GetObject<T>(this ISession session,string key) where T : class
        {
            string serializedObject = session.GetString(key);   
            if(string.IsNullOrEmpty(serializedObject))
            {
                return null;
            }
            T deserializedObject = JsonConvert.DeserializeObject<T>(serializedObject);
            return deserializedObject;
        }
    }
}
