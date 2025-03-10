using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace learningapp.Pages;

public class IndexModel : PageModel
{
     public List<Course> Courses=new List<Course>();
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration=configuration;
    }

    public void OnGet()
    {

        // usando o azure function para trazer os dados do banco
        string functionURL = "https://appfunction445.azurewebsites.net/api/sqltrigger";
        using(HttpClient client = new HttpClient()) {
            HttpResponseMessage response = await client.GetAsync(functionURL);
            string content = await response.Context.ReadAsStringAsync();
            Courses = JsonConverter.DeserializeObject<List<Course>>(content);
            return Page();

            
        //string connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
/*         var config = _configuration.GetSection("Common:Settings");
        string? connectionString = config.GetValue<string>("dbpassword");

        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        var sqlcommand = new SqlCommand(
        "SELECT CourseID,CourseName,Rating FROM Course;",sqlConnection);
         using (SqlDataReader sqlDatareader = sqlcommand.ExecuteReader())
         {
             while (sqlDatareader.Read())
                {
                    Courses.Add(new Course() {CourseID=Int32.Parse(sqlDatareader["CourseID"].ToString()),
                    CourseName=sqlDatareader["CourseName"].ToString(),
                    Rating=Decimal.Parse(sqlDatareader["Rating"].ToString())});
                }
         }   */

        }


    }
}
