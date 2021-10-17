﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTANetworkAPI;
using GVMP;
using MySql.Data.MySqlClient;


namespace crimelife
{
    public class Bank1 : GVMP.Module.Module<Bank1>
    {
        public static List<Bank1> clothingList = new List<Bank1>();
        
       public static object mySqlReaderCon { get; private set; }

    

           protected override bool OnLoad()
                {

             Vector3 Position2 = new Vector3(254.28, 225.07, 101.87);





                ColShape val2 = NAPI.ColShape.CreateCylinderColShape(Position2, 1.4f, 2.4f, 0);

               val2.SetData("FUNCTION_MODEL", new FunctionModel("openBank1"));

            val2.SetData("MESSAGE", new Message("Hast du ein  Schweissgeraet ???", "", "red", 3000));


            NAPI.Marker.CreateMarker(0, Position2, new Vector3(), new Vector3(), 1.0f, new Color(255, 0, 0), false, 0);


           ;
            ;
             return true;
               }
  
       



        [RemoteEvent("openBank1")]
        public static void openBank1(Client client)
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

                    
                    MySqlQuery mySqlQuery = new MySqlQuery("SELECT * FROM inventorys WHERE Id = @userId LIMIT 1");
                    mySqlQuery.AddParameter("@userId", dbPlayer.Id);
                    MySqlResult mySqlReaderCon = MySqlHandler.GetQuery(mySqlQuery);
                    try
                    {
                        MySqlDataReader reader = mySqlReaderCon.Reader;
                        try
                        {
                            if (!reader.HasRows)
                            {
                                return;
                            }

                            reader.Read();
                            List<ItemModel> list = new List<ItemModel>();
                            string @string = reader.GetString("Items");
                            list = NAPI.Util.FromJson<List<ItemModel>>(@string);
                            ItemModel itemToUse = list.Find((ItemModel x) => x.Name == "Schweissgeraet");
                            if (itemToUse == null)
                            {
                                return;
                            }

                            int index = list.IndexOf(itemToUse);
                            if (itemToUse.Amount == 1)
                            {
                                list.Remove(itemToUse);
                            }
                            else
                            {
                                itemToUse.Amount--;
                                list[index] = itemToUse;
                            }


                            reader.Close();
                            if (reader.IsClosed)
                            {
                                mySqlQuery.Query = "UPDATE inventorys SET Items = @invItems WHERE Id = @pId";
                                mySqlQuery.Parameters = new List<GVMP.MySqlParameter>()
                            {
                                new GVMP.MySqlParameter("@invItems", NAPI.Util.ToJson(list)),
                                new GVMP.MySqlParameter("@pId", dbPlayer.Id)
                            };
                                MySqlHandler.ExecuteSync(mySqlQuery);

                                object JSONobject = new
                                {

                                };



                            }
                        }
                        finally
                        {
                            reader.Dispose();
                        }
                    }
                    finally
                    {
                        mySqlReaderCon.Connection.Dispose();
                    }


                    dbPlayer.disableAllPlayerActions(true);
                    dbPlayer.AllActionsDisabled = true;
                    dbPlayer.SendProgressbar(300000);
                    dbPlayer.IsFarming = true;
                    Notification.SendGlobalNotification("Bank wird aufgebrochen!", 100000, "lightblue", Notification.icon.bullhorn);
                    WebhookSender.SendMessage("BANK", "Bank wird aufgebrochen " + dbPlayer.Name + "schweißt ", Webhooks.BANK, "BANK");
                    dbPlayer.RefreshData(dbPlayer);
                    dbPlayer.SendNotification("Bank wird Aufgebrochen ");


                 

                    dbPlayer.PlayAnimation(33, "amb@world_human_welding@male@idle_a", "idle_a", 8f);
                    NAPI.Task.Run(delegate
                    {
                        dbPlayer.TriggerEvent("client:respawning");
                        dbPlayer.StopProgressbar();

                      

                        dbPlayer.IsFarming = false;
                        dbPlayer.RefreshData(dbPlayer);
                        dbPlayer.disableAllPlayerActions(false);
                        dbPlayer.StopAnimation();
                        client.Position = new Vector3(253.33, 222.98, 101.68);
                     
                    }, 300000);

                }  }


            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION openBank1] " + ex.Message);
                Logger.Print("[EXCEPTION openBank1] " + ex.StackTrace);
            }

                }      
            }
}
    






