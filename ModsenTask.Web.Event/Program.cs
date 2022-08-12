using ModsenTask.DataAccess;
using ModsenTask.DataAccess.Extensions;
using ModsenTask.Web.Core.Extensions;
using ModsenTask.Web.Event.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDatabase(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddEventWebMapper()
    .AddUnitOfWork<ModsenTaskDbContext>()
    .AddIdentity(builder.Configuration)
    .AddHttpContextAccessor()
    .AddServices()
    .ApplyCors()
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.ExceptionMiddleware();
app.MapControllers();
app.Run();