namespace LinqPractices.DbOperations;

public class DataGenerator
{
    public static void Initialize()
    {
        using(var context = new LinqDbContext())
        {
            if(context.Students.Any())
            {
                return;
            }

            context.AddRange(
                new Student(){
                    Name = "Yasir",
                    Surname = "KAYA",
                    ClassId = 1
                },
                new Student(){
                    Name = "Sefa",
                    Surname = "KARAGÖZ",
                    ClassId = 1
                },
                new Student(){
                    Name = "Bora",
                    Surname = "AYDIN",
                    ClassId = 2
                },
                new Student(){
                    Name = "Numan",
                    Surname = "SATICI",
                    ClassId = 2
                }
            );

            context.SaveChanges();
        }
    }
}