using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator
{
  public class Field
  {
    public string Name;

    public string Type;

    // depth means count of arrays
    public int Depth;

    public Field(string name, string type, int depth)
    {
      Name = name; Depth = depth; Type = type;
    }
  }

  public struct Entity
  {
    public string name;
    public LinkedList<Field> fields;
  }
}