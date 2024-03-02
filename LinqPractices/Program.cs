using LinqPractices.DbOperations;

namespace LinqPractices;

class Program
{
    static void Main(string[] args)
    {
        DataGenerator.Initialize();
        LinqDbContext _context = new LinqDbContext();
        var students = _context.Students.ToList();


    }
}
