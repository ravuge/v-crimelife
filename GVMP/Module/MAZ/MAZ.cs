using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTANetworkAPI;
using GVMP;
using MySql.Data.MySqlClient;

namespace crimelife
{
    public class MAZ : GVMP.Module.Module<MAZ>
    {
        public static List<MAZ> clothingList = new List<MAZ>();
        public static List<MAZ> BlockedZones = new List<MAZ>();
        private static MAZ awd;

        public static object mySqlReaderCon { get; private set; }

        protected override bool OnLoad()
        {
            Vector3 Position = new Vector3(2055.69, 3433.75, 44.07);

            ColShape val = NAPI.ColShape.CreateCylinderColShape(Position, 1.4f, 2.4f, 0);
            val.SetData("FUNCTION_MODEL", new FunctionModel("openMAZ"));
            val.SetData("MESSAGE", new Message("Drücke E", "", "red", 3000));

            NAPI.Marker.CreateMarker(1, Position, new Vector3(), new Vector3(), 1.0f, new Color(255, 0, 0), false, 0);

         ;
         ;

            return true;
        }

        [RemoteEvent("openMAZ")]
        public static void openMAZ(Client client)
        {
            try
            {
                if (client == null) return;
                DbPlayer dbPlayer = client.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;
                Laboratory result = null;
                float distance = 99999;
            


                if (!dbPlayer.IsFarming)
                {

                    if (BlockedZones.Contains(awd))
                    {
                        dbPlayer.SendNotification("Absturz wurde bereits gemacht.", 3000, "Red", "Absturz");
                        return;
                    }

                    dbPlayer.disableAllPlayerActions(true);
                        dbPlayer.SendProgressbar(119999);
                        dbPlayer.IsFarming = true;
                        Notification.SendGlobalNotification("Zentrales Flugabwehrsystem: Das Flugzeug wird aufgebrochen!", 10000, "lightblue", Notification.icon.bullhorn);
                        dbPlayer.RefreshData(dbPlayer);
                        dbPlayer.SendNotification("Viel Spaß beim 5 Minuten schweißen .");
                    WebhookSender.SendMessage("MAZ", "Das Flugzeug wird aufgebrochen " + dbPlayer.Name + "schweißt ", Webhooks.MAZ, "MAZ");
                    dbPlayer.PlayAnimation(33, "amb@world_human_welding@male@idle_a", "idle_a", 8f);
                    BlockedZones.Add(awd);
                    NAPI.Task.Run(delegate
                        {
                           
                            dbPlayer.TriggerEvent("client:respawning");
                            dbPlayer.StopProgressbar();
                     
                            dbPlayer.UpdateInventoryItems("Gusenberg", 20, false);
           
                            dbPlayer.IsFarming = false;
                            dbPlayer.RefreshData(dbPlayer);
                            dbPlayer.disableAllPlayerActions(false);
                            dbPlayer.StopAnimation();
                          
                        }, 119999);
                    }
            }
            catch (Exception ex)
                    {
                        Logger.Print("[EXCEPTION openMAZ] " + ex.Message);
                        Logger.Print("[EXCEPTION openMAZ] " + ex.StackTrace);
            }
        }
    }
}
