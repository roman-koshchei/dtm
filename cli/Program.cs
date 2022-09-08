using translator;

namespace cli
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      string dtoFile = @"C:\Users\roman\git\paragoda\dtm\docs\test.dto";

      Parser parser = new();
      try
      {
        parser.ParseFile(dtoFile);
      }
      catch (ParseException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch
      {
        Console.WriteLine("Some unexpected error");
      }
    }
  }
}