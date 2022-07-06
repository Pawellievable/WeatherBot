using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;

namespace WeatherBot
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static ITelegramBotClient botClient = new TelegramBotClient("5509496337:AAFIWZBWhzt_Bcn9yUWehnbLLLdMPXw8B5c");
        private static async Task<string> PostRequestAsync(string url, string json)
        {
            using HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        private static async Task<string> GetRequestAsync(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static DateTime UnixTimeStampToDateTimeLocal(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() != " ")
                {
                    if (message.Text.Length >= 1)
                    {
                        try
                        {
                            
                            string jsonResponse = await GetRequestAsync("https://api.openweathermap.org/data/2.5/weather?q=" + message.Text.ToLower() + "&lang=ru&appid=662c623ec7311181ebb2ab42dadb564f&units=metric");
                            WeatherResponse response_global = JsonConvert.DeserializeObject<WeatherResponse>(jsonResponse);
                            string UTC = null;
                            if (response_global.timezone < 0)
                                UTC = " UTC ";
                            else 
                                UTC = " UTC +";
                            string WIND_DEG = null;
                            if (response_global.wind.deg < 348.75 && response_global.wind.deg >= 11.25)
                                WIND_DEG = "С";
                            else if (response_global.wind.deg < 11.25 && response_global.wind.deg >= 33.75)
                                WIND_DEG = "ССВ";
                            else if (response_global.wind.deg < 33.75 && response_global.wind.deg >= 56.25)
                                WIND_DEG = "СВ";
                            else if (response_global.wind.deg < 56.25 && response_global.wind.deg >= 78.75)
                                WIND_DEG = "ВСВ";
                            else if (response_global.wind.deg < 78.75 && response_global.wind.deg >= 101.25)
                                WIND_DEG = "В";
                            else if (response_global.wind.deg < 101.25 && response_global.wind.deg >= 123.75)
                                WIND_DEG = "ВЮВ";
                            else if (response_global.wind.deg < 123.75 && response_global.wind.deg >= 146.25)
                                WIND_DEG = "ЮВ";
                            else if (response_global.wind.deg < 146.25 && response_global.wind.deg >= 168.75)
                                WIND_DEG = "ЮЮВ";
                            else if (response_global.wind.deg < 168.75 && response_global.wind.deg >= 191.25)
                                WIND_DEG = "Ю";
                            else if (response_global.wind.deg < 191.25 && response_global.wind.deg >= 213.75)
                                WIND_DEG = "ЮЮЗ";
                            else if (response_global.wind.deg < 213.75 && response_global.wind.deg >= 236.25)
                                WIND_DEG = "ЮЗ";
                            else if (response_global.wind.deg < 236.25 && response_global.wind.deg >= 258.75)
                                WIND_DEG = "ЗЮЗ";
                            else if (response_global.wind.deg < 258.75 && response_global.wind.deg >= 281.25)
                                WIND_DEG = "З";
                            else if (response_global.wind.deg < 281.25 && response_global.wind.deg >= 303.75)
                                WIND_DEG = "ЗСЗ";
                            else if (response_global.wind.deg < 303.75 && response_global.wind.deg >= 326.25)
                                WIND_DEG = "СЗ";
                            else if (response_global.wind.deg < 326.25 && response_global.wind.deg >= 348.75)
                                WIND_DEG = "ССЗ";
                            string REGION = null;
                            switch (response_global.sys.country)
                            {
                                case "AU":
                                    REGION = "Австралия";
                                    break;
                                case "AT":
                                    REGION = "Австрия";
                                    break;
                                case "AZ":
                                    REGION = "Азербайджан";
                                    break;
                                case "AL":
                                    REGION = "Албания";
                                    break;
                                case "DZ":
                                    REGION = "Алжир";
                                    break;
                                case "AS":
                                    REGION = "Американское Самоа";
                                    break;
                                case "AI":
                                    REGION = "Ангилья";
                                    break;
                                case "AO":
                                    REGION = "Ангола";
                                    break;
                                case "AD":
                                    REGION = "Андорра";
                                    break;
                                case "AQ":
                                    REGION = "Антарктида";
                                    break;
                                case "AG":
                                    REGION = "Антигуа и Барбуда";
                                    break;
                                case "AN":
                                    REGION = "Антильские Острова";
                                    break;
                                case "AR":
                                    REGION = "Аргентина";
                                    break;
                                case "AM":
                                    REGION = "Армения";
                                    break;
                                case "AW":
                                    REGION = "Аруба";
                                    break;
                                case "AF":
                                    REGION = "Афганистан";
                                    break;
                                case "BS":
                                    REGION = "Багамские Острова";
                                    break;
                                case "BD":
                                    REGION = "Бангладеш";
                                    break;
                                case "BB":
                                    REGION = "Барбадос";
                                    break;
                                case "BH":
                                    REGION = "Бахрейн";
                                    break;
                                case "BZ":
                                    REGION = "Белиз";
                                    break;
                                case "BY":
                                    REGION = "Белоруссия";
                                    break;
                                case "BE":
                                    REGION = "Бельгия";
                                    break;
                                case "BJ":
                                    REGION = "Бенин";
                                    break;
                                case "BM":
                                    REGION = "Бермудские Острова";
                                    break;
                                case "BG":
                                    REGION = "Болгария";
                                    break;
                                case "BO":
                                    REGION = "Боливия";
                                    break;
                                case "BA":
                                    REGION = "Босния и Герцеговина";
                                    break;
                                case "BW":
                                    REGION = "Ботсвана";
                                    break;
                                case "BR":
                                    REGION = "Бразилия";
                                    break;
                                case "IO":
                                    REGION = "Британская территория в Индийском океане";
                                    break;                                
                                case "BN":
                                    REGION = "Бруней";
                                    break;
                                case "BV":
                                    REGION = "Буве";
                                    break;
                                case "BF":
                                    REGION = "Буркина-Фасо";
                                    break;
                                case "BI":
                                    REGION = "Бурунди";
                                    break;
                                case "BT":
                                    REGION = "Бутан";
                                    break;
                                case "VU":
                                    REGION = "Вануату";
                                    break;
                                case "VA":
                                    REGION = "Ватикан";
                                    break;
                                case "GB":
                                    REGION = "Великобритания";
                                    break;
                                case "HU":
                                    REGION = "Венгрия";
                                    break;
                                case "VE":
                                    REGION = "Венесуэла";
                                    break;
                                case "VG":
                                    REGION = "Виргинские Острова (Британские)";
                                    break;
                                case "VI":
                                    REGION = "Виргинские Острова (США)";
                                    break;
                                case "TP":
                                    REGION = "Восточный Тимор";
                                    break;
                                case "VN":
                                    REGION = "Вьетнам";
                                    break;
                                case "GA":
                                    REGION = "Габон";
                                    break;
                                case "HT":
                                    REGION = "Гаити";
                                    break;
                                case "GY":
                                    REGION = "Гайана";
                                    break;
                                case "GM":
                                    REGION = "Гамбия";
                                    break;
                                case "GH":
                                    REGION = "Гана";
                                    break;
                                case "GP":
                                    REGION = "Гваделупа";
                                    break;
                                case "GT":
                                    REGION = "Гватемала";
                                    break;
                                case "GF":
                                    REGION = "Гвиана Французская";
                                    break;
                                case "GN":
                                    REGION = "Гвинея";
                                    break;
                                case "GW":
                                    REGION = "Гвинея-Бисау";
                                    break;
                                case "DE":
                                    REGION = "Германия";
                                    break;
                                case "GI":
                                    REGION = "Гибралтар";
                                    break;
                                case "HN":
                                    REGION = "Гондурас";
                                    break;
                                case "HK":
                                    REGION = "Гонконг";
                                    break;
                                case "GD":
                                    REGION = "Гренада";
                                    break;
                                case "GL":
                                    REGION = "Гренландия";
                                    break;
                                case "GR":
                                    REGION = "Греция";
                                    break;
                                case "GE":
                                    REGION = "Грузия";
                                    break;
                                case "GU":
                                    REGION = "Гуам";
                                    break;
                                case "DK":
                                    REGION = "Дания";
                                    break;
                                case "DJ":
                                    REGION = "Джибути";
                                    break;
                                case "DM":
                                    REGION = "Доминика";
                                    break;
                                case "DO":
                                    REGION = "Доминиканская Республика";
                                    break;
                                case "EG":
                                    REGION = "Египет";
                                    break;
                                case "ZR":
                                    REGION = "Заир";
                                    break;
                                case "ZM":
                                    REGION = "Замбия";
                                    break;
                                case "EH":
                                    REGION = "Западная Сахара";
                                    break;
                                case "WS":
                                    REGION = "Западное Самоа";
                                    break;
                                case "ZW":
                                    REGION = "Зимбабве";
                                    break;
                                case "IL":
                                    REGION = "Израиль";
                                    break;
                                case "IN":
                                    REGION = "Индия";
                                    break;
                                case "ID":
                                    REGION = "Индонезия";
                                    break;
                                case "JO":
                                    REGION = "Иордания";
                                    break;
                                case "IQ":
                                    REGION = "Ирак";
                                    break;
                                case "IR":
                                    REGION = "Иран";
                                    break;
                                case "IE":
                                    REGION = "Ирландия";
                                    break;
                                case "IS":
                                    REGION = "Исландия";
                                    break;
                                case "ES":
                                    REGION = "Испания";
                                    break;
                                case "IT":
                                    REGION = "Италия";
                                    break;
                                case "YE":
                                    REGION = "Йемен";
                                    break;
                                case "CV":
                                    REGION = "Кабо-Верде";
                                    break;
                                case "KZ":
                                    REGION = "Казахстан";
                                    break;
                                case "KH":
                                    REGION = "Камбоджа";
                                    break;
                                case "CM":
                                    REGION = "Камерун";
                                    break;
                                case "CA":
                                    REGION = "Канада";
                                    break;
                                case "QA":
                                    REGION = "Катар";
                                    break;
                                case "KE":
                                    REGION = "Кения";
                                    break;
                                case "CY":
                                    REGION = "Кипр";
                                    break;
                                case "KG":
                                    REGION = "Киргизия";
                                    break;
                                case "KI":
                                    REGION = "Кирибати";
                                    break;
                                case "CN":
                                    REGION = "Китай";
                                    break;
                                case "CC":
                                    REGION = "Кокосовые Острова";
                                    break;
                                case "CO":
                                    REGION = "Колумбия";
                                    break;
                                case "KM":
                                    REGION = "Коморские острова";
                                    break;
                                case "CG":
                                    REGION = "Конго";
                                    break;
                                case "KP":
                                    REGION = "КНДР";
                                    break;
                                case "KR":
                                    REGION = "Корея";
                                    break;
                                case "CR":
                                    REGION = "Коста-Рика";
                                    break;
                                case "CI":
                                    REGION = "Кот-д'Ивуар";
                                    break;
                                case "CU":
                                    REGION = "Куба";
                                    break;
                                case "KW":
                                    REGION = "Кувейт";
                                    break;
                                case "LA":
                                    REGION = "Лаос";
                                    break;
                                case "LV":
                                    REGION = "Латвия";
                                    break;
                                case "LS":
                                    REGION = "Лесото";
                                    break;
                                case "LR":
                                    REGION = "Либерия";
                                    break;
                                case "LB":
                                    REGION = "Ливан";
                                    break;
                                case "LY":
                                    REGION = "Ливия";
                                    break;
                                case "LT":
                                    REGION = "Литва";
                                    break;
                                case "LI":
                                    REGION = "Лихтенштейн";
                                    break;
                                case "LU":
                                    REGION = "Люксембург";
                                    break;
                                case "MU":
                                    REGION = "Маврикий";
                                    break;
                                case "MR":
                                    REGION = "Мавритания";
                                    break;
                                case "MG":
                                    REGION = "Мадагаскар";
                                    break;
                                case "YT":
                                    REGION = "Майотта";
                                    break;
                                case "MO":
                                    REGION = "Макао";
                                    break;
                                case "MK":
                                    REGION = "Македония";
                                    break;
                                case "MW":
                                    REGION = "Малави";
                                    break;
                                case "MY":
                                    REGION = "Малайзия";
                                    break;
                                case "ML":
                                    REGION = "Мали";
                                    break;
                                case "MV":
                                    REGION = "Мальдивы";
                                    break;
                                case "MT":
                                    REGION = "Мальта";
                                    break;
                                case "MA":
                                    REGION = "Марокко";
                                    break;
                                case "MQ":
                                    REGION = "Мартиника";
                                    break;
                                case "MH":
                                    REGION = "Маршаловы Острова";
                                    break;
                                case "MX":
                                    REGION = "Мексика";
                                    break;
                                case "UM":
                                    REGION = "Мелкие отдаленные острова США";
                                    break;
                                case "FM":
                                    REGION = "Микронезия";
                                    break;
                                case "MZ":
                                    REGION = "Мозамбик";
                                    break;
                                case "MD":
                                    REGION = "Молдавия";
                                    break;
                                case "MC":
                                    REGION = "Монако";
                                    break;
                                case "MN":
                                    REGION = "Монголия";
                                    break;
                                case "MS":
                                    REGION = "Монтсеррат";
                                    break;
                                case "MM":
                                    REGION = "Мьянма";
                                    break;
                                case "NA":
                                    REGION = "Намибия";
                                    break;
                                case "NR":
                                    REGION = "Науру";
                                    break;
                                case "NT":
                                    REGION = "Нейтральная зона";
                                    break;
                                case "NP":
                                    REGION = "Непал";
                                    break;
                                case "NE":
                                    REGION = "Нигер";
                                    break;
                                case "NG":
                                    REGION = "Нигерия";
                                    break;
                                case "NL":
                                    REGION = "Нидерланды";
                                    break;
                                case "NI":
                                    REGION = "Никарагуа";
                                    break;
                                case "NU":
                                    REGION = "Ниуэ";
                                    break;
                                case "NZ":
                                    REGION = "Новая Зеландия";
                                    break;
                                case "NC":
                                    REGION = "Новая Каледония";
                                    break;
                                case "NO":
                                    REGION = "Норвегия";
                                    break;
                                case "NF":
                                    REGION = "Норфолк";
                                    break;
                                case "AE":
                                    REGION = "ОАЭ";
                                    break;
                                case "OM":
                                    REGION = "Оман";
                                    break;
                                case "SH":
                                    REGION = "Остров Святой Елены";
                                    break;
                                case "CX":
                                    REGION = "Остров Рождества";
                                    break;
                                case "CK":
                                    REGION = "Острова Кука";
                                    break;
                                case "KY":
                                    REGION = "Острова Кайман";
                                    break;
                                case "PK":
                                    REGION = "Пакистан";
                                    break;
                                case "PW":
                                    REGION = "Палау";
                                    break;
                                case "PA":
                                    REGION = "Панама";
                                    break;
                                case "PG":
                                    REGION = "Папуа-Новая Гвинея";
                                    break;
                                case "PY":
                                    REGION = "Парагвай";
                                    break;
                                case "PE":
                                    REGION = "Перу";
                                    break;
                                case "PN":
                                    REGION = "Питкэрн";
                                    break;
                                case "PL":
                                    REGION = "Польша";
                                    break;
                                case "PT":
                                    REGION = "Португалия";
                                    break;
                                case "PR":
                                    REGION = "Пуэрто-Рико";
                                    break;
                                case "RE":
                                    REGION = "Реюньон";
                                    break;
                                case "RU":
                                    REGION = "Россия";
                                    break;
                                case "RW":
                                    REGION = "Руанда";
                                    break;
                                case "RO":
                                    REGION = "Румыния";
                                    break;
                                case "SV":
                                    REGION = "Сальвадор";
                                    break;
                                case "SM":
                                    REGION = "Сан-Марино";
                                    break;
                                case "ST":
                                    REGION = "Сан-Томе и Принсипи";
                                    break;
                                case "SA":
                                    REGION = "Саудовская Аравия";
                                    break;
                                case "SZ":
                                    REGION = "Свазиленд";
                                    break;
                                case "SJ":
                                    REGION = "Свальбард и Ян-Майен";
                                    break;
                                case "MP":
                                    REGION = "Северные Марианские Острова";
                                    break;
                                case "SC":
                                    REGION = "Сейшельские Острова";
                                    break;
                                case "SN":
                                    REGION = "Сенегал";
                                    break;
                                case "PM":
                                    REGION = "АлжСен-Пьер Микелонир";
                                    break;
                                case "VC":
                                    REGION = "Сент-Винсент и Гренадины";
                                    break;
                                case "KN":
                                    REGION = "Сент-Китс и Невис";
                                    break;
                                case "LC":
                                    REGION = "Сент-Люсия";
                                    break;
                                case "SG":
                                    REGION = "Сингапур";
                                    break;
                                case "SY":
                                    REGION = "Сирия";
                                    break;
                                case "SK":
                                    REGION = "Словакия";
                                    break;
                                case "SI":
                                    REGION = "Словения";
                                    break;
                                case "US":
                                    REGION = "США";
                                    break;
                                case "SB":
                                    REGION = "Соломоновы Острова";
                                    break;
                                case "SO":
                                    REGION = "Сомали";
                                    break;
                                case "SD":
                                    REGION = "Судан";
                                    break;
                                case "SR":
                                    REGION = "Суринам";
                                    break;
                                case "SL":
                                    REGION = "Сьерра-Леоне";
                                    break;
                                case "TJ":
                                    REGION = "Таджикистан";
                                    break;
                                case "TH":
                                    REGION = "Тайланд";
                                    break;
                                case "TW":
                                    REGION = "Тайвань";
                                    break;
                                case "TZ":
                                    REGION = "Танзания";
                                    break;
                                case "TC":
                                    REGION = "Тёркс и Кайкос";
                                    break;
                                case "TG":
                                    REGION = "Того";
                                    break;
                                case "TK":
                                    REGION = "Токелау Острова";
                                    break;
                                case "TO":
                                    REGION = "Тонго";
                                    break;
                                case "TT":
                                    REGION = "Тринидад и Тобаго";
                                    break;
                                case "TV":
                                    REGION = "Тувалу";
                                    break;
                                case "TN":
                                    REGION = "Тунис";
                                    break;
                                case "TM":
                                    REGION = "Туркменистан";
                                    break;
                                case "TR":
                                    REGION = "Турция";
                                    break;
                                case "UG":
                                    REGION = "Уганда";
                                    break;
                                case "UZ":
                                    REGION = "Узбекистан";
                                    break;
                                case "UA":
                                    REGION = "Украина";
                                    break;
                                case "WF":
                                    REGION = "Уоллис и Футуна";
                                    break;
                                case "UY":
                                    REGION = "Уругвай";
                                    break;
                                case "FO":
                                    REGION = "Фарерские Острова";
                                    break;
                                case "FJ":
                                    REGION = "Фиджи";
                                    break;
                                case "PH":
                                    REGION = "Филиппины";
                                    break;
                                case "FI":
                                    REGION = "Финляндия";
                                    break;
                                case "FK":
                                    REGION = "Фолклендские Острова";
                                    break;
                                case "FR":
                                    REGION = "Франция";
                                    break;                                
                                case "PF":
                                    REGION = "Французская Полинезия";
                                    break;
                                case "TF":
                                    REGION = "Французские Южные Территории";
                                    break;
                                case "HM":
                                    REGION = "Херд и Макдональд Острова";
                                    break;
                                case "HR":
                                    REGION = "Хорватия";
                                    break;
                                case "CF":
                                    REGION = "Центральноафриканская республика";
                                    break;
                                case "TD":
                                    REGION = "Чад";
                                    break;
                                case "CZ":
                                    REGION = "Чешская Республика";
                                    break;
                                case "CL":
                                    REGION = "Чили";
                                    break;
                                case "CH":
                                    REGION = "Швейцария";
                                    break;
                                case "SE":
                                    REGION = "Швеция";
                                    break;
                                case "LK":
                                    REGION = "Шри-Ланка";
                                    break;
                                case "EC":
                                    REGION = "Эквадор";
                                    break;
                                case "GQ":
                                    REGION = "Экваториальная Гвинея";
                                    break;
                                case "ER":
                                    REGION = "Эритрея";
                                    break;
                                case "EE":
                                    REGION = "Эстония";
                                    break;
                                case "ET":
                                    REGION = "Эфиопия";
                                    break;
                                case "YU":
                                    REGION = "Югославия";
                                    break;
                                case "GS":
                                    REGION = "Южная Георгия и Южные Сандвичевы Острова";
                                    break;
                                case "ZA":
                                    REGION = "ЮАР";
                                    break;
                                case "JM":
                                    REGION = "Ямайка";
                                    break;
                                case "JP":
                                    REGION = "Япония";
                                    break;                                
                                default:
                                    REGION = response_global.sys.country;
                                    break;
                            }
                            await botClient.SendTextMessageAsync(message.Chat, "(" + UnixTimeStampToDateTime(response_global.dt).ToString("G", CultureInfo.CreateSpecificCulture("ru-RU")) + ")\n" + 
                                response_global.name + UTC + (response_global.timezone / 3600) + ":00 / " + REGION +
                                "\nКоординаты: " + response_global.coord.lon.ToString().Replace("," , ".") + " , " + response_global.coord.lat.ToString().Replace("," , ".") + 
                                "\n\nПогода: " + (char.ToUpper(response_global.weather[0].description[0]) + response_global.weather[0].description.Substring(1)) + 
                                "\n\nТемпература: " + response_global.main.temp.ToString().Replace(",", ".") + " °C" +
                                "\nОщущается как: " + response_global.main.feels_like.ToString().Replace(",", ".") + " °C" +
                                "\nMin температура: " + response_global.main.temp_min.ToString().Replace(",", ".") + " °C" + 
                                "\nMax температура: " + response_global.main.temp_max.ToString().Replace(",", ".") + " °C" +
                                "\nДавление: " + Math.Round((response_global.main.pressure / 1.333d), 0) + " мм. рт. ст." +
                                "\nВлажность: " + response_global.main.humidity + " %" +
                                "\n\nВетер: " + response_global.wind.speed + " м/с" +
                                "\nПорывы ветра до: " + response_global.wind.gust + " м/с" + 
                                "\nНаправление ветра: " + WIND_DEG +
                                "\n\nВосход солнца: " + UnixTimeStampToDateTimeLocal(response_global.sys.sunrise + response_global.timezone).ToString("G", CultureInfo.CreateSpecificCulture("ru-RU")) +
                                "\nЗаказ солнца: " + UnixTimeStampToDateTimeLocal(response_global.sys.sunset + response_global.timezone).ToString("G", CultureInfo.CreateSpecificCulture("ru-RU")));
                            Console.WriteLine("Name of city: {0}", response_global.name);
                            Console.WriteLine("Successful");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            await botClient.SendTextMessageAsync(message.Chat, "Ответ от сервиса не получен или введен несуществующий город");
                        }
                    }
                    else
                        await botClient.SendTextMessageAsync(message.Chat, "Введите название города, для получения информации о погоде");
                }
            }
        }
        public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("Запущен бот " + botClient.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandlePollingErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.WriteLine("====================================");
            Console.ReadLine();
        }
    }
}
