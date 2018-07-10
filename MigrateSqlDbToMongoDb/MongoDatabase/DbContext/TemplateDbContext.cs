using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class TemplateDbContext
	{
		private readonly IMongoDatabase _database;

		public TemplateDbContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
			_database = client.GetDatabase(configuration.GetSection("MongoDB:TemplateDatabaseName").Value);
		}

		public IMongoCollection<Domain.Template.AggregatesModel.Template> TemplateCollection => _database.GetCollection<Domain.Template.AggregatesModel.Template>(nameof(Domain.Template.AggregatesModel.Template));
		public IQueryable<Domain.Template.AggregatesModel.Template> Templates => TemplateCollection.AsQueryable();

		public IMongoCollection<Domain.Template.AggregatesModel.User> UserCollection => _database.GetCollection<Domain.Template.AggregatesModel.User>(nameof(Domain.Template.AggregatesModel.User));
		public IQueryable<Domain.Template.AggregatesModel.User> Users => UserCollection.AsQueryable();

		public IMongoCollection<Domain.Template.AggregatesModel.OrganizationalUnit> OrganizationalUnitCollection => _database.GetCollection<Domain.Template.AggregatesModel.OrganizationalUnit>(nameof(Domain.Template.AggregatesModel.OrganizationalUnit));
		public IQueryable<Domain.Template.AggregatesModel.OrganizationalUnit> OrganizationalUnits => OrganizationalUnitCollection.AsQueryable();

		public IMongoCollection<Domain.Template.AggregatesModel.JobCategory> JobCategoryCollection => _database.GetCollection<Domain.Template.AggregatesModel.JobCategory>(nameof(Domain.Template.AggregatesModel.JobCategory));
		public IQueryable<Domain.Template.AggregatesModel.JobCategory> JobCategories => JobCategoryCollection.AsQueryable();
	}
}
