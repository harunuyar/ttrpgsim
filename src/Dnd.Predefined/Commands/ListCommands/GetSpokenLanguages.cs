namespace Dnd.Predefined.Commands.ListCommands;

using Dnd._5eSRD.Models.Language;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSpokenLanguages : ListCommand<LanguageModel>
{
    public GetSpokenLanguages(IGameActor actor) : base(actor)
    {
    }

    protected override async Task InitializeResult()
    {
        foreach (var reference in Actor.Race.RaceModel?.Languages ?? [])
        {
            if (reference.Url == null)
            {
                continue;
            }

            var language = await DndContext.Instance.GetObject<LanguageModel>(reference.Url);

            if (language != null)
            {
                AddValue(language, $"From {Actor.Race.RaceModel?.Name}");
            }
        }

        AddValues(Actor.Race.LanguageOptions, "Chosen Language");

        if (Actor.Subrace != null)
        {
            AddValues(Actor.Subrace.LanguageOptions, "Chosen Language");
        }
    }
}
