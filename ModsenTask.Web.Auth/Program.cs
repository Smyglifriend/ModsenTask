using ModsenTask.DataAccess;
using ModsenTask.DataAccess.Extensions;
using ModsenTask.Web.Core.Extensions;
using ModsenTask.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDatabase(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebMapper()
    .AddUnitOfWork<ModsenTaskDbContext>()
    .AddIdentity(builder.Configuration)
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
app.UseRouting();
app.UseCors(builder.Services.GetUsingCors());
app.UseAuthentication();
app.UseAuthorization();
app.ExceptionMiddleware();
app.MapControllers();
app.Run();