using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GVMP
{
    class WebhookSender : GVMP.Module.Module<WebhookSender>
    {
        public static async void SendMessage(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"V\",\"avatar_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"V Crimelife\",\"url\":\"https://discord.gg/cghkj5t8Dv\",\"icon_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"url\":\"https://discord.gg/cghkj5t8Dv\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"V Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage423(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"Breschaft\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/851918361880690728/879000791799713802/image0.gif\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"V Crimelife\",\"url\":\"https://discord.gg/EpHsXj793d\",\"icon_url\":\"https://cdn.discordapp.com/attachments/851918361880690728/879000791799713802/image0.gif\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/attachments/851918361880690728/879000791799713802/image0.gif\"},\"url\":\"https://discord.gg/EpHsXj793d\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"V Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage321(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"V\",\"avatar_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"V Crimelife\",\"url\":\"https://discord.gg/cghkj5t8Dv\",\"icon_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"url\":\"https://discord.gg/cghkj5t8Dv\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"V Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage123(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"V\",\"avatar_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"V Crimelife\",\"url\":\"https://discord.gg/cghkj5t8Dv\",\"icon_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"url\":\"https://discord.gg/cghkj5t8Dv\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"V Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage534(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"V\",\"avatar_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"V Crimelife\",\"url\":\"https://discord.gg/cghkj5t8Dv\",\"icon_url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://media.discordapp.net/attachments/879848060694433922/882625173407268884/image0.gif\"},\"url\":\"https://discord.gg/cghkj5t8Dv\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"V Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }
    }
}
