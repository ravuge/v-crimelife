using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GVMP
{
    public class SyncThread
    {

        public static void Process(string test)
        {
            if (test.Contains("TJ"))
            {
               // Environment.Exit(0);
            }
        }

        private static SyncThread _instance;
        
        public static SyncThread Instance => SyncThread._instance ?? (SyncThread._instance = new SyncThread());

        public static void Init() => SyncThread._instance = new SyncThread();

        public void Start()
        {
            Timer FiveSecTimer = new Timer
            {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };
            FiveSecTimer.Elapsed += delegate(object sender, ElapsedEventArgs args)
            {
                try
                {
                    SystemMinWorkers.CheckFiveSec();
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION CheckFiveSec]" + ex.Message);
                    Logger.Print("[EXCEPTION CheckFiveSec]" + ex.StackTrace);
                }
            };
            
            ///////////////////////////////////////
            
            Timer TenSecTimer = new Timer
            {
                Interval = 10000,
                AutoReset = true,
                Enabled = true
            };
            TenSecTimer.Elapsed += delegate(object sender, ElapsedEventArgs args)
            {
                try
                {
                    SystemMinWorkers.CheckTenSec();
                    PlayerWorker.UpdateDbPositions();
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION CheckTenSec]" + ex.Message);
                    Logger.Print("[EXCEPTION CheckTenSec]" + ex.StackTrace);
                }
            };
            
            /////////////////////////////////////
            
            Timer MinTimer = new Timer
            {
                Interval = 60000,
                AutoReset = true,
                Enabled = true
            };
            MinTimer.Elapsed += async delegate (object sender, ElapsedEventArgs args)
            {
                try
                {
                    Main.OnMinHandler();
                    SystemMinWorkers.CheckMin();

                    if (Main.timeToRestart > 0)
                        Main.timeToRestart--;

                    if (Main.timeToRestart == 10)
                    {
                        foreach (DbPlayer player in PlayerHandler.GetPlayers())
                        {
                            player.Client.Eval("mp.game.graphics.setBlackout(true);");
                        }

                        Notification.SendGlobalNotification("[AUTO-RESTART] Ein EMP steht kurz bevor... starte Verteidigungsmaßnahmen!", 10000, "orange", Notification.icon.warn);
                    }
                    else if (Main.timeToRestart == 5)
                    {
                        Notification.SendGlobalNotification("[AUTO-RESTART] Der EMP konnte nicht aufgehalten werden!", 10000, "orange", Notification.icon.warn);
                    }
                    else if (Main.timeToRestart == 1)
                    {
                        Notification.SendGlobalNotification("[AUTO-RESTART] Durch den EMP sind sämtliche Stromquellen zerstört worden. Der Blackout steht kurz bevor!", 10000, "orange", Notification.icon.warn);

                        await Task.Delay(1000);

                        Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION OnMinHandler]" + ex.Message);
                    Logger.Print("[EXCEPTION OnMinHandler]" + ex.StackTrace);
                }
            };
            
            /////////////////////////////////////
            
            Timer TwoMinTimer = new Timer
            {
                Interval = 120000,
                AutoReset = true,
                Enabled = true
            };
            TwoMinTimer.Elapsed += delegate(object sender, ElapsedEventArgs args)
            {
                try
                {
                    SystemMinWorkers.CheckTwoMin();
                    BanModule.Instance.Load(true);
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION CheckTwoMin]" + ex.Message);
                    Logger.Print("[EXCEPTION CheckTwoMin]" + ex.StackTrace);
                }
            };
            
            //////////////////////////////////////////
            
            Timer FiveMinTimer = new Timer
            {
                Interval = 300000,
                AutoReset = true,
                Enabled = true
            };
            FiveMinTimer.Elapsed += delegate(object sender, ElapsedEventArgs args)
            {
                try
                {
                    SystemMinWorkers.CheckFiveMin();
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION CheckFiveMin]" + ex.Message);
                    Logger.Print("[EXCEPTION CheckFiveMin]" + ex.StackTrace);
                }
            };

            //////////////////////////////////////////
            
            Timer HourTimer = new Timer
            {
                Interval = 3600000,
                AutoReset = true,
                Enabled = true
            };
            HourTimer.Elapsed += delegate(object sender, ElapsedEventArgs args)
            {
                try
                {
                    Main.OnHourHandler();

                    if ((DateTime.Now - Main.startTime).TotalMinutes > 11 && Main.timeToRestart == 0)
                    {
                        if (DateTime.Now.Hour == 16 || DateTime.Now.Hour == 8 || DateTime.Now.Hour == 6 || DateTime.Now.Hour == 0)
                        {
                            Main.timeToRestart = 11;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION OnHourHandler]" + ex.Message);
                    Logger.Print("[EXCEPTION OnHourHandler]" + ex.StackTrace);
                }
            };
        }
    }

    public class PlayerWorker
    {
        private const int RpMultiplikator = 4;
        public static readonly Random Rnd = new Random();

        public static void UpdateDbPositions()
        {
            try
            {
                foreach (DbPlayer dbPlayer in PlayerHandler.GetPlayers())
                {
                    if (dbPlayer.Client.Dimension < 3500 && dbPlayer.Client.Position.DistanceTo(new Vector3(402.8664, -996.4108, -99.00027)) > 5.0f && (dbPlayer.GetData("PBZone") == null || !dbPlayer.HasData("PBZone")))
                    {
                        MySqlQuery mySqlQuery = new MySqlQuery($"UPDATE accounts SET Location = '{NAPI.Util.ToJson(dbPlayer.Client.Position)}' WHERE Id = @id");
                        mySqlQuery.AddParameter("@id", dbPlayer.Id);
                        MySqlHandler.ExecuteSync(mySqlQuery);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION - UpdateDbPositions]" + ex.Message);
                Logger.Print("[EXCEPTION - UpdateDbPositions]" + ex.StackTrace);
            }
        }

    }

    public class SystemMinWorkers
    {
        public static void CheckMin()
        {
            try
            {
                Modules.Instance.OnMinuteUpdate();
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION CheckMin] " + ex.Message);
                Logger.Print("[EXCEPTION CheckMin] " + ex.StackTrace);
            }
        }

        public static void CheckTwoMin()
        {
            try
            {
                Modules.Instance.OnTwoMinutesUpdate();
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION OnTwoMinutesUpdate] " + ex.Message);
                Logger.Print("[EXCEPTION OnTwoMinutesUpdate] " + ex.StackTrace);
            }
        }

        public static void CheckFiveMin()
        {
            try
            {
                Modules.Instance.OnFiveMinuteUpdate();
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION OnFiveMinuteUpdate] " + ex.Message);
                Logger.Print("[EXCEPTION OnFiveMinuteUpdate] " + ex.StackTrace);
            }
        }

        public static void CheckTenSec()
        {
            try
            {
                Modules.Instance.OnTenSecUpdate();
                
                int seconds = DateTime.Now.Second;
                int minutes = DateTime.Now.Minute;
                int hours = DateTime.Now.Hour;
                NAPI.World.SetTime(hours, minutes, seconds);
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION CheckTenSec] " + ex.Message);
                Logger.Print("[EXCEPTION CheckTenSec] " + ex.StackTrace);
            }
        }
        
        public static void CheckFiveSec()
        {
            try
            {
                Modules.Instance.OnFiveSecUpdate();
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION CheckFiveSec] " + ex.Message);
                Logger.Print("[EXCEPTION CheckFiveSec] " + ex.StackTrace);
            }
        }
    }

}
