using Discord.Interactions;

namespace Hyenalerts.Interactions;

[Group("alerts", "View and manage alerts")]
public class AlertModule : InteractionModuleBase<InteractionContext>
{
    [SlashCommand("new", "Create a new named alert")]
    public async Task NewAsync(string name)
    {
        // Verify name isn't already in use
        // Ask which notification to add:
        //  twitch; online
        //  youtube; online, new video
        // Ask where to post it:
        //  discord, telegram, bsky, twitter
    }
}