using System;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using ArchiSteamFarm;
using ArchiSteamFarm.Plugins;
using ArchiSteamFarm.Localization;
using JetBrains.Annotations;
using SteamKit2;
using System.Linq;
using System.Collections.Concurrent;

namespace ASFItemDropManager {
	[Export(typeof(IPlugin))]
	// public sealed class ASFItemDropManager : IBotSteamClient, IBotCommand, IBotCardsFarmerInfo {
	public sealed class ASFItemDropManager : IBotSteamClient, IBotCommand {
		private static ConcurrentDictionary<Bot, ItemDropHandler> ItemDropHandlers = new ConcurrentDictionary<Bot, ItemDropHandler>();
		public string Name => "ASF Item Dropper";
		public Version Version => typeof(ASFItemDropManager).Assembly.GetName().Version ?? new Version("0");

		public void OnLoaded() => ASF.ArchiLogger.LogGenericInfo("ASF Item Drop Plugin by webben");

		// public static async Task<string?> OnBotFarmingStartet([NotNull] Bot bot)
        // {
		// 	return bot.Commands.FormatBotResponse(await Task.Run<string>(()=>"okay")).ConfigureAwait(false);
        // }

		public async Task<string?> OnBotCommand([NotNull] Bot bot, ulong steamID, [NotNull] string message, string[] args) {

			switch (args.Length) {
				case 0:
					bot.ArchiLogger.LogNullError(nameof(args));

					return null;
				case 1:
					switch (args[0].ToUpperInvariant()) {

						default:
							return null;
					}
				default:
					switch (args[0].ToUpperInvariant()) {
						
						case "ISTART" when args.Length > 2:
							return await StartItemIdle(steamID, bot, args[1], Utilities.GetArgsAsText(args, 2, ",")).ConfigureAwait(false);
						case "ISTOP" when args.Length > 1:
							return await StopItemIdle(steamID, bot).ConfigureAwait(false);
						case "ICHECK" when args.Length > 2:
							return await CheckItem(steamID, bot, args[1], Utilities.GetArgsAsText(args, 2, ",")).ConfigureAwait(false);
						default:
							return null;
					}
			}
		}

		public void OnBotSteamCallbacksInit([NotNull] Bot bot, [NotNull] CallbackManager callbackManager) { }

		public IReadOnlyCollection<ClientMsgHandler> OnBotSteamHandlersInit([NotNull] Bot bot) {
			ItemDropHandler CurrentBotItemDropHandler = new ItemDropHandler();
			ItemDropHandlers.TryAdd(bot, CurrentBotItemDropHandler);
			return new HashSet<ClientMsgHandler> { CurrentBotItemDropHandler };
		}

		//Responses

		

		private static async Task<string?> StartItemIdle(ulong steamID, Bot bot, string appid, string droplist)
		{
			if (!bot.HasPermission(steamID, BotConfig.EPermission.Master))
			{
				return null;
			}
		
			if (!uint.TryParse(appid, out uint appId))
			{
				return bot.Commands.FormatBotResponse(string.Format(Strings.ErrorIsInvalid, nameof(appId)));
			}
			if (!ItemDropHandlers.TryGetValue(bot, out ItemDropHandler? ItemDropHandler))
			{
				return bot.Commands.FormatBotResponse(string.Format(Strings.ErrorIsEmpty, nameof(ItemDropHandlers)));
			}
			return bot.Commands.FormatBotResponse(await Task.Run<string>(() => ItemDropHandler.itemIdleingStart(bot, appId)).ConfigureAwait(false));

		}
		private static async Task<string?> StopItemIdle(ulong steamID, Bot bot)
		{
			if (!bot.HasPermission(steamID, BotConfig.EPermission.Master))
			{
				return null;
			}

			
			if (!ItemDropHandlers.TryGetValue(bot, out ItemDropHandler? ItemDropHandler))
			{
				return bot.Commands.FormatBotResponse(string.Format(Strings.ErrorIsEmpty, nameof(ItemDropHandlers)));
			}
			return bot.Commands.FormatBotResponse(await Task.Run<string>(() => ItemDropHandler.itemIdleingStop(bot)).ConfigureAwait(false));

		}
		private static async Task<string?> CheckItem(ulong steamID, Bot bot, string appid, string itemdefId)
		{
			if (!bot.HasPermission(steamID, BotConfig.EPermission.Master))
			{
				return null;
			}

			if (!uint.TryParse(appid, out uint appId))
			{
				return bot.Commands.FormatBotResponse(string.Format(Strings.ErrorIsInvalid, nameof(appId)));
			}
			if (!uint.TryParse(itemdefId, out uint itemdefid))
			{
				return bot.Commands.FormatBotResponse(string.Format(Strings.ErrorIsInvalid, nameof(itemdefid)));
			}
			if (!ItemDropHandlers.TryGetValue(bot, out ItemDropHandler? ItemDropHandler))
			{
				return bot.Commands.FormatBotResponse(string.Format(Strings.ErrorIsEmpty, nameof(ItemDropHandlers)));
			}
			return bot.Commands.FormatBotResponse(await Task.Run<string>(() => ItemDropHandler.checkTime(appId, itemdefid)).ConfigureAwait(false));

		}


	}

}
