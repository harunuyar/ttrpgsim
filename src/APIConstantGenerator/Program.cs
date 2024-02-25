namespace APIConstantGenerator;

using Dnd._5eSRD.Client;
using System.Text;

public partial class Program
{
    // Example: ability-scores to AbilityScores
    private static string RevertSnakeCase(string value)
    {
        var words = value.Split('-');
        var result = string.Empty;
        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                result += word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
        }

        // Remove all whitespaces
        result = new string(result.Where(c => char.IsLetterOrDigit(c)).ToArray());

        return result;
    }

    static void Main(string[] args)
    {
        var dndClient = new DndClient();

        var objectTypes = dndClient.GetObjectTypes().Result;

        foreach ((string objectType, string objectTypeUrl) in objectTypes)
        {
            var objectName = RevertSnakeCase(objectType);
            var constants = dndClient.GetAllReferenceObjects(objectTypeUrl).Result;

            var fileContent = new StringBuilder();
            fileContent.AppendLine($"namespace Dnd._5eSRD.Constants;");
            fileContent.AppendLine();
            fileContent.AppendLine($"public class {objectName}");
            fileContent.AppendLine("{");
            fileContent.AppendLine($"    public const string ObjectType = \"{objectTypeUrl}\";");
            foreach (var constant in constants)
            {
                fileContent.AppendLine($"    public const string {RevertSnakeCase(constant.Index!)} = \"{constant.Url}\";");
            }
            fileContent.AppendLine("}");

            var folderPath = Path.Combine("Dnd.5eSRD", "Constants");
            var filePath = Path.Combine("Dnd.5eSRD", "Constants", $"{objectName}.cs");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, fileContent.ToString());
        }

        // Generate Levels.cs explicitly
        {
            var fileContent = new StringBuilder();
            fileContent.AppendLine($"namespace Dnd._5eSRD.Constants;");
            fileContent.AppendLine();
            fileContent.AppendLine($"public class Levels");
            fileContent.AppendLine("{");

            var classes = dndClient.GetAllReferenceObjects("api/classes").Result;

            foreach (var @class in classes)
            {
                fileContent.AppendLine($"    public const string {RevertSnakeCase(@class.Index!)}ObjectType = \"{@class.Url}/levels\";");

                var levels = dndClient.GetAllObjectsLevelsExclusive<Dictionary<string, object>>($"{@class.Url}/levels").Result;

                foreach (var level in levels)
                {
                    fileContent.AppendLine($"    public const string {RevertSnakeCase(level["index"].ToString()!)} = \"{level["url"]}\";");
                }
            }

            fileContent.AppendLine("}");

            var folderPath = Path.Combine("Dnd.5eSRD", "Constants");
            var filePath = Path.Combine("Dnd.5eSRD", "Constants", "Levels.cs");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, fileContent.ToString());
        }

        // Generate SubclassLevels.cs explicitly
        {
            var fileContent = new StringBuilder();
            fileContent.AppendLine($"namespace Dnd._5eSRD.Constants;");
            fileContent.AppendLine();
            fileContent.AppendLine($"public class SubclassLevels");
            fileContent.AppendLine("{");

            var classes = dndClient.GetAllReferenceObjects("api/subclasses").Result;

            foreach (var @class in classes)
            {
                fileContent.AppendLine($"    public const string {RevertSnakeCase(@class.Index!)}ObjectType = \"{@class.Url}/levels\";");

                var levels = dndClient.GetAllObjectsLevelsExclusive<Dictionary<string, object>>($"{@class.Url}/levels").Result;

                foreach (var level in levels)
                {
                    fileContent.AppendLine($"    public const string {RevertSnakeCase(level["index"].ToString()!)} = \"{level["url"]}\";");
                }
            }

            fileContent.AppendLine("}");

            var folderPath = Path.Combine("Dnd.5eSRD", "Constants");
            var filePath = Path.Combine("Dnd.5eSRD", "Constants", "SubclassLevels.cs");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, fileContent.ToString());
        }
    }
}