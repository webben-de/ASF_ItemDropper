using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ArchiSteamFarm;
using ArchiSteamFarm.Localization;
using SteamKit2;
using SteamKit2.Internal;
using System.Globalization;
using System.Collections.Concurrent;

namespace ASFItemDropManager {

	public sealed class ItemDropHandler : ClientMsgHandler {
		private SteamUnifiedMessages.UnifiedService<IInventory> _inventoryService;

		ConcurrentDictionary<ulong, StoredResponse> Responses = new ConcurrentDictionary<ulong, StoredResponse>();

		

		public override void HandleMsg(IPacketMsg packetMsg) {
			var handler = Client.GetHandler<SteamUnifiedMessages>();

			if (packetMsg == null) {
				ASF.ArchiLogger.LogNullError(nameof(packetMsg));

				return;
			}

			switch (packetMsg.MsgType) {
				case EMsg.ClientGetUserStatsResponse:
					HandleGetUserStatsResponse(packetMsg);

					break;
				case EMsg.ClientStoreUserStatsResponse:
					HandleStoreUserStatsResponse(packetMsg);

					break;
			}

		}

	

	

	
		internal string itemIdleingStart(Bot bot, uint appid)
        {
			ClientMsgProtobuf<CMsgClientGamesPlayed> response = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
			response.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed {
				game_id = new GameID( appid ),
				steam_id_gs = bot.SteamID
				//  steam_id_for_user = bot.SteamID

			});

			Client.Send(response);
			return "Start idling for " + appid;
        }
		internal async Task<string> checkTime(uint appid, uint itemdefid)
		{
			CInventory_ConsumePlaytime_Request reponse = new CInventory_ConsumePlaytime_Request { appid = appid, itemdefid = itemdefid };
			var steamUnifiedMessages = Client.GetHandler<SteamUnifiedMessages>();
			_inventoryService = steamUnifiedMessages.CreateService<IInventory>();

			var responce = await _inventoryService.SendMessage(x => x.ConsumePlaytime(reponse));
			var result = responce.GetDeserializedResponse<CInventory_Response>();
			if (result.item_json != "[]")
			{
				try
				{
					Console.WriteLine(result.item_json);
					return $"Item droped {Client.SteamID} game:{appid}\n{result.item_json}";
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}


				Console.WriteLine($"Item droped {Client.SteamID} game:{appid}\n{result.item_json}\n");
			}
			return "Item droped {Client.SteamID} game:{appid}\n{result.item_json}";
        }
		internal string itemIdleingStop(Bot bot)
		{
			ClientMsgProtobuf<CMsgClientGamesPlayed> response = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
			{
				response.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = 0 });
			}

			Client.Send(response);
			return "Stop idling ";
		}
		
	}

}
