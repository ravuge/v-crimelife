using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTANetworkAPI;
using GVMP;
using MySql.Data.MySqlClient;

namespace crimelife
{

    public class Bank3 : GVMP.Module.Module<Bank3>
    {
        public static List<Bank3> clothingList = new List<Bank3>();
        public static List<Bank3> BlockedZones = new List<Bank3>();
        private static Bank3 awd;

        public static object mySqlReaderCon { get; private set; }

        protected override bool OnLoad()
        {

            Vector3 Position2 = new Vector3(259.41, 217.96, 101.68);





            ColShape val2 = NAPI.ColShape.CreateCylinderColShape(Position2, 1.4f, 2.4f, 0);

            val2.SetData("FUNCTION_MODEL", new FunctionModel("openBank3"));
            val2.SetData("MESSAGE", new Message("Bank", "", "red", 3000));


            NAPI.Marker.CreateMarker(0, Position2, new Vector3(), new Vector3(), 1.0f, new Color(255, 0, 0), false, 0);


            ;
            ;
            return true;
        }



        [RemoteEvent("openBank3")]
        public static void openBank3(Client client)
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
                        dbPlayer.SendNotification("Diese Bank wurde bereits gemacht.", 3000, "orange", "Bank");
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
                Logger.Print("[EXCEPTION openBank3] " + ex.Message);
                Logger.Print("[EXCEPTION openBank3] " + ex.StackTrace);
            }
        }
    }
}
