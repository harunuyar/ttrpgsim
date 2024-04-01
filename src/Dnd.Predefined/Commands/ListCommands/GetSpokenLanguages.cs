namespace Dnd.Predefined.Commands.ListCommands;

using Dnd._5eSRD.Models.Language;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSpokenLanguages : ListCommand<LanguageModel>
{
    public GetSpokenLanguages(IGameActor actor) : base(actor)
    {
    }
}
