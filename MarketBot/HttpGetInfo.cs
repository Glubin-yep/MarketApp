using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using static MarketBot.User_Date;
using System.Windows.Media.Imaging;

namespace MarketBot
{
    class HttpGetInfo
    {

        public static string GetMoney()
        {
            var user_Date = new User_Date.Balans();
            string responseData = string.Empty;
            const string actionUrl = "https://market.csgo.com/api/v2/get-money?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = JsonConvert.DeserializeObject<User_Date.Balans>(responseData);
            return user_Date.money.ToString();
        }
        public static BitmapImage GetAvatar()
        {
            var user_Date = new User_Date.User();
            string responseData = string.Empty;
            const string actionUrl = "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=00F44B78D419316F764EAD327522119D&steamids=76561198829066528";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = JsonConvert.DeserializeObject<User_Date.User>(responseData);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user_Date.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();
            

            return bitmapImage;
        }
        public static string GetNickname()
        {
            var user_Date = new User_Date.User();
            string responseData = string.Empty;
            const string actionUrl = "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=00F44B78D419316F764EAD327522119D&steamids=76561198829066528";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = JsonConvert.DeserializeObject<User_Date.User>(responseData);

            return user_Date.response.players.First().personaname;
        }
        public static bool GetPing()
        {
            var user_Date = new User_Date.Ping();
            string responseData = string.Empty;
            const string actionUrl = "https://market.csgo.com/api/v2/ping?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())
            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = JsonConvert.DeserializeObject<User_Date.Ping>(responseData);
            return user_Date.online;
        }
        public static Items GetItems()
        {
            var user_Date = new User_Date.Items();
            string responseData = string.Empty;
            const string actionUrl = "https://market.csgo.com/api/v2/items?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = user_Date = JsonConvert.DeserializeObject<User_Date.Items>(responseData);
            return user_Date;
        }
        public static Inventory GetSteamInventory()
        {
            var user_Date = new User_Date.Inventory();
            string responseData = string.Empty;
            const string actionUrl = "https://market.csgo.com/api/v2/my-inventory/?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = user_Date = JsonConvert.DeserializeObject<User_Date.Inventory>(responseData);
            return user_Date;
        }
        public static string GetId(string current_item)
        {
            string[] strings = current_item.Split(":");
            return strings[strings.Length - 1];
        }
        public static Sell SetSell(string id,string price, string currency)
        {
            var user_Date = new User_Date.Sell();
            id = GetId(id);
            string responseData = string.Empty;
            string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389&id={id}&price={price}00&cur={currency}";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = user_Date = JsonConvert.DeserializeObject<User_Date.Sell>(responseData);
            return user_Date;
        }
        public static Update SetPrice(string item_id, string price, string currency)
        {
            var user_Date = new User_Date.Update();
            item_id = GetId(item_id);
            string responseData = string.Empty;
            string actionUrl = $"https://market.csgo.com/api/v2/set-price?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389&item_id={item_id}&price={price}00&cur={currency}";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())

            {

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseData = reader.ReadToEnd();
                            reader.Close();
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            user_Date = user_Date = JsonConvert.DeserializeObject<User_Date.Update>(responseData);
            return user_Date;
        }
    }
}
