using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace MarketBot
{
    class HttpGetInfo
    {
        public static string GetMoney()
        {
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

                return responseData;
            }

        }
        public static string GetAvatar()
        {
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
            return responseData;
        }
        public static string GetPing()
        {
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
            return responseData;
        }
        public static string GetItems()
        {
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
            return responseData;
        }
        public static string GetSteamInventory()
        {
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
            return responseData;
        }
        public static string GetId(string current_item)
        {
            string[] strings = current_item.Split(":");
            return strings[strings.Length - 1];
        }
        public static string SetSell(string id, int price, string currency)
        {
            string responseData = string.Empty;
            string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389&id={id}&price={price}&cur={currency}";

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
            return responseData;
        }
        public static string SetPrice(string item_id, int price, string currency)
        {
            string responseData = string.Empty;
            string actionUrl = $"https://market.csgo.com/api/v2/set-price?key=5Gpq3KhWO0u4t3L60mYY9VLzsjuv389&item_id={item_id}&price={price}&cur={currency}";

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
            return responseData;
        }
    }
}
