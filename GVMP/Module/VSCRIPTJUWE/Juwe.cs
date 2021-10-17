﻿using GTANetworkAPI;
using System;
using System.Collections.Generic;

namespace GVMP
{
    public class juwe : GVMP.Module.Module<juwe>
    {
        public static List<JuweModel> Juwe_ = new List<JuweModel>();

        
        protected override bool OnLoad()
        {

            Juwe_.Add(new JuweModel
            {
                Position = new Vector3(-626.77, -235.5, 38.06),
                robbed = false
            });

            Juwe_.Add(new JuweModel
            {
                Position = new Vector3(-623.12, -233.07, 38.06),
                robbed = false
            }); Juwe_.Add(new JuweModel
            {
                Position = new Vector3(-627.95, -233.86, 38.06),
                robbed = false
            }); Juwe_.Add(new JuweModel
            {
                Position = new Vector3(-624.68, -230.86, 38.06),
                robbed = false
            }); Juwe_.Add(new JuweModel
            {
                Position = new Vector3(-626.61835, -238.43246, 36.957073),
                robbed = false
            });

            Juwe_.ForEach(x => {

                ColShape val1 = NAPI.ColShape.CreateCylinderColShape(x.Position, 1.4f, 2.4f, 0);
                val1.SetData("FUNCTION_MODEL", new FunctionModel("robJuwe"));
                val1.SetData("MESSAGE", new Message("Drücke E um den Juwe auszurauben", "", "red", 3000));
                NAPI.Marker.CreateMarker(29, x.Position, new Vector3(), new Vector3(), 1.0f, new Color(255, 0, 0), false, 0);

            });
            return true;
        }


        [RemoteEvent("robJuwe")]
        public static void robATM(Client client)
        {
            try
            {
                if (client == null) return;
                DbPlayer dbPlayer = client.GetPlayer();

                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;


                Juwe_.ForEach(x =>
                {
                    if (x.Position.DistanceTo(client.Position) <= 1.5f)
                    {
                        if (!dbPlayer.IsFarming)
                        {
                            if (!x.robbed)
                            {
                                dbPlayer.disableAllPlayerActions(true);
                                dbPlayer.AllActionsDisabled = true;
                                dbPlayer.SendProgressbar(15000);
                                dbPlayer.IsFarming = true;
                                Notification.SendGlobalNotification("Juwe wird aufgebrochen!", 100000, "lightblue", Notification.icon.bullhorn);
                                dbPlayer.RefreshData(dbPlayer);
                                dbPlayer.PlayAnimation(33, "amb@world_human_welding@male@idle_a", "idle_a", 8f);
                                dbPlayer.SendNotification("Du raubst nun den Juwe aus!", 2000, "grey");
                                x.robbed = true;



                                NAPI.Task.Run(delegate
                                {
                                    dbPlayer.TriggerEvent("client:respawning");
                                    dbPlayer.StopProgressbar();
                                    dbPlayer.UpdateInventoryItems("Diamant", 2, false);
                                    dbPlayer.IsFarming = false;
                                    dbPlayer.RefreshData(dbPlayer);
                                    dbPlayer.disableAllPlayerActions(false);
                                    dbPlayer.StopAnimation();
                                    dbPlayer.SendNotification("Du hast den Juwe erfolgreich ausgeraubt!!", 2000, "green");
                                }, 15000);

                            }
                            else
                            {
                                dbPlayer.SendNotification("Juwe wurde bereits angegriffen.", 3000, "red", "Juwe");
                                return;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION robATM] " + ex);
            }
        }
    }
}
