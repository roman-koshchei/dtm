using System.Linq;
using System.Reflection;

namespace translator
{
  public enum EntityId
  {
    Enum, Model
  }

  public struct RawEntity
  {
    public EntityId id;
    public string name;
    public string[] fields;
  }

  public class Parser
  {
    // add dictionary
    private static readonly string[] supportedTypes = {
      "string", "int", "long", "float", "double", "bool"
    };

    //private LinkedList<Model> models = new();

    private LinkedList<RawEntity> rawEntities = new();

    public LinkedList<Entity> ParseFile(string file)
    {
      string text = File.ReadAllText(file);

      string[] modelsStr = text.Split('}', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

      foreach (string modelStr in modelsStr) AddType(modelStr);

      LinkedList<Entity> entities = new();

      foreach (var rawEntity in rawEntities)
      {
        if (rawEntity.id == EntityId.Model)
        {
          entities.AddLast(ParseModel(rawEntity));
        }
        else if (rawEntity.id == EntityId.Enum)
        {
          //entities.AddLast(ParseEnum(rawEntity));
        }
      }

      return entities;
    }

    public static Entity ParseEnum(RawEntity entity)
    {
      Entity model = new()
      {
        name = entity.name,
        fields = new()
      };

      foreach (string fieldStr in entity.fields)
      {
        int nameTypeSeparator = fieldStr.IndexOf(' ');
        if (nameTypeSeparator == -1) throw new ParseException($"Field {fieldStr} is incorrect");

        string name = fieldStr[..nameTypeSeparator];
        string type = fieldStr[(nameTypeSeparator + 1)..].Replace(" ", "");

        int depth = type.Count(c => c == '[');
        if (depth > 0)
        {
          type = type[..type.IndexOf(']')];
        }

        model.fields.AddLast(new Field(name, type, depth));
      }
      return model;
    }

    public static Entity ParseModel(RawEntity entity)
    {
      Entity model = new()
      {
        name = entity.name,
        fields = new()
      };

      foreach (string fieldStr in entity.fields)
      {
        int nameTypeSeparator = fieldStr.IndexOf(' ');
        if (nameTypeSeparator == -1) throw new ParseException($"Field {fieldStr} is incorrect");

        string name = fieldStr[..nameTypeSeparator];
        string type = fieldStr[(nameTypeSeparator + 1)..].Replace(" ", "");

        int depth = type.Count(c => c == '[');
        if (depth > 0)
        {
          type = type[..type.IndexOf(']')];
        }

        model.fields.AddLast(new Field(name, type, depth));
      }
      return model;
    }

    public void CheckTypeSupport(string type)
    {
      //if (!supportedTypes.Contains(type) && !allTypeBodys.ContainsKey(type))
      //{
      //  throw new ParseException($"Type {type} is'n supported");
      //}
    }

    public void AddType(string modelStr)
    {
      if (!modelStr.Contains('{')) throw new ParseException("Open brecket '{' not found for some entity");

      string[] nameBody = modelStr.Split('{', StringSplitOptions.TrimEntries);

      string[] idName = nameBody[0].Split(' ', StringSplitOptions.TrimEntries);

      if (idName.Length != 2) throw new ParseException("Incorrect naming of dto entity");

      RawEntity entity = new()
      {
        id = idName[0] switch
        {
          "model" => EntityId.Model,
          "enum" => EntityId.Enum,
          _ => throw new ParseException("Incorrect entity id"),
        },
        name = idName[1],
        fields = nameBody[1].Split(new[] { '\n', ';' },
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
      };

      rawEntities.AddLast(entity);
    }
  }
}