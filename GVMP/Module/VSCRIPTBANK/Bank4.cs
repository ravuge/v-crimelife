using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTANetworkAPI;
using GVMP;
using MySql.Data.MySqlClient;

namespace crimelife
{
   
    public class Bank5 : GVMP.Module.Module<Bank5>
    {
        public static List<Bank5> clothingList = new List<Bank5>();
        public static List<Bank5> BlockedZones = new List<Bank5>();
        private static Bank5 awd;

        public static object mySqlReaderCon { get; private set; }

        protected override bool OnLoad()
        {

            Vector3 Position2 = new Vector3(256.95, 214.69, 101.68);





            ColShape val2 = NAPI.ColShape.CreateCylinderColShape(Position2, 1.4f, 2.4f, 0);

            val2.SetData("FUNCTION_MODEL", new FunctionModel("openBank5"));
            val2.SetData("MESSAGE", new Message("Bank", "", "red", 3000));


            NAPI.Marker.CreateMarker(0, Position2, new Vector3(), new Vector3(), 1.0f, new Color(255, 0, 0), false, 0);


            ;
            ;
            return true;
        }


    
        [RemoteEvent("openBank5")]
        public static void openBank5(Client client)
        {
            try
            {//Vscript_DerEchte
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
                            
                        return;
                    }



                    dbPlayer.disableAllPlayerActions(true);
                    dbPlayer.AllActionsDisabled = true;
                    dbPlayer.SendProgressbar(3000);
                    Notification.SendGlobalNotification("Bank wird aufgebrochen!", 100000, "lightblue", Notification.icon.bullhorn);
                    dbPlayer.IsFarming = true;
                    WebhookSender.SendMessage("BANK", "Bank wird eingesammelt " + dbPlayer.Name + "sammelt ", Webhooks.BANK, "BANK");
                    dbPlayer.RefreshData(dbPlayer);
                    dbPlayer.SendNotification("Bank wird aufgebrochen  ");

                    dbPlayer.PlayAnimation(33, "amb@medic@standing@tendtodead@idle_a", "idle_a", 8f);
                    NAPI.Task.Run(delegate
                    {
                        dbPlayer.TriggerEvent("client:respawning");
                        dbPlayer.StopProgressbar();


                        dbPlayer.UpdateInventoryItems("Goldbarren", 2, false);
                        dbPlayer.IsFarming = false;
                        dbPlayer.RefreshData(dbPlayer);
                        dbPlayer.disableAllPlayerActions(false);
                        dbPlayer.StopAnimation();
                        BlockedZones.Add(awd);
                    }, 3000);
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION openBank5] " + ex.Message);
                Logger.Print("[EXCEPTION openBank5] " + ex.StackTrace);
            }
        }
    }
}
